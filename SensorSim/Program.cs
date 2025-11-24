using System;
using SensorSim.Models;

namespace Sensors
{
     internal static class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                var sensor = Sensor.InitializeSensor();

                if (sensor != null)
                {
                    Console.WriteLine("Sensor ID:" + sensor.Id);
                    Console.WriteLine("Sensor Name:" + sensor.Name);
                    Console.WriteLine("Sensor Location:" + sensor.Location);
                    Console.WriteLine("Sensor Initialized Successfully");
                }
            }
            catch(SensorInitializeException ex)
            {
                Console.WriteLine($"Failed to initialize sensor {ex.Message}");
            }
            

            

        }
    }
}