using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorDataModel.Models
{
    public class User
    {
        [Key]
        public int UserIdIndex { get; set; }

        public string StudentId { get; set; }
        public int CustomGroupId { get; set; }
        
    }
}
