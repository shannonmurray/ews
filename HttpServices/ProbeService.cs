
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

        public ProbeService(DbConn connection)
        {
            _connection = connection;
        }

        public void Post(Probe request)
        {
            using (var transaction = _connection.Conn.BeginTransaction(IsolationLevel.RepeatableRead))
            {
                using (
                    var command = new SqlCommand("SELECT COUNT(*) FROM patients WHERE patient_id=@patientId;",
                        transaction.Connection, transaction))
                {
                    command.Parameters.AddWithValue("@patientId", request.PatientId);
                    if ((int) command.ExecuteScalar() <= 0)
                        throw new ArgumentException(string.Format("Patient with ID {0} does not exist",
                            request.PatientId));
                }

                using (
                    var command = new SqlCommand(
                        "INSERT INTO patient_updates " +
                        "   (patient_id, logged_at, respiration_rate, oxygen_saturation, temperature, systolic_bp, heart_rate) " +
                        "VALUES " +
                        "(@patId, GETDATE(), @rr, @os, @tmp, @sbp, @hr);",
                        transaction.Connection, transaction))
                {
                    command.Parameters.AddWithValue("@patId", request.PatientId);
                    command.Parameters.AddWithValue("@rr", request.RespirationRate);
                    command.Parameters.AddWithValue("@os", request.OxygenSaturation);
                    command.Parameters.AddWithValue("@tmp", request.Temperature);
                    command.Parameters.AddWithValue("@sbp", request.SystolicBp);
                    command.Parameters.AddWithValue("@hr", request.HeartRate);

                    var numRowsUpdated = command.ExecuteNonQuery();
                    if (numRowsUpdated != 1)
                        throw new ArgumentException("Expected one row to be updated. Actually: "+ numRowsUpdated);
                }

                transaction.Commit();
            }
        }
    }
}
