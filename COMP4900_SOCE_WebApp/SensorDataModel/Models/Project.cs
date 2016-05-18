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

        [Required]
        [Display(Name = "Project Name")]
        public string ProjectName { get; set; }

        [Required]
        [Display(Name = "Project Description")]
        public string ProjectDescription { get; set; }
        
        [Required]
        [Display(Name = "Username")]
        [UIHint("_UsersDropDown")]
        public string UserName { get; set; }

    }
}
