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
using SensorDataModel.Models;
using System.Threading.Tasks;
using System.Collections;
using SensorDataModel.Extensions;
using System.Diagnostics;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;

namespace COMP4900_SOCE_WebApp.Controllers
{
    public class UsersController : Controller
    {
        //public UserManager<ApplicationUser> UserManager { get; private set; }
        private ApplicationDbContext db = new ApplicationDbContext();
        
        //Get: Roles
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "UserName,Email,fName,lName,PasswordHash")] ApplicationUser user, string roleName)
        {
            var roles = db.Roles.ToList();
            List<string> roleList = new List<string>();
            foreach (var role in roles)
            {
                roleList.Add(role.Name);
            }
            ViewBag.RoleDropdownlist = new SelectList(roleList);

            if (user.PasswordHash != null)
            {
                if (user.PasswordHash.Length < 6)
                {
                    ViewBag.PassLength = "Password must be at least 6 characters";
                    return View(user);
                }
            }
            else
            {
                ViewBag.PassLength = "Password must be at least 6 characters";
                return View(user);
            }

            if (ModelState.IsValid)
            {
                var roleStore = new RoleStore<IdentityRole>(db);
                var userStore = new UserStore<ApplicationUser>(db);
                var userManager = new UserManager<ApplicationUser>(userStore);
                var newUserMake = userManager.FindByName(user.UserName);
                //var checkUser = db.Users.Find(user.UserName);
                if (newUserMake == null)
                {
                    var newUser = new ApplicationUser
                    {
                        UserName = user.UserName.ToUpper(),
                        Email = user.Email,
                        fName = user.fName,
                        lName = user.lName,
                        //PasswordHash = user.PasswordHash,
                        //LockoutEnabled = false,
                        //SecurityStamp = Guid.NewGuid().ToString(),
                        //AccessFailedCount = 1,
                        IsActive = true
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "UserName,Email,fName,lName,PasswordHash")] ApplicationUser user, bool isActive)
        {
            if (ModelState.IsValid)
            {
                using (var contxt = new ApplicationDbContext())
                {
                    bool saved;
                    var edit = contxt.Users
                        .Where(m => m.UserName == user.UserName)
                        .FirstOrDefault();
                    edit.fName = user.fName;
                    edit.lName = user.lName;
                    edit.IsActive = isActive;
                    var hash = new PasswordHasher();
                    edit.PasswordHash = hash.HashPassword(user.PasswordHash);
                    edit.Email = user.Email;
                    edit.UserName = user.UserName;
                    do
                    {
                        saved = false;
                        try
                        {
                            //db.Users.Add(user);
                            //contxt.Entry(user).State = EntityState.Modified;
                            contxt.SaveChanges();
                        }
                        catch (DbUpdateConcurrencyException ex)
                        {
                            saved = true;
                            var entries = ex.Entries.Single();
                            entries.OriginalValues.SetValues(entries.GetDatabaseValues());
                            //entries.OriginalValues.SetValues(entries.GetDatabaseValues());
                        }
                    } while (saved);
                }




                //db.Users.Attach(user);
                //user.IsActive = isActive;
                //db.Entry(user).State = EntityState.Modified;
                //ctx.Entry(user).Property(i => i.UserName).IsModified = true;
                //ctx.Entry(user).Property(i => i.Email).IsModified = true;
                //ctx.Entry(user).Property(i => i.fName).IsModified = true;
                //ctx.Entry(user).Property(i => i.lName).IsModified = true;
                //ctx.Entry(user).Property(i => i.PasswordHash).IsModified = true;
                //ctx.Entry(user).Property(i => i.IsActive).IsModified = true;
                //db.SaveChanges();



                //ApplicationUser u = UserManager.FindById(user.id);
                //db.Entry(user).State = EntityState.Modified;
                //db.Entry(user).Property(i => i.UserName).IsModified = true;
                //db.Entry(user).Property(i => i.Email).IsModified = true;
                //db.Entry(user).Property(i => i.fName).IsModified = true;
                //db.Entry(user).Property(i => i.lName).IsModified = true;
                //db.Entry(user).Property(i => i.PasswordHash).IsModified = true;
                //db.Entry(user).Property(i => i.IsActive).IsModified = true;
                //db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Options/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(string id)
        {
            var user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //var user = db.Users.Find(id);

            return View(user);
        }

        // POST: Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(string id)
        {
            //var user = db.Roles.Find(id);
            //db.Roles.Remove(user);
            var userActive = db.Users.Find(id);
            userActive.IsActive = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Roles/Edit/5
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public ActionResult RoleAddToUser(string userName, string roleName)
        {
            var user = db.Users.Where(u => u.UserName == userName).FirstOrDefault();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            userManager.AddToRole(userManager.FindByEmail(user.Email).Id, roleName);
            TempData["Message"] = "- " + roleName + " role added to user " + userName + " successfully !";

            return RedirectToAction("ManageUserRoles");
        }

        // POST: Users/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult GetRoles(string userName)
        {
            var user = db.Users.Where(u => u.UserName == userName).FirstOrDefault();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            TempData["UserRoles"] = userManager.GetRoles(user.Id);

            return RedirectToAction("ManageUserRoles");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteRoleForUser(string userName, string roleName)
        {
            if (userName == "A00111111" && roleName == "Admin")
            {
                TempData["Message"] = "- " + roleName + " role cannot be removed from user " + userName;
                return RedirectToAction("ManageUserRoles");
            }

            var user = db.Users.Where(u => u.UserName == userName).FirstOrDefault();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            if (userManager.IsInRole(user.Id, roleName))
            {
                userManager.RemoveFromRole(userManager.FindByEmail(user.Email).Id, roleName);
                TempData["Message"] = "- " + roleName + " role removed from user " + userName + " successfully !";
            }
            else
            {
                TempData["Message"] = "This user doesn't belong to selected role.";
            }

            return RedirectToAction("ManageUserRoles");
        }

        [Authorize(Roles = "Admin")]
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