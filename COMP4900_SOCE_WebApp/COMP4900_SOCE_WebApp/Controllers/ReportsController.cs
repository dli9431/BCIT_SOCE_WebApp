using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SensorDataModel.Models;

namespace COMP4900_SOCE_WebApp.Controllers
{
    public class ReportsController : Controller
    {
        private SensorContext db = new SensorContext();

        // GET: Reports
        public ActionResult Index()
        {
            return View(db.SensorProjects.ToList());
        }

        // GET: Reports/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SensorProject sensorProject = db.SensorProjects.Find(id);
            if (sensorProject == null)
            {
                return HttpNotFound();
            }
            return View(sensorProject);
        }

        // GET: Reports/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Reports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SensorProjectId,SensorProjectName,SensorProjectType,SensorName")] SensorProject sensorProject)
        {
            if (ModelState.IsValid)
            {
                db.SensorProjects.Add(sensorProject);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sensorProject);
        }

        // GET: Reports/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SensorProject sensorProject = db.SensorProjects.Find(id);
            if (sensorProject == null)
            {
                return HttpNotFound();
            }
            return View(sensorProject);
        }

        // POST: Reports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SensorProjectId,SensorProjectName,SensorProjectType,SensorName")] SensorProject sensorProject)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sensorProject).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sensorProject);
        }

        // GET: Reports/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SensorProject sensorProject = db.SensorProjects.Find(id);
            if (sensorProject == null)
            {
                return HttpNotFound();
            }
            return View(sensorProject);
        }

        // POST: Reports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SensorProject sensorProject = db.SensorProjects.Find(id);
            db.SensorProjects.Remove(sensorProject);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

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
