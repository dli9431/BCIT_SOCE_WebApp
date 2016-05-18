using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorDataModel.Models
{
    public class SensorProject
    {
        [Key]
        public int SensorProjectId { get; set; }

        [Required]
        [Display(Name = "Sensor Project Name")]
        public string SensorProjectName { get; set; }

        [Required]
        [Display(Name = "Sensor Project Type")]
        public string SensorProjectType { get; set; }

        [Required]
        [Display(Name = "Sensor Name")]
        public string SensorName { get; set; }
    }
}
