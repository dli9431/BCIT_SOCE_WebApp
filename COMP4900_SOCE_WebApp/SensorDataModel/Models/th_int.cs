using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace SensorDataModel.Models
{
    public class th_int
    {
        [Key]
        public int th_intId { get; set; }

        public string SensorName { get; set; }

        public double SensorValue { get; set; }

        public DateTime DateTime { get; set; }
    }
}
