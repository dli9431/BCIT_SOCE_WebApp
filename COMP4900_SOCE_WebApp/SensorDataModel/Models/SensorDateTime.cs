using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorDataModel.Models
{
    public class SensorDateTime
    {
        [Key]
        public int DateTimeId { get; set; }

        public DateTime DateTime { get; set; }
    }
}
