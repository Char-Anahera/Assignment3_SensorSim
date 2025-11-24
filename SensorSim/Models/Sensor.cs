using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SensorSim.Models
{
    public class Sensor
    {
        public int Id { get; set; }
        public string Name {  get; set; }
        public string Location { get; set; }
        public double MinTemp { get; set; }
        public double MaxTemp { get; set; }


        //Create constructor to ensure every sensor object has all attributes
        [JsonConstructor]
        public Sensor(int id, string name, string location, double minTemp, double maxTemp) 
        {
            Id = id;
            Name = name;
            Location = location;
            MinTemp = minTemp;
            MaxTemp = maxTemp;
        }


        private static readonly string filePath = Path.Combine("Data", "SensorConfig.json");

        public static Sensor? InitializeSensor()
        {
            if (!File.Exists(filePath))
            {
                return null;
            }

            string jsonString = File.ReadAllText(filePath);

            if (string.IsNullOrEmpty(jsonString))
            {
                return null;
            }

            var newSensor = JsonSerializer.Deserialize<Sensor>(jsonString);

            if (IsValid(newSensor) == false)
            {
                return null;
            }

            return newSensor;
        }

        //Checks sensor to see if it has all valid attributes
        public static Boolean IsValid(Sensor sensor)
        {
            if (sensor.Id <= 0) return false;
            if (string.IsNullOrEmpty(sensor.Name)) return false;
            if (string.IsNullOrEmpty(sensor.Location)) return false;
            if (sensor.MinTemp <= 0 || sensor.MaxTemp <= 0) return false;
            if (sensor.MinTemp >= sensor.MaxTemp) return false;

            return true;
        }



        //// Load sensor from config file
        //public static Sensor? LoadSensor()
        //{
        //    try
        //    {
        //        if (!File.Exists(filePath))
        //        {
        //            return null;
        //        }

        //        string jsonString = File.ReadAllText(filePath);

        //        Sensor obj = JsonSerializer.Deserialize<Sensor>(jsonString);
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}
        
    }
}
