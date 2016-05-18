using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorDataModel.Models
{
    public class CustomGroup
    {
        [Key]
        public int CustomGroupId { get; set; }

        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Display(Name = "Custom Group Name")]
        public string CustomGroupName { get; set; }

        [Display(Name = "Project Name")]
        public string ProjectName { get; set; }

        [Display(Name = "Sensor Name")]
        public string SensorName { get; set; }
    }
}
