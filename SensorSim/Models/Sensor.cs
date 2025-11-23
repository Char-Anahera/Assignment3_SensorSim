using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
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


        private static readonly string filePath = Path.Combine("Data", "SensorConfig.json");

        // Load sensor from config file
        public static Sensor? LoadSensor()
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    return null;
                }

                string jsonString = File.ReadAllText(filePath);

                return JsonSerializer.Deserialize<Sensor>(jsonString) ;
            }
            catch
            {
                return null;
            }
        }


        public static Sensor ValidateSensor()
        {
            try
            {
                
                 
            }
            catch
            {

            }
        }
        
    }
}
