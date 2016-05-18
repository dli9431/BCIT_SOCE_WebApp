using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorDataModel.Models
{
    public class SavedReport
    {
        [Key]
        public int SavedReportId { get; set; }

        [Display(Name = "Report Name")]
        public string ReportName { get; set; }

        [Display(Name = "Project Name")]
        public string ProjectName { get; set; }

        [Display(Name = "Custom Group Name")]
        public string CustomGroupName { get; set; }

        [Display(Name = "Begin Date Time")]
        public DateTime BeginDate { get; set; }

        [Display(Name = "End Date Time")]
        public DateTime EndDate { get; set; }

    }
}
