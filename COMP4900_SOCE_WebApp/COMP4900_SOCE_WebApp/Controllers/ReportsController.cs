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
using Microsoft.AspNet.Identity;

namespace COMP4900_SOCE_WebApp.Controllers
{
    public class ReportsController : Controller
    {
        private SensorContext db = new SensorContext();
        private ApplicationDbContext db2 = new ApplicationDbContext();
        
        // GET: Reports
        public ActionResult Reports(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }

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

            //code that checks if person is allowed to check this specific report
            var currentUser = User.Identity.GetUserName();
            var currentRole = User.IsInRole("Admin") || User.IsInRole("Supervisor");
            var projUser = db.Projects
                .Where(m => m.ProjectName == projectName)
                .Select(m => m.UserName)
                .FirstOrDefault();

            if (currentUser != projUser || currentRole)
            {
                return View("../Security/Unauthorized");
            }

            return View(sensorList);
        }

        [HttpPost]
        public ActionResult FilterReports(string cgName)
        {
            if (cgName == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

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

