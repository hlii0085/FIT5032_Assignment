﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FIT5032_Assignment.Models;
using FIT5032_Assignment.Utils;

namespace FIT5032_Assignment.Controllers
{
    public class BookingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Bookings
        public ActionResult Index()
        {
            var bookings = db.Bookings.Include(b => b.Hospital);
            if (User.IsInRole("Admin"))
            {
                return View("Index", bookings.ToList());
            }
            else
            {
                return View("CustomerBooking", bookings.ToList());
            }

        }

        
        //public ActionResult Index()
        //{
        //    var bookings = db.Bookings.Include(b => b.Hospital);
        //    return View(bookings.ToList());
        //}

        // GET: Bookings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // GET: Bookings/Create
        public ActionResult Create()
        {
            ViewBag.HospitalId = new SelectList(db.Hospitals, "Id", "HospitalName");
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,HospitalId,Time")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Bookings.Add(booking);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.HospitalId = new SelectList(db.Hospitals, "Id", "HospitalName", booking.HospitalId);
            return View(booking);
        }

        // GET: Bookings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            ViewBag.HospitalId = new SelectList(db.Hospitals, "Id", "HospitalName", booking.HospitalId);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,HospitalId,Time")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                var bookH = db.BookEvents.Include(be => be.ApplicationUser).Where(be => be.Booking.Id == booking.Id).ToList();
                List<string> emails = new List<string>();
                for(int i = 0; i < bookH.Count; i++)
                {
                    emails.Add(bookH[i].ApplicationUser.Email);
                }
                string subject = "Booking change";
                string content = "The time change to " + booking.Time.ToString();
                EmailSender es = new EmailSender();
                es.SendBulk(emails, subject, content);

                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HospitalId = new SelectList(db.Hospitals, "Id", "HospitalName", booking.HospitalId);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Booking booking = db.Bookings.Find(id);
            db.Bookings.Remove(booking);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //GET BOOKING/ID
        public ActionResult BookingByHospital(int id)
        {
            ViewBag.hospital = db.Hospitals.FirstOrDefault(b => b.Id == id);
            var bookings = db.Bookings.Include(be => be.Hospital).Where(be => be.HospitalId == id).ToList();
            return View(bookings);
        }

        //GET BOOKING/ID
        public JsonResult GetBookingsByHospital(int id)
        {
            var bookings = db.Bookings.Include(be => be.Hospital).Where(be => be.HospitalId == id).ToList();
            return new JsonResult { Data = bookings, JsonRequestBehavior = JsonRequestBehavior.AllowGet};
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
