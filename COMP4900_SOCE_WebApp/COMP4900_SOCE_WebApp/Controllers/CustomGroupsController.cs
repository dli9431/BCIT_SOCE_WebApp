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
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "CustomGroupId,StudentId,CustomGroupName,ProjectName,SensorName")] CustomGroup customGroup)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.CustomGroups.Add(customGroup);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(customGroup);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentId,CustomGroupName,ProjectName,SensorName")] CustomGroup customGroup)
        {
            if (ModelState.IsValid)
            {
                db.CustomGroups.Add(customGroup);
                db.SaveChanges();
                return RedirectToAction("Index");
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
            db.CustomGroups.Remove(customGroup);
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
