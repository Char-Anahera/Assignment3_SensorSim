using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorSim.Models
{
    public class Sensor
    {
        public string Name {  get; set; }
        public string Location { get; set; }
        public double MinTemp { get; set; }
        public double MaxTemp { get; set; }


        public Sensor(string name, string location, double minTemp, double maxTemp  )
        {
            Name = name;
            Location = location;
            MinTemp = minTemp;
            MaxTemp = maxTemp;
        }
    }
}
