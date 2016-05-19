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
    public class ProjectsController : Controller
    {
        private SensorContext db = new SensorContext();
        private ApplicationDbContext db2 = new ApplicationDbContext();

        // GET: Projects
        public ActionResult Index()
        {
            return View(db.Projects.ToList());
        }

        // GET: Projects/Details/5
        public ActionResult Details(int? id)
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

            ViewBag.AssignedUser = "Assigned User";
            ViewBag.AssignedSensors = "Assigned Sensors";

            var userQuery = db.Projects
                .Where(m => m.ProjectId == id)
                .Select(m => m.UserName).FirstOrDefault().ToString();
            
            //pass user assigned to project to details view
            ViewBag.AssignedUserValue = userQuery;

            //query the project name based on passed in projectId
            var sensorQuery1 = db.Projects
                .Where(m => m.ProjectId == id)
                .Select(m => m.ProjectName).FirstOrDefault().ToString();

            //query the list of sensors based on projectName
            var sensorQuery2 = db.SensorProjects
                .Where(m => m.SensorProjectName == sensorQuery1)
                .Select(m => m.SensorName).ToList();
            List<string> sensorList = new List<string>();
            foreach (var q in sensorQuery2)
            {
                sensorList.Add(q.ToString());
            }

            //pass list of sensors assigned to project to details view
            ViewBag.SensorList = sensorList;

            return View(project);
        }

        // GET: Projects/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProjectId,ProjectName,ProjectDescription")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Projects.Add(project);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(project);
        }

        // GET: Projects/Edit/5
        public ActionResult Edit(int? id)
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
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProjectId,ProjectName,ProjectDescription")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(project);
        }

        // GET: Projects/AssignUsersToProjects/5
        public ActionResult AssignSensorsToProjects(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            GroupProject groupProject = new GroupProject();

            groupProject.Projects = (from b in db.Projects
                                     where b.ProjectId == id
                                     select b).FirstOrDefault();

            var checkProject = groupProject.Projects;
            checkProject = db.Projects.Find(id);

            if (checkProject == null)
            {
                return HttpNotFound();
            }
            return View(groupProject);
        }
        


        // POST: Projects/AssignUsersToProjects/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AssignSensorsToProjects([Bind(Include = "SensorProjectName, SensorProjectType, SensorName")] SensorProject sensorProject)
        {
            if (ModelState.IsValid)
            {       
                db.Entry(sensorProject).State = EntityState.Added;
                db.SaveChanges();
                return RedirectToAction("Index", "Projects");
            }
            return View(sensorProject);
        }

        // GET: Projects/AssignUsersToProjects/5
        public ActionResult AssignUsersToProjects(int? id)
        {
            var users = db2.Users.ToList();
            List<string> usernames = new List<string>();
            foreach (var user in users)
            {
                usernames.Add(user.UserName);
            }

            ViewBag.Users = new SelectList(usernames);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/AssignUsersToProjects/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AssignUsersToProjects(Project project)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(project);
        }

        // GET: Projects/RemoveSensorsFromProjects
        public ActionResult RemoveSensorsFromProjects (int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            //query for projectName
            var projectNameQ = db.Projects
                .Where(m => m.ProjectId == id)
                .Select(m => m.ProjectName)
                .FirstOrDefault();

            //send projectname to remove sensors view
            ViewBag.ProjectName = projectNameQ;

            //query for sensors in project
            var sensors = db.SensorProjects
                .Where(m => m.SensorProjectName == projectNameQ)
                .Select(m => m.SensorName)
                .ToList();

            List<string> sensorList = new List<string>();

            //add to list
            foreach (var q in sensors)
            {
                sensorList.Add(q.ToString());
            }

            //send list of sensors to remove sensor view
            ViewBag.SensorList = sensorList;

            if (projectNameQ == null)
            {
                return HttpNotFound();
            }
            return View(db.SensorProjects
                .Where(m => m.SensorProjectName == projectNameQ)
                .ToList());
        }


        // GET: Projects/Delete/5
        public ActionResult Delete(int? id)
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
            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
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
