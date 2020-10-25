using FIT5032_Assignment.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FIT5032_Assignment.Controllers
{
    public class UserRolesController:Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        private UserManager<ApplicationUser> UserManager;

        public UserRolesController()
        {
            var userStore = new UserStore<ApplicationUser>(context);
            UserManager = new UserManager<ApplicationUser>(userStore);
        }

        // GET: /Roles/Index
        public ActionResult Index()
        {
            return View();
        }

        //GET: /UserRoles/AddRoleToUser
        public ActionResult AddRoleToUser()
        {
            var list = context.Roles.OrderBy(r => r.Name).ToList().Select(role => new SelectListItem { Value = role.Name.ToString(), Text = role.Name }).ToList();
            ViewBag.Roles = list;
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddRoleToUser(string UserName, string RoleName)
        {
            try
            {
                ApplicationUser user = context.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                this.UserManager.AddToRole(user.Id, RoleName);
                ViewBag.ResultMessage = "Added successfully !";
                return RedirectToAction("AddRoleToUser");
            }
            catch
            {
                return View();
            }
        }

        //GET: /UserRoles/GetUserRoles
        public ActionResult GetUserRoles()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetUserRoles(string UserName)
        {
            if(!string.IsNullOrWhiteSpace(UserName))
            {
                ApplicationUser user = context.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                ViewBag.RolesForThisUser = this.UserManager.GetRoles(user.Id);
            }
            return View("GetUserRoles");
        }

        //GET: /DeleteUserRole
        [HttpGet]
        public ActionResult DeleteUserRole()
        {
            var roles = context.Roles.OrderBy(r => r.Name).ToList().Select(r => new SelectListItem { Value = r.Name.ToString(), Text = r.Name }).ToList();
            ViewBag.Roles = roles;
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteUserRole(string UserName, string RoleName)
        {
            ApplicationUser user = context.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            if(this.UserManager.IsInRole(user.Id, RoleName))
            {
                this.UserManager.RemoveFromRole(user.Id, RoleName);
            }

            return View("DeleteUserRole");
        }


    }
}