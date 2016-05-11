using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Net;
using System.Data.Entity;
using COMP4900_SOCE_WebApp.Models;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using System.Collections;
using SensorDataModel.Extensions;
using System.Diagnostics;

namespace COMP4900_SOCE_WebApp.Controllers
{
    public class UsersController : Controller
    {
        //public UserManager<ApplicationUser> UserManager { get; private set; }

        private ApplicationDbContext db = new ApplicationDbContext();

        //Get: Roles
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Roles/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            var roles = db.Roles.ToList();
            List<string> roleList = new List<string>();
            foreach (var role in roles)
            {
                roleList.Add(role.Name);
            }
            ViewBag.RoleDropdownlist = new SelectList(roleList);
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserName,Email,EmailConfirmed,fName,lName,PasswordHash,LockoutEnabled,AccessFailedCount")] ApplicationUser user, string roleName)
        {
            if (ModelState.IsValid)
            {
                //ArrayList rolesList = new ArrayList();
                //rolesList.Add("Student");
                //rolesList.Add("Supervisor");
                //rolesList.Add("Admin");

                var roleStore = new RoleStore<IdentityRole>(db);
                var userStore = new UserStore<ApplicationUser>(db);
                var userManager = new UserManager<ApplicationUser>(userStore);

                var newUserMake = userManager.FindByName(user.UserName);
                if (newUserMake == null)
                {
                    var newUser = new ApplicationUser
                    {
                        UserName = user.UserName,
                        Email = user.Email,
                        EmailConfirmed = user.EmailConfirmed,
                        fName = user.fName,
                        lName = user.lName,
                        //PasswordHash = user.PasswordHash,
                        LockoutEnabled = user.LockoutEnabled,
                        SecurityStamp = Guid.NewGuid().ToString(),
                        AccessFailedCount = user.AccessFailedCount
                    };
                    var result = userManager.Create(newUser, user.PasswordHash);
                    if (result.Succeeded)
                    {
                        userManager.SetLockoutEnabled(newUser.Id, false);
                        userManager.AddToRole(newUser.Id, roleName);
                    }
                    else
                    {
                        Debug.WriteLine("Cant create user");
                    }
                    //db.Users.Add(newUser);
                    //db.Users.Add(newUser);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }

            return View(user);
        }

        // GET: Roles/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Roles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ApplicationUser user)
        {
            if (ModelState.IsValid)
            {
                //ApplicationUser u = UserManager.FindById(user.id);
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Options/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = db.Roles.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var user = db.Roles.Find(id);
            db.Roles.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Roles/Edit/5

        public ActionResult LockOut(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Roles/LockOut/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LockOut(ApplicationUser user)
        {
            if (ModelState.IsValid)
            {
                if (user.LockoutEnabled == true)
                {
                    user.LockoutEndDateUtc = DateTime.UtcNow.AddYears(2);
                }
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }
        // GET: Users/ManageUserRoles

        public ActionResult ManageUserRoles()
        {
            var users = db.Users.ToList();
            List<string> usernames = new List<string>();
            foreach (var user in users)
            {
                usernames.Add(user.UserName);
            }

            var roles = db.Roles.ToList();
            List<string> valid = new List<string>();
            foreach (var role in roles)
            {
                valid.Add(role.Name);
            }

            ViewBag.UserRoles = TempData["UserRoles"];
            ViewBag.Message = TempData["Message"];
            ViewBag.UserDropdownlist = new SelectList(usernames);
            ViewBag.RoleDropdownlist = new SelectList(valid);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RoleAddToUser(string userName, string roleName)
        {
            var user = db.Users.Where(u => u.UserName == userName).FirstOrDefault();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            userManager.AddToRole(userManager.FindByEmail(user.Email).Id, roleName);
            TempData["Message"] = "- " + roleName + " role added to user " + userName + "successfully !";

            return RedirectToAction("ManageUserRoles");
        }

        // POST: Users/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetRoles(string userName)
        {
            var user = db.Users.Where(u => u.UserName == userName).FirstOrDefault();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            TempData["UserRoles"] = userManager.GetRoles(user.Id);

            return RedirectToAction("ManageUserRoles");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRoleForUser(string userName, string roleName)
        {
            if (userName == "A00111111" && roleName == "Admin")
            {
                TempData["Message"] = "- " + roleName + " role cannot removed from user " + userName;
                return RedirectToAction("ManageUserRoles");
            }

            var user = db.Users.Where(u => u.UserName == userName).FirstOrDefault();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            if (userManager.IsInRole(user.Id, roleName))
            {
                userManager.RemoveFromRole(userManager.FindByEmail(user.Email).Id, roleName);
                TempData["Message"] = "- " + roleName + " role removed from user " + userName + "successfully !";
            }
            else
            {
                TempData["Message"] = "This user doesn't belong to selected role.";
            }

            return RedirectToAction("ManageUserRoles");
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