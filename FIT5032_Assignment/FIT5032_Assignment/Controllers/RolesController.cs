using FIT5032_Assignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FIT5032_Assignment.Controllers
{
    public class RolesController :Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        // GET: /Roles/Index
        //[Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            if(User.IsInRole("Admin"))
            {
                var roles = context.Roles.ToList();
                return View(roles);
            }
            else
            {
                return HttpNotFound();
            }
        }

        //GET: /Roles/Create
        public ActionResult Create()
        {
            return View();
        }

        //POST: /Roles/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                context.Roles.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole()
                {
                    Name = collection["RoleName"]
                });
                context.SaveChanges();
                ViewBag.ResultMessage = "Role created successfully !";
                return RedirectToAction("Index");
            }
            catch
            {
                return View(); 
            }
        }

        //GET: /Roles/Edit
        public ActionResult Edit(string roleName)
        {
            var thisRole = context.Roles.Where(r => r.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            return View(thisRole);
        }

        //POST: /Roles/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Microsoft.AspNet.Identity.EntityFramework.IdentityRole role)
        {
            try
            {
                context.Entry(role).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // DELETE
        public ActionResult Delete(string RoleName)
        {
            var thisRole = context.Roles.Where(role => role.Name.Equals(RoleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            context.Roles.Remove(thisRole);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}