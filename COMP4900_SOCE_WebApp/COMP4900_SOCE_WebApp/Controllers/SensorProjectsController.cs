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

namespace COMP4900_SOCE_WebApp.Controllers
{
    public class SensorProjectsController : Controller
    {
        private SensorContext db = new SensorContext();

        // GET: SensorProjects
        [Authorize(Roles = "Admin, Supervisor")]
        public async Task<ActionResult> Index()
        {
            return View(await db.SensorProjects.ToListAsync());
        }

        //******************************************************************************



        // GET: Projects/AssignUsersToSensors/5
        [Authorize(Roles = "Admin, Supervisor")]
        public ActionResult AssignSensorsToProjects(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Project project = db.Projects.Find(id);
            var projectName = project.ProjectName;

            //Project project = db.SensorProjects.Find(id);

            if (project == null)
            {
                return HttpNotFound();
            }
            return View();
        }

        // POST: Projects/AssignUsersToProjects/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Supervisor")]
        public ActionResult AssignSensorsToProjects(Project project)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(project);
        }



        //******************************************************************************


        // GET: SensorProjects/Details/5
        [Authorize(Roles = "Admin, Supervisor")]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SensorProject sensorProject = await db.SensorProjects.FindAsync(id);
            if (sensorProject == null)
            {
                return HttpNotFound();
            }
            return View(sensorProject);
        }

        // GET: SensorProjects/Create
        [Authorize(Roles = "Admin, Supervisor")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: SensorProjects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Supervisor")]
        public async Task<ActionResult> Create([Bind(Include = "SensorProjectId,SensorProjectName,SensorProjectType,SensorName")] SensorProject sensorProject)
        {
            if (ModelState.IsValid)
            {
                db.SensorProjects.Add(sensorProject);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(sensorProject);
        }

        // GET: SensorProjects/Edit/5
        [Authorize(Roles = "Admin, Supervisor")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SensorProject sensorProject = await db.SensorProjects.FindAsync(id);
            if (sensorProject == null)
            {
                return HttpNotFound();
            }
            return View(sensorProject);
        }

        // POST: SensorProjects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Supervisor")]
        public async Task<ActionResult> Edit([Bind(Include = "SensorProjectId,SensorProjectName,SensorProjectType,SensorName")] SensorProject sensorProject)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sensorProject).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(sensorProject);
        }

        // GET: SensorProjects/Delete/5
        [Authorize(Roles = "Admin, Supervisor")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SensorProject sensorProject = await db.SensorProjects.FindAsync(id);

            var projectName = db.SensorProjects
                .Where(m => m.SensorProjectId == id)
                .Select(m => m.SensorProjectName).FirstOrDefault();

            var projectId = db.Projects
                .Where(m => m.ProjectName == projectName)
                .Select(m => m.ProjectId).FirstOrDefault().ToString();

            ViewBag.ProjectId = projectId;

            if (sensorProject == null)
            {
                return HttpNotFound();
            }
            return View(sensorProject);
        }

        // POST: SensorProjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Supervisor")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var projectName = db.SensorProjects
                 .Where(m => m.SensorProjectId == id)
                 .Select(m => m.SensorProjectName).FirstOrDefault();

            var projectId = db.Projects
                .Where(m => m.ProjectName == projectName)
                .Select(m => m.ProjectId).FirstOrDefault();
            
            SensorProject sensorProject = await db.SensorProjects.FindAsync(id);
            db.SensorProjects.Remove(sensorProject);
            await db.SaveChangesAsync();
            return RedirectToAction("RemoveSensorsFromProjects", "Projects", new { id = projectId });
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
