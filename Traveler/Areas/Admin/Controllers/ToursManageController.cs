using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Traveler.Models;

namespace Traveler.Areas.Admin.Controllers
{
    public class ToursManageController : Controller
    {
        private TravelBookingEntities db = new TravelBookingEntities();

        // GET: Admin/ToursManage
        public ActionResult Index()
        {
            var tours = db.Tours.Include(t => t.DepartureLocation).Include(t => t.Destination).Include(t => t.TourGuide);
            return View(tours.ToList());
        }

        // GET: Admin/ToursManage/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tour tour = db.Tours.Find(id);
            if (tour == null)
            {
                return HttpNotFound();
            }
            return View(tour);
        }

        // GET: Admin/ToursManage/Create
        public ActionResult Create()
        {
            ViewBag.DepartureLocationID = new SelectList(db.DepartureLocations, "ID", "DepartureName");
            ViewBag.DestinationID = new SelectList(db.Destinations, "ID", "DestinationName");
            ViewBag.TourGuideID = new SelectList(db.TourGuides, "ID", "TourGuideName");
            return View();
        }

        // POST: Admin/ToursManage/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Tour tour, HttpPostedFileBase Image1, HttpPostedFileBase Image2, HttpPostedFileBase Image3)
        {
            if (ModelState.IsValid)
            {
                DateTime parseDateTime;
                if (DateTime.TryParse(tour.DepartureDay.ToString(), out parseDateTime) && DateTime.TryParse(tour.EndDay.ToString(), out parseDateTime))
                {
                    if (Image1 != null)
                    {
                        var fileName = Path.GetFileName(Image1.FileName);
                        var path = Path.Combine(Server.MapPath("~/Content/img"), fileName);
                        tour.Image1 = fileName;
                        Image1.SaveAs(path);
                    }

                    if (Image2 != null)
                    {
                        var fileName = Path.GetFileName(Image2.FileName);
                        var path = Path.Combine(Server.MapPath("~/Content/img"), fileName);
                        tour.Image2 = fileName;
                        Image2.SaveAs(path);
                    }

                    if (Image3 != null)
                    {
                        var fileName = Path.GetFileName(Image3.FileName);
                        var path = Path.Combine(Server.MapPath("~/Content/img"), fileName);
                        tour.Image3 = fileName;
                        Image3.SaveAs(path);

                    }

                    db.Tours.Add(tour);

                    db.SaveChanges();
                }
                
                return RedirectToAction("Index");
            }

            ViewBag.DepartureLocationID = new SelectList(db.DepartureLocations, "ID", "DepartureName", tour.DepartureLocationID);
            ViewBag.DestinationID = new SelectList(db.Destinations, "ID", "DestinationName", tour.DestinationID);
            ViewBag.TourGuideID = new SelectList(db.TourGuides, "ID", "TourGuideName", tour.TourGuideID);
            return View(tour);
        }

        // GET: Admin/ToursManage/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tour tour = db.Tours.Find(id);
            if (tour == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartureLocationID = new SelectList(db.DepartureLocations, "ID", "DepartureName", tour.DepartureLocationID);
            ViewBag.DestinationID = new SelectList(db.Destinations, "ID", "DestinationName", tour.DestinationID);
            ViewBag.TourGuideID = new SelectList(db.TourGuides, "ID", "TourGuideName", tour.TourGuideID);

            ViewBag.Schedule = db.Schedules.Where(s => s.TourID == id).ToList();
            return View(tour);
        }

        // POST: Admin/ToursManage/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TourName,DestinationID,SlotNumber,DepartureDay,EndDay,DepartureLocationID,Price,Hotel,ViewNumber,GatherPlace,TourGuideID,Image1,Image2,Image3,TourDescription")] Tour tour)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tour).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartureLocationID = new SelectList(db.DepartureLocations, "ID", "DepartureName", tour.DepartureLocationID);
            ViewBag.DestinationID = new SelectList(db.Destinations, "ID", "DestinationName", tour.DestinationID);
            ViewBag.TourGuideID = new SelectList(db.TourGuides, "ID", "TourGuideName", tour.TourGuideID);
            return View(tour);
        }

        // GET: Admin/ToursManage/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tour tour = db.Tours.Find(id);
            if (tour == null)
            {
                return HttpNotFound();
            }
            return View(tour);
        }

        // POST: Admin/ToursManage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tour tour = db.Tours.Find(id);
            db.Tours.Remove(tour);
            db.SaveChanges();
            return RedirectToAction("Index");
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
