
using System;
using System.Data;
using System.Data.SqlClient;
using HttpDtos;
using ServiceStack.ServiceInterface;

namespace HttpServices
{
    public class ProbeService : Service
    {
        private readonly DbConn _connection;

        /// <summary>
        /// The Reporter ID for a probe update
        /// </summary>
        internal int ProbeId
        {
            get
            {
                if (!_probeId.HasValue)
                {
                    using (var transaction = _connection.Conn.BeginTransaction(IsolationLevel.RepeatableRead))
                    {
                        using (
                            var command = new SqlCommand(
                                "SELECT Id FROM Doctors WHERE FirstName='Auto' AND Surname='Probe'",
                                transaction.Connection, transaction))
                        {
                            _probeId = (int)command.ExecuteScalar();
                        }
                    }
                }
                return _probeId.Value;
            }
            set { _probeId = value; }
        }
        private static int? _probeId;

        public ProbeService(DbConn connection)
        {
            _connection = connection;
        }

        public void Post(Probe request)
        {
            // Validate Input
            if (!request.PatientId.HasValue) throw new ArgumentException("No Patient ID was provided");
            if (!request.HeartRate.HasValue) throw new ArgumentException("No Heart Rate was provided");
            if (!request.OxygenSaturations.HasValue) throw new ArgumentException("No Oxygen Saturations value was provided");
            if (!request.RespirationRate.HasValue) throw new ArgumentException("No Respiration Rate was provided");
            if (!request.SystolicBp.HasValue) throw new ArgumentException("No Blood Pressure value was provided");
            if (!request.Temperature.HasValue) throw new ArgumentException("No Temperature was provided");
            
            // If no doctor code was included then assume it was an automatic probe reading
            if (!request.Reporter.HasValue)
                request.Reporter = ProbeId;
            // Within one transaction
            using (var transaction = _connection.Conn.BeginTransaction(IsolationLevel.RepeatableRead))
            {
                // Check the patient exists
                using (
                    var command = new SqlCommand("SELECT COUNT(*) FROM Patients WHERE Id=@patientId;",
                        transaction.Connection, transaction))
                {
                    command.Parameters.AddWithValue("@patientId", request.PatientId);
                    if ((int) command.ExecuteScalar() <= 0)
                        throw new ArgumentException(string.Format("Patient with ID {0} does not exist",
                            request.PatientId));
                }
                // Insert the new record
                using (
                    var command = new SqlCommand(
                        "INSERT INTO PatientUpdates " +
                        "   (Patient, Reporter, RespirationRate, OxygenSaturations, Temperature, SystolicBloodPresure, HeartRate) " +
                        "VALUES " +
                        "(@patId, @probe, @rr, @os, @tmp, @sbp, @hr);",
                        transaction.Connection, transaction))
                {
                    command.Parameters.AddWithValue("@patId", request.PatientId);
                    command.Parameters.AddWithValue("@probe", request.Reporter);
                    command.Parameters.AddWithValue("@rr", request.RespirationRate);
                    command.Parameters.AddWithValue("@os", request.OxygenSaturations);
                    command.Parameters.AddWithValue("@tmp", request.Temperature);
                    command.Parameters.AddWithValue("@sbp", request.SystolicBp);
                    command.Parameters.AddWithValue("@hr", request.HeartRate);

                    var numRowsUpdated = command.ExecuteNonQuery();
                    if (numRowsUpdated != 1)
                        throw new ArgumentException("Expected one row to be updated. Actually: "+ numRowsUpdated);
                }
                // Commit the transaction
                transaction.Commit();
            }
        }
    }
}
