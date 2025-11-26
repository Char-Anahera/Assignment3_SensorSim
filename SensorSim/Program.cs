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
                Data.StartSensor();
                //var newSensor = Sensor.InitializeSensor();

                //while (true)
                //{
                //    var data = new Data();
                //    var sensorData = data.SimulateData(newSensor);
                //    Console.WriteLine(sensorData.LogData(sensorData));
                //    Thread.Sleep(1000);
                //}

            }
            catch(SensorInitializeException ex)
            {
                Console.WriteLine($"Failed to initialize sensor {ex.Message}");
            }
        }
    }
}