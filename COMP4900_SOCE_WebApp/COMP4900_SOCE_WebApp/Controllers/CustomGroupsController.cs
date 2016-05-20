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
using Microsoft.AspNet.Identity.EntityFramework;

namespace COMP4900_SOCE_WebApp.Controllers
{
    public class CustomGroupsController : Controller
    {
        private SensorContext db = new SensorContext();
        private ApplicationDbContext db2 = new ApplicationDbContext();

        // GET: CustomGroups
        [Authorize (Roles = "Admin, Supervisor")]
        public ActionResult Index()
        {
            var userStore = new UserStore<ApplicationUser>(db2);
            var userManager = new UserManager<ApplicationUser>(userStore);
            var currentUser = userManager.FindById(User.Identity.GetUserId()).UserName.ToString();
            
            string passed = "TestGroup1";
            List<string> customGroup = new List<string>();
            var query = db.CustomGroups.Where(m => m.CustomGroupName == passed).ToList();
                
            foreach (var q in query)
            {
                customGroup.Add(q.SensorName.ToString());
            }

            ViewBag.User = currentUser;
            ViewBag.Test = customGroup;

            return View(db.CustomGroups.ToList());
        }

        // GET: CustomGroups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomGroup customGroup = db.CustomGroups.Find(id);
            if (customGroup == null)
            {
                return HttpNotFound();
            }
            return View(customGroup);
        }

        // GET: CustomGroups/Create
        public ActionResult Create(int id)
        {
            ViewBag.ProjectId = id;

            CustomGroup cg = new CustomGroup();
            var projectName = db.Projects
                .Where(m => m.ProjectId == id)
                .Select(m => m.ProjectName).FirstOrDefault();
            cg.ProjectName = projectName;

            var user = db.Projects
                .Where(m => m.ProjectName == projectName)
                .Select(m => m.UserName)
                .FirstOrDefault().ToString();
                
            cg.UserName = user;

            var sensorsInProject = db.SensorProjects
                .Where(m => m.SensorProjectName == projectName)
                .Select(m => m.SensorName)
                .ToList();

            ViewBag.SensorList = new SelectList(sensorsInProject);

            //ViewBag.SensorList = new SelectList(sensorsInProject, "SensorName", "SensorName");
            return View(cg);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserName,CustomGroupName,ProjectName")] CustomGroup customGroup, string SelectedSensor)
        {
            var projectId = db.Projects
               .Where(m => m.ProjectName == customGroup.ProjectName)
               .Select(m => m.ProjectId).FirstOrDefault();
            ViewBag.ProjectId = projectId;

            var projectName = db.Projects
                .Where(m => m.ProjectId == projectId)
                .Select(m => m.ProjectName)
                .FirstOrDefault();

            var sensorsInProject = db.SensorProjects
                .Where(m => m.SensorProjectName == projectName)
                .Select(m => m.SensorName)
                .ToList();

            ViewBag.SensorList = new SelectList(sensorsInProject);

            if (ModelState.IsValid)
            {
                customGroup.SensorName = SelectedSensor;
                db.CustomGroups.Add(customGroup);
                db.SaveChanges();
                return RedirectToAction("Details", "Projects", new { id = projectId });
            }

            return View(customGroup);
        }

        // GET: CustomGroups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomGroup customGroup = db.CustomGroups.Find(id);
            if (customGroup == null)
            {
                return HttpNotFound();
            }
            return View(customGroup);
        }

        // POST: CustomGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomGroupId,StudentId,CustomGroupName,ProjectName,SensorName")] CustomGroup customGroup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customGroup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customGroup);
        }

        // GET: CustomGroups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomGroup customGroup = db.CustomGroups.Find(id);

            var projectName = db.CustomGroups
                .Where(m => m.CustomGroupId == id)
                .Select(m => m.ProjectName)
                .FirstOrDefault().ToString();

            var projectId = db.Projects
                .Where(m => m.ProjectName == projectName)
                .Select(m => m.ProjectId)
                .FirstOrDefault().ToString();

            ViewBag.ProjectId = projectId;

            if (customGroup == null)
            {
                return HttpNotFound();
            }
            return View(customGroup);
        }

        // POST: CustomGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CustomGroup customGroup = db.CustomGroups.Find(id);

            var projectName = db.CustomGroups
                .Where(m => m.CustomGroupId == id)
                .Select(m => m.ProjectName)
                .FirstOrDefault().ToString();

            var projectId = db.Projects
                .Where(m => m.ProjectName == projectName)
                .Select(m => m.ProjectId)
                .FirstOrDefault().ToString();

            ViewBag.ProjectId = projectId;
            ViewBag.CustGroupName = customGroup.CustomGroupName;

            db.CustomGroups.Remove(customGroup);
            db.SaveChanges();
            //return RedirectToAction("Index");
            return RedirectToAction("Details", "Projects", new { id = projectId});
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
