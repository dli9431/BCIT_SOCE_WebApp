using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorDataModel.Models
{
    public class GroupProject
    {
        public Project Projects { get; set; }
        public SensorProject SensorProject { get; set; }
    }
}
