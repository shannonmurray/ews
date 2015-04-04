
using ServiceStack.ServiceHost;

namespace HttpDtos
{
    [Route("/api/probe/{PatientId}", Verbs = "POST",
        Summary = "Handles a patient update")]
    public class Probe : IReturnVoid
    {
        public int? PatientId { get; set; }
        public int? Reporter { get; set; }
        public int? RespirationRate { get; set; }
        public int? OxygenSaturations { get; set; }
        public float? Temperature { get; set; }
        public int? SystolicBp { get; set; }
        public int? HeartRate { get; set; }
    }
}
