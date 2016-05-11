using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorDataModel.Models
{
    public class UserGroup
    {
        [Key]
        public int UserIdIndex { get; set; }

        public string StudentId { get; set; }
        public int CustomGroupId { get; set; }
        
    }
}
