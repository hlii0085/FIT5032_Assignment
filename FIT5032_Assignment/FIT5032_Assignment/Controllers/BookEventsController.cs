using EllipticCurve;
using FIT5032_Assignment.Models;
using FIT5032_Assignment.Utils;
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
            var userId = User.Identity.GetUserId();
            var check = db.BookEvents.Where(be => be.BookingId == id && be.ApplicationUserId.Equals(userId)).FirstOrDefault();
            var booking = db.Bookings.Where(be => be.Id == id).FirstOrDefault();

            if(check == null)
            {
                var bookEvent = new BookEvent { ApplicationUserId = User.Identity.GetUserId(), BookingId = id };
                db.BookEvents.Add(bookEvent);
                db.SaveChanges();

                //Send email
                string toMail = User.Identity.GetUserName();
                string subject = "Hospital booking successfull";
                string content = "You have successfully booked an enquiry. The time is " + booking.Time.ToString();
                EmailSender es = new EmailSender();
                es.Send(toMail, subject, content);
            } else
            {
                ViewBag.Error = "You have already booked this hospital!";
            }            
            return View("Index");
        }

        public JsonResult Bar()
        {
            var bookings = db.BookEvents.Include("Booking").Include("Booking.Hospital").ToList();

            var bookingCount =
                from booking in bookings
                group booking by booking.Booking.Hospital into books
                select new
                {
                    Event = books.Key,
                    count = books.Count()
                };

            return new JsonResult { Data = bookingCount, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        
        }
    }
}