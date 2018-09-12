using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Security.Models;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MVC_Security.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        ///Alle Views en actions dat userbeheer nodig heeft
        /// </summary>
        /// <returns></returns>
        public ActionResult Userbeheer()
        {
            IDbSet<ApplicationUser> alleUsers = context.Users;
            return View(alleUsers);
        }

        public ActionResult VerwijderUser(string id)
        {
            var user = context.Users.Find(id);
            return View(user);
        }

        [HttpPost]
        public ActionResult VerwijderUserDoorvoeren(string id)
        {
            var user = context.Users.FirstOrDefault(u => u.Id == id);
            if(user != null)
            {
                context.Users.Remove(user);
                context.SaveChanges();
            }
            return RedirectToAction("Userbeheer");
        }

        public ActionResult UserDetail(string id)
        {
            var user = context.Users.Find(id);
            ViewBag.userid = id;
            ViewBag.usernaam = user.UserName;

            var rolesVoorUser = new List<IdentityRole>();
            foreach (var role in user.Roles)
            {
                 rolesVoorUser.Add(context.Roles.Find(role.RoleId));
            };
            return View(rolesVoorUser);

        }

        public ActionResult UserDetail2(string id)
        {
            var user = context.Users.Find(id);
            return View(user);
        }

        public ActionResult VerwijderRoleForMember(string userid, string roleid)
        {
            var user = context.Users.FirstOrDefault(u => u.Id == userid);
            var role = context.Roles.FirstOrDefault(r => r.Id == roleid);
            if(user != null && role != null)
            {
                IdentityUserRole userrole = user.Roles.SingleOrDefault(ur => (ur.UserId == userid && ur.RoleId == roleid));
                user.Roles.Remove(userrole);
                context.SaveChanges();
            }
            return RedirectToAction("UserDetail", "User", new { id = userid });
        }


        /// <summary>
        /// Alle Views en actions dat een Rolebeheer nodig heeft
        /// </summary>
        /// <returns></returns>
        public ActionResult Rolebeheer()
        {
            IDbSet<IdentityRole> alleRoles = context.Roles;
            return View(alleRoles);
        }

        public ActionResult VerwijderRole(string id)
        {
            var role = context.Roles.Find(id);
            return View(role);
        }

        [HttpPost]
        public ActionResult VerwijderRoleDoorvoeren(string id)
        {
            var role = context.Roles.FirstOrDefault(r => r.Id == id);
            if(role != null)
            {
                context.Roles.Remove(role);
                context.SaveChanges();
            }
            return RedirectToAction("Rolebeheer");
        }

        public ActionResult RoleDetail(string id)
        {
            var role = context.Roles.Find(id);
            RoleDetailViewModel vm = new RoleDetailViewModel();
            vm.RoleName = role.Name;
            vm.RoleId = role.Id;
            vm.UsersUitRole = new List<ApplicationUser>();
            foreach (var userrole in role.Users)
            {
                vm.UsersUitRole.Add(context.Users.Find(userrole.UserId));
            }
            vm.SelectUser = new List<SelectListItem>();
            foreach (var user in context.Users)
            {
                if(! vm.UsersUitRole.Contains(user))
                    vm.SelectUser.Add(new SelectListItem { Text = user.UserName, Value = user.Id });
            }
            return View(vm);
        }

        public ActionResult VerwijderMemberFromRole(string userid, string roleid)
        {
            var user = context.Users.FirstOrDefault(u => u.Id == userid);
            var role = context.Roles.FirstOrDefault(r => r.Id == roleid);

            if(user != null && role != null)
            {
                IdentityUserRole userrole = user.Roles.SingleOrDefault(ur => (ur.RoleId == roleid && ur.UserId == userid));
                user.Roles.Remove(userrole);
                context.SaveChanges();
            }
            return RedirectToAction("RoleDetail", "User", new { id = roleid });
        }

        [HttpPost]
        public ActionResult MemberToevoegen(string roleId, string selectUser)
        {
            var user = context.Users.FirstOrDefault(u => u.Id == selectUser);
            var role = context.Roles.FirstOrDefault(r => r.Id == roleId);
            if(user != null && role != null)
            {
                IdentityUserRole userrole = new IdentityUserRole();
                userrole.RoleId = roleId;
                userrole.UserId = selectUser;
                user.Roles.Add(userrole);
                context.SaveChanges();
            }
            return RedirectToAction("RoleDetail", "User", new { id = roleId });

        }

        // GET: User
        public ActionResult Index()
        {
            return View();
        }
    }
}