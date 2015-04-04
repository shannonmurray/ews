
using ServiceStack.ServiceHost;

namespace HttpDtos
{
    [Route("/api/probe/{PatientId}", Verbs = "POST",
        Summary = "Handles a patient update")]
    public class Probe : IReturnVoid
    {
        public long PatientId { get; set; }
        public int RespirationRate { get; set; }
        public int OxygenSaturation { get; set; }
        public float Temperature { get; set; }
        public int SystolicBp { get; set; }
        public int HeartRate { get; set; }
    }
}
