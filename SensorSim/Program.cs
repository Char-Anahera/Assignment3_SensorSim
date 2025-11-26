using System;
using SensorSim.Models;

namespace Sensors
{
     internal static class Program
    {
        // boolean to check if simulator has been requested to stop
        private static bool exitSimulation = false;

        private static void Main(string[] args)
        {
            try
            {
                // prints line to inform user they can quit at any time
                Console.WriteLine("Press Q at any time to quit");

                // creates a new thread and calls on detectKey method to check if quit has been requested
                Thread keyDetectionThread = new Thread(DetectKey);
                keyDetectionThread.Start();

                //starts sensor loop
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

        // method to check if Q key has been pressed
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