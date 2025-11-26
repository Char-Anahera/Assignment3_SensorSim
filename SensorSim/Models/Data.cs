using SensorSim.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SensorSim.Models
{
    public class Data
    {
        // Set RNG for simulating a temperature.
        private static readonly Random Random = new();

        // defines attributes of data
        public string SensorName { get; set; }
        public double Temperature {  get; set; }
        public DateTime DateTime { get; set; }


        // Simulate data method. Uses parsed sensor as parameters, returns the data it simulated
        public Data SimulateData(Sensor sensor)
        {
            // While loop to try again for data if it is not valid
            while (true)
            {
                // rng to generate a temperature based on sensors min and max
                double temperature = (Random.NextDouble()*(sensor.MaxTemp - sensor.MinTemp) + sensor.MinTemp);
                var newData = new Data();

                // set data attributes
                newData.SensorName = sensor.Name;
                newData.Temperature = temperature;
                newData.DateTime = DateTime.Now;

                // validation check
                if (ValidateData(newData, sensor))
                {
                    // returns validated data
                    return newData;
                }
            }
        }


        // check for if data is valid, based on the sensors parameters
        public bool ValidateData(Data sensorData, Sensor sensor)
        {
            // Checks if data exists and if it falls between the sensors set range
            if (sensorData == null) return false;
            if (sensorData.Temperature < sensor.MinTemp) return false;
            if (sensorData.Temperature > sensor.MaxTemp) return false;

            // Returns true if passes
            else return true;
        }


        // Log data method, calls the storeData method and returns a formatted string
        public void LogData(Data sensorData)
        {
            // creates repo to acccess database
            var repo = new DataRepo();

            // stores data in the database
            repo.StoreData(sensorData);

            // checks if data could be an anomaly. Notifies user if true
            if (DetectAnomaly(sensorData))
            {
                Console.WriteLine("Unusual reading. Anomaly detected.");
            }

            // displays to user the recent data and smoothed data average
            Console.WriteLine($"{sensorData.DateTime:HH:mm:ss}     Temperature: {sensorData.Temperature:F2}°C");
            Console.WriteLine($"            Average: {SmoothData():F2}°C");
            Console.WriteLine();
        }


        // smooth data method to reduce noise and give a clean average
        public static double SmoothData()
        {
            // creates repo to access database
            var repo = new DataRepo();
            var history = repo.GetRecentTemps();

            // calculates average of the last 5 entries
            double average = 0;

            foreach (var t in history)
            {
                average += t;
            }

            double smoothedData = average / history.Count;

            // return average
            return smoothedData;
        }

        // detect anomaly method to check if a value deviates from the recent average
        public bool DetectAnomaly(Data data)
        {
            // calls method to find the recent average
            double average = FindRecentAverage();

            // defines what spikes are
            double sensitivity = 1;

            // checks if temperature is between the upper and lower bounds of the average
            if(data.Temperature > (average + sensitivity) || data.Temperature < (average - sensitivity))
            {
                return true;
            }
            return false;
        }


        // find recent average method to find the average of last 15 data entries
        public static double FindRecentAverage()
        {
            // calls repo to access database
            var repo = new DataRepo();
            var history = repo.GetAverageTemps();

            // calculates average
            double average = 0;

            foreach(var t in history)
            {
                average += t;
            }

            average = average / history.Count;

            //returns average
            return average;
        }
    }
}
