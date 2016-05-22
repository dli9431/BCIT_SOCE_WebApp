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
            ViewBag.ProjectId = id;

            var custGroups = db.CustomGroups
                .Where(m => m.ProjectName == projectName)
                .OrderBy(m => m.CustomGroupName)
                .Select(m => m.CustomGroupName)
                .Distinct()
                .ToList();

            ViewBag.CustomGroups = custGroups;
            
            return View(sensorList);
        }

        [HttpPost]
        public ActionResult FilterReports(string cgName)
        {
            List<Sensor> selectedSensors = new List<Sensor>();
            List<Sensor> defaultSensors = new List<Sensor>();
            
            var cgQuery = db.CustomGroups
                .Where(m => m.CustomGroupName == cgName)
                .Select(m => m.SensorName)
                .ToList();

            foreach (var i in cgQuery)
            {
                var sQuery = db.Sensors
                    .Where(m => m.SensorName == i)
                    .FirstOrDefault();
                selectedSensors.Add(sQuery);
            }
            
            if (Request.IsAjaxRequest())
            {
                return PartialView("_PartialReport", selectedSensors);
            }

            return PartialView(defaultSensors);
            //return View(sensorList);
        }     
        
    }
}

