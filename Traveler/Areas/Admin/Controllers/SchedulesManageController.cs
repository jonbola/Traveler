using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Traveler.Models;

namespace Traveler.Areas.Admin.Controllers
{
    public class SchedulesManageController : Controller
    {
        private TravelBookingEntities db = new TravelBookingEntities();

        // GET: Admin/SchedulesManage
        public ActionResult Index()
        {
            var schedules = db.Schedules.Include(s => s.Tour);
            return View(schedules.ToList());
        }

        // GET: Admin/SchedulesManage/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Schedule schedule = db.Schedules.Find(id);
            if (schedule == null)
            {
                return HttpNotFound();
            }
            return View(schedule);
        }

        // GET: Admin/SchedulesManage/Create
        public ActionResult Create(int? idTour)
        {
            ViewBag.Tour = db.Tours.FirstOrDefault(t => t.ID == idTour);

            ViewBag.TourID = new SelectList(db.Tours, "ID", "TourName");
            return View();
        }

        // POST: Admin/SchedulesManage/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TourID,ScheduleName,ScheduleDetail")] Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                db.Schedules.Add(schedule);
                db.SaveChanges();
                return RedirectToAction("Edit", "ToursManage", new { id = schedule.TourID });

            }

            ViewBag.TourID = new SelectList(db.Tours, "ID", "TourName", schedule.TourID);
            return View(schedule);
        }

        // GET: Admin/SchedulesManage/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Schedule schedule = db.Schedules.Find(id);
            if (schedule == null)
            {
                return HttpNotFound();
            }
            ViewBag.TourID = new SelectList(db.Tours, "ID", "TourName", schedule.TourID);
            return View(schedule);
        }

        // POST: Admin/SchedulesManage/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TourID,ScheduleName,ScheduleDetail")] Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(schedule).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit","ToursManage", new {id = schedule.TourID});
            }
            ViewBag.TourID = new SelectList(db.Tours, "ID", "TourName", schedule.TourID);
            return View(schedule);
        }

        // GET: Admin/SchedulesManage/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Schedule schedule = db.Schedules.Find(id);
            if (schedule == null)
            {
                return HttpNotFound();
            }
            return View(schedule);
        }

        // POST: Admin/SchedulesManage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Schedule schedule = db.Schedules.Find(id);
            int? idTour = schedule.TourID;
            db.Schedules.Remove(schedule);
            db.SaveChanges();
            return RedirectToAction("Edit", "ToursManage", new { id = idTour });

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
