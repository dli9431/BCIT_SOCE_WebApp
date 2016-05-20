using COMP4900_SOCE_WebApp.Models;
using SensorDataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace COMP4900_SOCE_WebApp.Controllers
{
    public class ExportController : Controller
    {
        private SensorContext db = new SensorContext();
        private ApplicationDbContext db2 = new ApplicationDbContext();

        // GET: Export
        public ActionResult Index()
        {
            return View();
        }
    }
}