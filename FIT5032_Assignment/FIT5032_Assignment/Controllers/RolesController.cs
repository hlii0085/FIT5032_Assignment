using FIT5032_Assignment.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        //public class EmailProcessing
        //{
        //    private const String API_KEY = "";

        //    public void Send(String toEmail, String subject, String contents)
        //    {
        //        var client = new SendGridClient(API_KEY);
        //        var from = new EmailAddress("lihaoxing0827@gmail.com", "HAOXING LI EMAIL");
        //        var to = new EmailAddress(toEmail, "");
        //        var plainTextContent = contents;
        //        var htmlContent = "<p>" + contents + "</p>";
        //        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        //        var response = client.SendEmailAsync(msg);
        //    }
        //}

        //EmailProcessing emailHandling = new EmailProcessing();
        //string emailSubject = "You have Successfully Booking " + model.hospitalName;
        //string emailBody = "<div><h2> Hospital Name: </h2><span> " + model.hospitalName + " </span></div>"
        //                    + "<div><h2> Booking Time: </h2><span> " + model.bookingTime + " </span></div>";
        //emailHandling.Send(user.Identity.GetUserName(), emailSubject, emailBody);

    }
}