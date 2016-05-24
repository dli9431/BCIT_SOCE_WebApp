using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SensorDataModel.Models;
using COMP4900_SOCE_WebApp.Models;
using Microsoft.AspNet.Identity;

namespace COMP4900_SOCE_WebApp.Controllers
{
    public class SavedReportsController : Controller
    {
        private SensorContext db = new SensorContext();
        private ApplicationDbContext db2 = new ApplicationDbContext();


        // GET SavedReports/Index
        public ActionResult Index(string UserName)
        {
            //return all saved reports for admin/supervisor
            var currentRole = User.IsInRole("Admin") || User.IsInRole("Supervisor");
            if (currentRole)
            {
                return View(db.SavedReports.ToList());
            }

            //return saved report for specific user
            var checkProjects = db.Projects
                .Where(m => m.UserName == UserName)
                .ToList();

            List<SavedReport> saved = new List<SavedReport>();
            foreach (var i in checkProjects)
            {
                var checkReports = db.SavedReports
                    .Where(m => m.ProjectName == i.ProjectName.ToString())
                    .FirstOrDefault();
                saved.Add(checkReports);
            }
            return View(saved);
        }

        // POST SavedReports/Save
        [HttpPost]
        public ActionResult Save(int id, string cn, string name)
        {
            SavedReport newReport = new SavedReport();
            var projName = db.Projects
                .Where(m => m.ProjectId == id)
                .FirstOrDefault();
            newReport.ProjectName = projName.ProjectName;
            newReport.CustomGroupName = cn;
            newReport.ReportName = name;
            //newReport.BeginDate = sDate;
            //newReport.EndDate = eDate;
            db.SavedReports.Add(newReport);
            db.SaveChanges();
            return RedirectToAction("Reports", "Reports", new { id = id });
        }


        //// GET: SavedReports
        //public async Task<ActionResult> Index()
        //{
        //    return View(await db.SavedReports.ToListAsync());
        //}

        //// GET: SavedReports/Details/5
        //public async Task<ActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    SavedReport savedReport = await db.SavedReports.FindAsync(id);
        //    if (savedReport == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(savedReport);
        //}

        //// GET: SavedReports/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: SavedReports/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Create([Bind(Include = "SavedReportId,ReportName,ProjectName,CustomGroupName,BeginDate,EndDate")] SavedReport savedReport)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.SavedReports.Add(savedReport);
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }

        //    return View(savedReport);
        //}

        //// GET: SavedReports/Edit/5
        //public async Task<ActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    SavedReport savedReport = await db.SavedReports.FindAsync(id);
        //    if (savedReport == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(savedReport);
        //}

        //// POST: SavedReports/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit([Bind(Include = "SavedReportId,ReportName,ProjectName,CustomGroupName,BeginDate,EndDate")] SavedReport savedReport)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(savedReport).State = EntityState.Modified;
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    return View(savedReport);
        //}

        //// GET: SavedReports/Delete/5
        //public async Task<ActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    SavedReport savedReport = await db.SavedReports.FindAsync(id);
        //    if (savedReport == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(savedReport);
        //}

        //// POST: SavedReports/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteConfirmed(int id)
        //{
        //    SavedReport savedReport = await db.SavedReports.FindAsync(id);
        //    db.SavedReports.Remove(savedReport);
        //    await db.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
