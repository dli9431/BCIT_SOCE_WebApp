using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorDataModel.Models
{
    public class Sensor
    {
        [Key]
        public int sensorId { get; set; }

        public string SensorName { get; set; }

        public double? SensorValue { get; set; }

        public DateTime DateTime { get; set; }

        public string ProjectName { get; set; }
    }
}
