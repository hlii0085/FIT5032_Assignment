using FIT5032_Assignment.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FIT5032_Assignment.Controllers
{
    public class BookEventsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BookEvents
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Customer")]
        public ActionResult Book(int id)
        {
            var bookEvent = new BookEvent { ApplicationUserId = User.Identity.GetUserId(), BookingId = id };
            db.BookEvents.Add(bookEvent);
            db.SaveChanges();
            return View("Index");
        }
    }
}