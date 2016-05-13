using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace SensorDataModel.Models
{
    public class mh_north
    {
        [Key]
        public int mh_northId { get; set; }

        public string SensorName { get; set; }

        public double SensorValue { get; set; }

        public DateTime DateTime { get; set; }
    }
}
