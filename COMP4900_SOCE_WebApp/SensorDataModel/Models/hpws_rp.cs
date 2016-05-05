using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace SensorDataModel.Models
{
    public class hpws_rp
    {
        [Key]
        public string SensorProjectName { get; set; }

        public string SensorName { get; set; }

        public double SensorValue { get; set; }

        public int SensorDateTimeId { get; set; }
    }
}
