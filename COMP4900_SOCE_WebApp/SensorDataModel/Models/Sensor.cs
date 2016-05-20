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

        [Required]
        [Display(Name = "Sensor Name")]
        public string SensorName { get; set; }

        [Display(Name = "Sensor Value")]
        public double? SensorValue { get; set; }

        [Required]
        [Display(Name = "Date Time")]
        public DateTime DateTime { get; set; }

        [Required]
        [Display(Name = "Project Name")]
        // Linked with SensorProject's SensorProjectType
        public string ProjectName { get; set; }
    }
}
