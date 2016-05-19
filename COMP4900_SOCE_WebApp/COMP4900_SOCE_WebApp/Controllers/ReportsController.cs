using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SensorDataModel.Models;
using COMP4900_SOCE_WebApp.Models;

namespace COMP4900_SOCE_WebApp.Controllers
{
    public class ReportsController : Controller
    {
        private SensorContext db = new SensorContext();
        private ApplicationDbContext db2 = new ApplicationDbContext();
        
        // GET: Reports
        public ActionResult Reports(int? id)
        {
            var projectName = db.Projects
                .Where(m => m.ProjectId == id)
                .Select(m => m.ProjectName).FirstOrDefault();

            var projectTypes = db.SensorProjects
                .Where(m => m.SensorProjectName == projectName)
                .Select(m => m.SensorProjectType)
                .ToList();

            var sensorList = db.Sensors
                .Where(m => projectTypes.Contains(m.ProjectName))
                .ToList();
           
            ViewBag.ProjectName = projectName.ToString();           
            
            return View(sensorList);
        }        
    }
}
