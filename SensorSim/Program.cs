using System;
using SensorSim.Configuration;
using SensorSim.Models;

namespace Sensors
{
     internal static class Program
    {
        // boolean to check if simulator has been requested to stop
        private static bool mainPage = true;
        private static bool sensorOn = false;

        private static void Main(string[] args)
        {
            try
            {
                Database.Initialize();

                while (mainPage)
                {
                    try
                    {
                        Console.WriteLine("===============================================");
                        Console.WriteLine("            Sensor Simulator");
                        Console.WriteLine("      Please choose an option to begin");
                        Console.WriteLine("===============================================");
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine("1: Start Sensor");
                        Console.WriteLine("2: View History");
                        Console.WriteLine("3: View Average Temperatures");
                        Console.WriteLine("4: Reset Sensor");
                        Console.WriteLine();

                        string input = Console.ReadLine();
                        int option = Convert.ToInt32(input);

                        switch (option)
                        {
                            case 1:
                                sensorOn = true;

                                // creates a new thread and calls on detectKey method to check if quit has been requested
                                Thread keyDetectionThread = new Thread(DetectKey);
                                keyDetectionThread.Start();

                                Console.WriteLine();
                                // prints line to inform user they can quit at any time
                                Console.WriteLine("Press Q to return to home");
                                Console.WriteLine();

                                //starts sensor loop
                                Sensor.StartSensor();
                                break;

                            case 2:
                                // prints line to inform user they can quit at any time
                                Sensor.ViewHistory();
                                break;

                            case 3:
                                // prints line to inform user they can quit at any time
                                Sensor.GetAverage();
                                break;

                            case 4:
                                // prints line to inform user they can quit at any time
                                Console.WriteLine();
                                Console.WriteLine("Are you sure you want to delete the sensor history? You cannot get this back.");
                                Console.WriteLine("This will remove all history and reset all averages.");
                                Console.WriteLine("Y - yes");
                                Console.WriteLine("N - no");

                                string confirmReset = Console.ReadLine();

                                if (confirmReset == "Y" || confirmReset == "y")
                                {
                                    Sensor.ResetSensor();
                                }

                                break;

                            default:
                                Console.WriteLine();
                                Console.WriteLine("Please choose an option 1 - 4");
                                Console.WriteLine();
                                break;
                        }
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Invalid input. Please enter an option, 1 - 4");
                        Console.WriteLine();
                        continue;
                    }
                }
            }
            catch (SensorInitializeException ex)
            {
                Console.WriteLine($"Failed to initialize sensor: {ex.Message}");
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        // method to check if Q key has been pressed
        private static void DetectKey()
        {
            while (sensorOn)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                    if (keyInfo.Key == ConsoleKey.Q)
                    {
                        Sensor.StopSensor();

                        sensorOn = false;
                    }
                }
                Thread.Sleep(100);
            }
        }
    }
}