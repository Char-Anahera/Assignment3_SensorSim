using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SensorSim.Configuration;

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
            var repo = new DataRepo();

            repo.StoreData(sensorData);

            Console.WriteLine($"{sensorData.DateTime:HH:mm:ss} Temperature: {sensorData.Temperature:F2} °C");
        }
    }
}
