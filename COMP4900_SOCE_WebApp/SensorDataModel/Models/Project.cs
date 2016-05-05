using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorDataModel.Models
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }

        //[Required]
        [Display(Name = "Project Name")]
        public string Name { get; set; }

        //[Required]
        [Display(Name = "Project Description")]
        public string Description { get; set; }

        //[Required]
        [Display(Name = "Supervisor")]
        public IEnumerable<string> supervisors { get; set; }

        //[Required]
        [Display(Name = "Students")]
        public IEnumerable<string> students { get; set; }

        //user sets this for project
        //[Required]
        //[Display(Name = "Sensor Group")]

        //public IEnumerable<> sensors { get; set; }

    }
}
