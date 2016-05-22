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

        [Display(Name = "Sensor Project Name")]
        // Linked with ProjectName (can be anything)
        public string SensorProjectName { get; set; }
        
        [Display(Name = "Sensor Project Type")]
        // This links with Sensor Model's ProjectName
        public string SensorProjectType { get; set; }

        [Display(Name = "Sensor Name")]
        public string SensorName { get; set; }
    }
}
