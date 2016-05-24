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
using System.Text;
using System.Globalization;

namespace COMP4900_SOCE_WebApp.Controllers
{
    public class ReportsController : Controller
    {
        private SensorContext db = new SensorContext();
        private ApplicationDbContext db2 = new ApplicationDbContext();
        string sensor1, sensor2, sensor3, sensor4, sensor5, sensor6;

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

            if (!currentRole)
            {
                if (currentUser != projUser)
                {
                    return View("../Security/Unauthorized");
                }
            }

            return View(sensorList);
        }

        // GET: Reports/SavedReports
        public ActionResult SavedReports(string repName)
        {
            SavedReport sr = new SavedReport();
            sr = db.SavedReports
                .Where(m => m.ReportName == repName)
                .FirstOrDefault();
            if (sr == null)
            {
                return HttpNotFound();
            }

            ViewBag.ProjectName = sr.ProjectName;
            ViewBag.ReportName = sr.ReportName;

            // get user's specific custom group from save report
            var custGroup = db.SavedReports
                .Where(m => m.ReportName == repName)
                .Select(m => m.CustomGroupName)
                .FirstOrDefault();
            
            ViewBag.CustomGroup = custGroup;

            ViewBag.begin = db.SavedReports
                .Where(m => m.ProjectName == sr.ProjectName)
                .Select(m => m.BeginDate)
                .FirstOrDefault();

            ViewBag.end = db.SavedReports
                .Where(m => m.ProjectName == sr.ProjectName)
                .Select(m => m.EndDate)
                .FirstOrDefault();

            // get all custom groups
            var custGroups = db.CustomGroups
                .Where(m => m.ProjectName == sr.ProjectName)
                .OrderBy(m => m.CustomGroupName)
                .Select(m => m.CustomGroupName)
                .Distinct()
                .ToList();
            ViewBag.CustomGroups = custGroups;

            //code that checks if person is allowed to check this specific report
            //var currentUser = User.Identity.GetUserName();
            //var currentRole = User.IsInRole("Admin") || User.IsInRole("Supervisor");
            //var projUser = db.Projects
            //    .Where(m => m.ProjectName == projectName)
            //    .Select(m => m.UserName)
            //    .FirstOrDefault();

            //if (!currentRole)
            //{
            //    if (currentUser != projUser)
            //    {
            //        return View("../Security/Unauthorized");
            //    }
            //}

            List<Sensor> selectedSensors = new List<Sensor>();
            List<Sensor> defaultSensors = new List<Sensor>();

            var sensors = db.CustomGroups
                .Where(m => m.CustomGroupName == custGroup)
                .Select(m => m.SensorName)
                .ToList();

            ViewData["SensorList"] = sensors;

            foreach (var i in sensors)
            {
                var sQuery = db.Sensors
                    .Where(m => m.SensorName == i)
                    .FirstOrDefault();
                selectedSensors.Add(sQuery);
            }

            return View(selectedSensors);
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

        [HttpPost]
        public ActionResult ExportReports (string cn)
        {
            List<double?> list = new List<double?>();

            DateTime begin = DateTime.ParseExact("04/22/16 12:10", "MM/dd/yy H:mm", CultureInfo.InvariantCulture);
            DateTime end = DateTime.ParseExact("04/22/16 12:15", "MM/dd/yy H:mm", CultureInfo.InvariantCulture);

            var csv = new StringBuilder();

            var snList = db.CustomGroups
                .Where(m => m.CustomGroupName == cn)
                .Select(m => m.SensorName)
                .ToList();

            //var customGroupSensors = (from x in db.CustomGroups                                    
            //                           where x.CustomGroupName == form
            //                          select x.SensorName).ToArray();

            //var distinctSensorNames = (from x in db.Sensors

            //                           where begin <= x.DateTime
            //                           where end >= x.DateTime
            //                           select x.SensorName).Distinct().ToArray();

            var distictDateTimes = (from x in db.Sensors
                                    where begin <= x.DateTime
                                    where end >= x.DateTime
                                    select x.DateTime).Distinct().ToArray();

            csv.Append("Sensor,");
            csv.Append(String.Join(",", snList) + "\n");

            for (int i = 0; i < distictDateTimes.Count(); i++)
            {
                var dateTimeTmp = distictDateTimes[i];

                for (int j = 0; j < snList.Count(); j++)
                {
                    var headerTmp = snList[j];

                    var sensorValues = (from x in db.Sensors
                                        where x.SensorName.Contains(headerTmp)
                                        where x.DateTime.Equals(dateTimeTmp)
                                        select x.SensorValue).FirstOrDefault();

                    list.Add(sensorValues);
                }
                csv.Append(dateTimeTmp.ToString("MM/dd/yy H:mm") + "," + String.Join(",", list));
                list.Clear();
                csv.Append("\n");
            }

            Random rng = new Random();
            int fileNum = rng.Next();
            string fileName = "D:/test" + fileNum + ".csv";
            //System.IO.File.WriteAllText("D:/test.csv", csv.ToString());
            System.IO.File.WriteAllText(fileName, csv.ToString());
            //return View();
            return View("Download");
        }




        // GET: Charts
        public ActionResult Report(string SensorList, DateTime? FDTS, DateTime? EDTS)
         {
            string[] s = SensorList.Split(',');

            for (var i = 0; i < s.Length; i++)
            {
                int x = i + 1;

                string sensorName = "sensor" + x;
                switch (sensorName)
                {
                    case "sensor1":
                        sensor1 = Convert.ToString(s[i]);
                        break;
                    case "sensor2":
                        sensor2 = Convert.ToString(s[i]);
                        break;
                    case "sensor3":
                        sensor3 = Convert.ToString(s[i]);
                        break;
                    case "sensor4":
                        sensor4 = Convert.ToString(s[i]);
                        break;
                    case "sensor5":
                        sensor5 = Convert.ToString(s[i]);
                        break;
                    case "sensor6":
                        sensor6 = Convert.ToString(s[i]);
                        break;
                    default:
                        break;
                }
            }

            return GetCharts(sensor1, sensor2, sensor3, sensor4, sensor5, sensor6, FDTS, EDTS);
        }


        public ActionResult GetCharts(string sensor1, string sensor2, string sensor3, string sensor4, string sensor5, string sensor6, DateTime? FDTS, DateTime? EDTS)
        {

            var chkData = (from c in db.Sensors
                           where c.SensorName == sensor1 || c.SensorName == sensor2
                               || c.SensorName == sensor3 || c.SensorName == sensor4
                               || c.SensorName == sensor5 || c.SensorName == sensor6
                               && c.DateTime >= FDTS
                               && c.DateTime <= EDTS
                           select new
                           {
                               c.SensorName,
                               c.SensorValue,
                               c.DateTime
                           }).OrderBy(c => c.DateTime);
            var List = chkData.ToList();

            return Json(List, JsonRequestBehavior.AllowGet);
        }

    }
}

