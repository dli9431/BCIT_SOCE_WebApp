using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace SensorDataModel.Models
{
    public class th_ext
    {
        [Key]
        public int th_extId { get; set; }

        public string SensorName { get; set; }

        public double SensorValue { get; set; }

        public DateTime DateTime { get; set; }
    }
}
