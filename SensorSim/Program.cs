using System;
using SensorSim.Models;

namespace Sensors
{
     internal static class Program
    {
        private static bool exitSimulation = false;
        private static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Press Q at any time to quit");

                Thread keyDetectionThread = new Thread(DetectKey);
                keyDetectionThread.Start();

                Sensor.StartSensor();

            }
            catch (SensorInitializeException ex)
            {
                Console.WriteLine($"Failed to initialize sensor: {ex.Message}");
                Sensor.StopSensor();
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                Sensor.StopSensor();
            }
        }

        private static void DetectKey()
        {
            while (!exitSimulation)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                    if (keyInfo.Key == ConsoleKey.Q)
                    {
                        exitSimulation = true;
                        Sensor.StopSensor();
                    }
                }
                Thread.Sleep(100);
            }
        }
    }
}