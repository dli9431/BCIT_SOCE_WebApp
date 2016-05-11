using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace SensorDataModel.Models
{
    public class gvs_north
    {
        [Key]
        public int gvs_northId { get; set; }

        public string SensorName { get; set; }

        public double SensorValue { get; set; }

        public int SensorDateTimeId { get; set; }
    }
}
