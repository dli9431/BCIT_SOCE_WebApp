using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorDataModel.Models
{
    public class gvs_south
    {
        [Key]
        public int gvs_southId { get; set; }

        public string SensorName { get; set; }

        public double SensorValue { get; set; }

        public int SensorDateTimeId { get; set; }
    }
}
