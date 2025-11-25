using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorSim.Models
{
    
    public class Data
    {
        private static readonly Random Random = new();

        public string SensorName { get; set; }
        public double Temperature {  get; set; }
        public DateTime DateTime { get; set; }


        public void StartSensor(Sensor sensor) 
        {


        }

        public Data SimulateData(Sensor sensor)
        {
            double variation = 5.0;
            double mean = (sensor.MinTemp + sensor.MaxTemp) / 2;

            while (true)
            {
                double temperature = mean + (Random.NextDouble() * 2 - 1) * variation;
                var newData = new Data();

                newData.SensorName = sensor.Name;
                newData.Temperature = temperature;
                newData.DateTime = DateTime.Now;

                if (ValidateData(newData, sensor))
                {
                    return newData;
                }
            }
        }

        public bool ValidateData(Data sensorData, Sensor sensor)
        {
            if (sensorData == null) return false;
            if (sensorData.Temperature < sensor.MinTemp) return false;
            if (sensorData.Temperature > sensor.MaxTemp) return false;
            else return true;
        }


        public string LogData(Data sensorData)
        {
            Data.StoreData(sensorData);

            return $"{sensorData.DateTime:HH:mm:ss} Temperature: {sensorData.Temperature} °C";
        }


        public static void StoreData(Data sensorData)
        {

        }

        public void StopSensor()
        {

        }
    }
}

//while (true)
//{
//    double tempC = GenerateTemperature();
//    Console.WriteLine($"{DateTime.Now:HH:mm:ss}  Temperature: {tempC:F1} °C");
//    Thread.Sleep(1000);
//    var reading = new Reading();
//    reading.SensorName = sensorName;
//    reading.DateTime = DateTime.Now;
//    reading.Value = tempC;


//}
//        }

//        private static double GenerateTemperature()
//{
//    const double mean = 22.0;
//    const double variation = 5.0;
//    return mean + (Random.NextDouble() * 2 - 1) * variation;
//}