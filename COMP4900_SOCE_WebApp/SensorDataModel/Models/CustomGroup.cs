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

        public string CustomGroupName { get; set; }
        public string SensorName { get; set; }
        
    }
}
