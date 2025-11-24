using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
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


        private static readonly string filePath = Path.Combine(AppContext.BaseDirectory, "Configuration", "SensorConfig.json");

        public static Sensor? InitializeSensor()
        {
            try
            {

                if (!File.Exists(filePath)) { throw new SensorInitializeException("Configuration file was not found"); }

                string jsonString = File.ReadAllText(filePath);

                if (string.IsNullOrEmpty(jsonString)) { throw new SensorInitializeException("Configuration file is empty"); }

                string verifiedString = SanitizeString(jsonString);

                var newSensor = JsonSerializer.Deserialize<Sensor>(verifiedString);

                if (newSensor == null) { throw new SensorInitializeException("Object could not be parsed"); }

                if (IsValid(newSensor) == false) { throw new SensorInitializeException("Sensor configuration is invalid"); }

                return newSensor;

            }
            catch (Exception ex)
            {
                throw new SensorInitializeException($"Invalid JSON format: {ex.Message}");
            }
            
        }

        //Checks sensor to see if it has all valid attributes
        public static bool IsValid(Sensor sensor)
        {
            if (sensor.Id <= 0) return false;
            if (string.IsNullOrEmpty(sensor.Name) || sensor.Name.Length > 100) return false;
            if (string.IsNullOrEmpty(sensor.Location) || sensor.Location.Length > 100) return false;
            if (sensor.MinTemp <= 0 || sensor.MaxTemp <= 0) return false;
            if (sensor.MinTemp >= sensor.MaxTemp) return false;

            return true;
        }

        //Sanitizes JSON input
        public static string SanitizeString(string input)
        {
            // Remove <script> tags
            input = Regex.Replace(input, "<script.*?>.*?</script>", "", RegexOptions.IgnoreCase | RegexOptions.Singleline);

            // Remove all HTML tags like <tag>
            input = Regex.Replace(input, "<.*?>", "");

            // Remove dangerous control characters
            input = Regex.Replace(input, @"[\x00-\x1F]", "");

            return input.Trim();
        }
    }

    public class SensorInitializeException : Exception
    {
        public SensorInitializeException(string message) : base(message)
        {
        }
    }

}
