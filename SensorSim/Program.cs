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
                var newSensor = Sensor.InitializeSensor();

                while (true)
                {
                    var data = new Data();
                    var sensorData = data.SimulateData(newSensor);


                    Console.WriteLine($"{sensorData.DateTime:HH:mm:ss} Temperature: {sensorData.Temperature} °C");
                    Thread.Sleep(1000);
                }

            }
            catch(SensorInitializeException ex)
            {
                Console.WriteLine($"Failed to initialize sensor {ex.Message}");
            }
            

            

        }
    }
}