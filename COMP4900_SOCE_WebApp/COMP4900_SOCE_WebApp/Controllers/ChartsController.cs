using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SensorDataModel.Models;
using System.Collections;
using System.Globalization;
using System.Diagnostics;
using SensorDataModel;

namespace COMP4900_SOCE_WebApp.Controllers
{
    public class ChartsController : Controller
    {
        private SensorContext db = new SensorContext();

        public ActionResult DateTime()
        {
            return View();
        }
        // GET: Charts
        public ActionResult Index()
        {
            var queries = db.Sensors.ToList();

            List<SelectListItem> sensorNamesList = new List<SelectListItem>();

            foreach (var query in queries)
            {
                sensorNamesList.Add(new SelectListItem()
                {
                    Text = query.SensorName,
                    Value = query.SensorName
                });
            }

            ViewData["sensor1"] = sensorNamesList;
            ViewData["sensor2"] = sensorNamesList;
            ViewData["sensor3"] = sensorNamesList;
            ViewData["sensor4"] = sensorNamesList;
            ViewData["sensor5"] = sensorNamesList;
            ViewData["sensor6"] = sensorNamesList;
            return View();
        }
        string sensor1, sensor2, sensor3, sensor4, sensor5, sensor6;

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
            //  Debug.Write("begin date: ");
            //FDTS = new DateTime(2016, 4, 22, 19, 27, 15);
            //EDTS = new DateTime(2016, 4, 30, 19, 27, 15);
            // var FfDTS = DateTime.ParseExact("04/22/2016 12:10", "MM/dd/yy H:mm", CultureInfo.InvariantCulture);

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