using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace SensorDataModel.Models
{
    public class mh_south
    {
        [Key]
        public int mh_southId { get; set; }

        public string SensorName { get; set; }

        public double SensorValue { get; set; }

        public int SensorDateTimeId { get; set; }
    }
}
