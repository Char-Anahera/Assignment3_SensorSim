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
        // sets status for sensor being on or off
        private static bool sensorOn = false;

        //sets path for configuration file
        private static readonly string filePath = Path.Combine(AppContext.BaseDirectory, "Configuration", "SensorConfig.json");

        // defines attributes of sensor 
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public double MinTemp { get; set; }
        public double MaxTemp { get; set; }


        // constructor for senso. requires all attributes
        public Sensor(int id, string name, string location, double minTemp, double maxTemp)
        {
            Id = id;
            Name = name;
            Location = location;
            MinTemp = minTemp;
            MaxTemp = maxTemp;
        }

        // method to initialize sensor. Checks sensor can be used before returning it
        public static Sensor? InitializeSensor()
        {
            try
            {
                // Checks the content of the file and contents
                // checks file exists, throws error if not
                if (!File.Exists(filePath))
                {
                    throw new SensorInitializeException("Configuration file was not found");
                }

                string jsonString = File.ReadAllText(filePath);

                // checks if the file was empty
                if (string.IsNullOrEmpty(jsonString))
                {
                    throw new SensorInitializeException("Configuration file is empty");
                }

                // calls sanitizeString method to avoid using malicious content
                string verifiedString = SanitizeString(jsonString);


                // Checks the stored sensor can be used as an object
                // converts the string to a sensor object
                var newSensor = JsonSerializer.Deserialize<Sensor>(verifiedString);

                // checks if sensor was parsed to an object correctly
                if (newSensor == null)
                {
                    throw new SensorInitializeException("Object could not be parsed");
                }

                // calls isValid method to check the configuration is correct
                if (!IsValid(newSensor))
                {
                    throw new SensorInitializeException("Sensor configuration is invalid");
                }

                //if the sensor passes all checks, it is returned
                return newSensor;
            }
            // catch to define what error occurred
            catch (Exception ex)
            {
                throw new SensorInitializeException($"{ex.Message}");
            }
        }

        //Checks sensors attributes are valid and logical
        public static bool IsValid(Sensor sensor)
        {
            // disallows any negative values, empty strings or strings over 100 characters
            if (sensor.Id <= 0) return false;
            if (string.IsNullOrEmpty(sensor.Name) || sensor.Name.Length > 100) return false;
            if (string.IsNullOrEmpty(sensor.Location) || sensor.Location.Length > 100) return false;
            if (sensor.MinTemp <= 0 || sensor.MaxTemp <= 0) return false;
            if (sensor.MinTemp >= sensor.MaxTemp) return false;

            // if it passes all checks, confirm valid
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

        // startSensor loop
        public static void StartSensor()
        {
            // turns sensor on
            sensorOn = true;

            // calls InitializeSensor to return clean sensor
            var newSensor = InitializeSensor();

            while (sensorOn)
            {
                // calls simulateData to produce a reading
                var data = new Data();
                var sensorData = data.SimulateData(newSensor);

                // calls logData to format simulated data and store to the database. Then prints data
                string formattedData = sensorData.LogData(sensorData);
                Console.WriteLine(formattedData);

                // pauses simulator
                Thread.Sleep(1000);
            }
        }

        // stopSensor
        public static void StopSensor()
        {
            // turns sensor off
            sensorOn = false;

            // prints line confirming sensor has stopped
            Console.WriteLine("Sensor has stopped");
        }
    }
}

    public class SensorInitializeException : Exception
    {
        public SensorInitializeException(string message) : base(message)
        {
        }
    }