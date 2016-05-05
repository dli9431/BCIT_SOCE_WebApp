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

        public string SensorProjectName { get; set; }

        public string SensorProjectType { get; set; }

        public string SensorName { get; set; }
    }
}
