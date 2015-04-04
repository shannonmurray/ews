
using System;
using HttpDtos;
using ServiceStack.ServiceClient.Web;

namespace Simulator
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new JsonServiceClient("http://localhost:59801/");
            var random = new Random();
            while (true)
            {
                client.Post(new Probe
                {
                    PatientId = 0,
                    HeartRate = 0,
                    OxygenSaturation = 43,
                    RespirationRate = 23,
                    SystolicBp = 99,
                    Temperature = (float)random.NextDouble() * 50
                });
                System.Threading.Thread.Sleep(20000);
            }
        }
    }
}
