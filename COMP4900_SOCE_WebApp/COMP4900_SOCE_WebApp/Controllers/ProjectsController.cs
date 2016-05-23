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
using PagedList;

namespace COMP4900_SOCE_WebApp.Controllers
{
    public class ProjectsController : Controller
    {
        private SensorContext db = new SensorContext();
        private ApplicationDbContext db2 = new ApplicationDbContext();

        // GET: Projects
        [Authorize(Roles = "Admin, Supervisor, Student")]
        public ActionResult Index()
        {

            var userStore = new UserStore<ApplicationUser>(db2);
            var userManager = new UserManager<ApplicationUser>(userStore);

            //gets current application UserName ex: A00111111
            var user = userManager.FindByName(User.Identity.GetUserName());

            //gets all projects of the current user
            var currentProjects = db.Projects
                .Where(m => m.UserName == user.UserName)
                .ToList();

            //get the current user role id
            var currentRole = User.IsInRole("Admin") || User.IsInRole("Supervisor");

            if (currentRole == false)
            {
                return View(currentProjects);
            }
            else
            {
                return View(db.Projects.ToList());
            }
            //return View(db.Projects.ToList());
        }

        // GET: Projects/Details/5
        [Authorize(Roles = "Admin, Supervisor, Student")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            var projName = project.ProjectName;
            //var userQuery = db.Projects
            //    .Where(m => m.ProjectId == id)
            //    .Select(m => m.UserName).FirstOrDefault().ToString();

            //pass user assigned to project to details view
            ViewBag.AssignedUserValue = project.UserName;

            //query the project name based on passed in projectId
            //var sensorQuery1 = db.Projects
            //    .Where(m => m.ProjectId == id)
            //    .Select(m => m.ProjectName).FirstOrDefault().ToString();

            //query the list of sensors based on projectName
            var sensorQuery2 = db.SensorProjects
                .Where(m => m.SensorProjectName == projName)
                .Select(m => m.SensorName)
                .ToList();

            List<string> sensorList = new List<string>();

            foreach (var q in sensorQuery2)
            {
                sensorList.Add(q);
            }

            //pass list of sensors assigned to project to details view
            ViewBag.SensorList = sensorList;

            //get + pass list of customgroups from within the project
            var custGroup = db.CustomGroups
                .Where(m => m.ProjectName == projName)
                .Where(m => m.UserName == project.UserName)
                .ToList();


            ViewBag.CustomGroupModel = new CustomGroup();
            ViewBag.CustomGroupValues = custGroup;

            return View(project);
        }

        // GET: Projects/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            var allUsers = db2.Users
                .Select(m => m.UserName)
                .ToList();
            ViewBag.UserList = new SelectList(allUsers);

            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "ProjectId,ProjectName,ProjectDescription")] Project project, string SelectedUser)
        {
            var allUsers = db2.Users
                .Select(m => m.UserName)
                .ToList();
            ViewBag.UserList = new SelectList(allUsers);

            if (ModelState.IsValid)
            {
                project.UserName = SelectedUser;
                db.Projects.Add(project);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(project);
        }

        // GET: Projects/Edit/5
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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

        [HttpPost]
        public PartialViewResult SearchSensors(string keyword)
        {
            // System.Threading.Thread.Sleep(2000);
            var sensors = from s in db.Sensors
                          select s;
            var data = db.Sensors.Where(f => f.SensorName.Contains(keyword)).ToList();

            ViewBag.SensorList = data;
            return PartialView(sensors);
        }

        // GET: Projects/AssignUsersToProjects/5
        [Authorize(Roles = "Admin")]
        public ActionResult AssignSensorsToProjects(int? id, string sortOrder)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.ProjectSortParm = sortOrder == "ProjectName" ? "project_desc" : "ProjectName";

            var sensors = (from s in db.Sensors
                           select s).Take(25);
            switch (sortOrder)
            {
                case "name_desc":
                    sensors = sensors.OrderByDescending(s => s.SensorName);
                    break;
                case "project_desc":
                    sensors = sensors.OrderByDescending(s => s.ProjectName);
                    break;
                case "ProjectName":
                    sensors = sensors.OrderBy(s => s.ProjectName);
                    break;
                default:
                    sensors = sensors.OrderBy(s => s.SensorName);
                    break;
            }

            //return View(students.ToList());

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.projectId = id;
            ViewBag.projectDesc = db.Projects
                .Where(m => m.ProjectId == id)
                .Select(m => m.ProjectDescription)
                .FirstOrDefault();

            var checkProject = db.Projects.Find(id);

            if (checkProject == null)
            {
                return HttpNotFound();
            }
            SensorProject sensorProject = new SensorProject();

            //get project name based on id
            var projectName = db.Projects
                .Where(m => m.ProjectId == id)
                .Select(m => m.ProjectName).FirstOrDefault();

            sensorProject.SensorProjectName = projectName;

            //make list of project types
            //faster than querying entire db for all unique project types
            List<string> types = new List<string>();
            types.Add("hpws");
            types.Add("hpws_rp");
            types.Add("mh_north");
            types.Add("mh_south");
            types.Add("th_int");
            types.Add("th_ext");
            types.Add("th_ps");
            types.Add("gvs_south");
            types.Add("gvs_north");

            ViewBag.ProjectList = new SelectList(types);
            ViewBag.SensorList = sensors.ToList();
            return View(sensorProject);
            //return View(sensors.ToList());
        }

        // GET Projects/AssignGroup
        [Authorize(Roles = "Admin")]
        public ActionResult AssignGroup(int id)
        {
            var project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            var projName = project.ProjectName;
            ViewBag.projectId = id;

            List<string> types = new List<string>();
            types.Add("hpws");
            types.Add("hpws_rp");
            types.Add("mh_north");
            types.Add("mh_south");
            types.Add("th_int");
            types.Add("th_ext");
            types.Add("th_ps");
            types.Add("gvs_south");
            types.Add("gvs_north");

            ViewBag.ProjectTypes = types;
            SensorProject sp = new SensorProject();
            sp.SensorProjectId = id;
            sp.SensorProjectName = projName;

            return View(sp);
        }


        // POST: Projects/AssignGroup
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult ConfirmGroup(int id, string projType)
        {
            List<string> types = new List<string>();
            types.Add("hpws");
            types.Add("hpws_rp");
            types.Add("mh_north");
            types.Add("mh_south");
            types.Add("th_int");
            types.Add("th_ext");
            types.Add("th_ps");
            types.Add("gvs_south");
            types.Add("gvs_north");

            ViewBag.ProjectTypes = types;

            var project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            var projName = project.ProjectName;
            var sensors = db.Sensors
                .Where(m => m.ProjectName == projType)
                .Select(m => m.SensorName)
                .ToList();

            if (sensors == null)
            {
                return HttpNotFound();
            }

            var checkExisting = db.SensorProjects
                .Where(m => m.SensorProjectName == projName)
                .Select(m => m.SensorName)
                .ToList();
            foreach (var i in sensors)
            {
                if (!checkExisting.Contains(i))
                {
                    SensorProject sp = new SensorProject();
                    sp.SensorProjectName = projName;
                    sp.SensorProjectType = projType;
                    sp.SensorName = i;

                    db.Entry(sp).State = EntityState.Added;
                    db.SaveChanges();
                }
                else
                {
                    ViewBag.Duplicates = "Cannot assign duplicates to project";
                    return RedirectToAction("AssignGroup", "Projects", new { id = id });
                }

            }
            return RedirectToAction("Details", "Projects", new {id = id});
        }

        // GET Projects/RemoveGroup
        [Authorize(Roles = "Admin")]
        public ActionResult RemoveGroup(int id)
        {
            var project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            var projName = project.ProjectName;
            ViewBag.projectId = id;

            List<string> types = new List<string>();
            types.Add("hpws");
            types.Add("hpws_rp");
            types.Add("mh_north");
            types.Add("mh_south");
            types.Add("th_int");
            types.Add("th_ext");
            types.Add("th_ps");
            types.Add("gvs_south");
            types.Add("gvs_north");

            ViewBag.ProjectTypes = types;
            SensorProject sp = new SensorProject();
            //sp.SensorProjectId = id;
            //sp.SensorProjectName = projName;

            return View(sp);
        }


        // POST: Projects/ConfirmRemove
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult ConfirmRemove(int id, string projType)
        {
            List<string> types = new List<string>();
            types.Add("hpws");
            types.Add("hpws_rp");
            types.Add("mh_north");
            types.Add("mh_south");
            types.Add("th_int");
            types.Add("th_ext");
            types.Add("th_ps");
            types.Add("gvs_south");
            types.Add("gvs_north");

            ViewBag.ProjectTypes = types;

            var project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            var projName = project.ProjectName;
            var sensors = db.Sensors
                .Where(m => m.ProjectName == projType)
                .Select(m => m.SensorName)
                .ToList();

            if (sensors == null)
            {
                return HttpNotFound();
            }

            var checkExisting = db.SensorProjects
                .Where(m => m.SensorProjectName == projName)
                .Select(m => m.SensorName)
                .ToList();

            foreach (var i in sensors)
            {
                if (checkExisting.Contains(i))
                {
                    var find = db.SensorProjects
                        .Where(m => m.SensorName == i)
                        .FirstOrDefault();
                    db.SensorProjects.Remove(find);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Details", "Projects", new { id = id });
        }

        // POST: Projects/AssignSensorsToProjects/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult AssignSensorsToProjects([Bind(Include = "SensorProjectName, SensorProjectType, SensorName")] SensorProject sensorProject, string SelectedType)
        {
            List<string> types = new List<string>();
            types.Add("hpws");
            types.Add("hpws_rp");
            types.Add("mh_north");
            types.Add("mh_south");
            types.Add("th_int");
            types.Add("th_ext");
            types.Add("th_ps");
            types.Add("gvs_south");
            types.Add("gvs_north");

            ViewBag.ProjectList = new SelectList(types);

            var projectId = db.Projects
                   .Where(m => m.ProjectName == sensorProject.SensorProjectName)
                   .Select(m => m.ProjectId).FirstOrDefault();

            ViewBag.projectDesc = db.Projects
                .Where(m => m.ProjectId == projectId)
                .Select(m => m.ProjectDescription)
                .FirstOrDefault();

            var projectName = db.Projects
                .Where(m => m.ProjectId == projectId)
                .Select(m => m.ProjectName).FirstOrDefault();
            sensorProject.SensorProjectName = projectName;
            sensorProject.SensorProjectType = SelectedType;

            var checkExisting = db.SensorProjects
                .Where(m => m.SensorName == sensorProject.SensorName)
                .FirstOrDefault();

            if (checkExisting != null)
            {
                ViewBag.Duplicates = "You have already added that sensor to the project";
                return View(sensorProject);
            }

            if (ModelState.IsValid)
            {
                db.Entry(sensorProject).State = EntityState.Added;
                db.SaveChanges();
                return RedirectToAction("Details", "Projects", new { id = projectId });
            }
            return View(sensorProject);
        }

        // GET: Projects/AssignUsersToProjects/5
        [Authorize(Roles = "Admin")]
        public ActionResult AssignUsersToProjects(int? id)
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

            var users = db2.Users.ToList();
            List<string> usernames = new List<string>();
            foreach (var user in users)
            {
                usernames.Add(user.UserName);
            }

            ViewBag.Users = new SelectList(usernames);
            ViewBag.ProjectId = id;


            return View(project);
        }

        // POST: Projects/AssignUsersToProjects/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult AssignUsersToProjects(Project project)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Projects", new { id = project.ProjectId });
            }
            return View(project);
        }

        // GET: Projects/RemoveSensorsFromProjects
        [Authorize(Roles = "Admin")]
        public ActionResult RemoveSensorsFromProjects(int? id)
        {
            ViewBag.projectId = id;

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
                sensorList.Add(q);
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
        [Authorize(Roles = "Admin, Supervisor")]
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
        [Authorize(Roles = "Admin")]
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
