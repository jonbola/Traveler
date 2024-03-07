using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Traveler.Models;

namespace Traveler.Controllers
{
    public class ToursController : Controller
    {
        TravelBookingEntities db = new TravelBookingEntities();
        // GET: Tours
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Detail(int id)
        {
            var tour = db.Tours.FirstOrDefault(t => t.ID == id);
            ViewBag.Schedule = db.Schedules.Where(sch => sch.TourID == id).ToList();
            return View(tour);
        }


        public ActionResult ResetBooking(int? idTour)
        {

            Session["AdultCusList"] = null;
            Session["ChildCusList"] = null;

            this.AddAdultCus(idTour);
            return RedirectToAction("BookingTour", "Tours", new { idTour = idTour });

        }

        [HttpGet]
        public ActionResult BookingTour(int? idTour)
        {
            if (idTour == null)
            {
                
                return RedirectToAction("Index"); 
            }

            
            var tour = db.Tours.FirstOrDefault(t => t.ID == idTour);

            if (tour == null)
            {
                
                return HttpNotFound(); 
            }


            ViewBag.LstAdultCus = GetAdultCusList();
            ViewBag.LstChildCus = GetChildCusList();
            ViewBag.Tour = tour;
            ViewBag.ChildTicketPrice = tour.Price * 0.7;
            ViewBag.FinalPrice = this.GetFinalPrice();
            ViewBag.FinalSlot = this.GetFinalSlot();
            ViewBag.ThongBao = "";

            return View();
        }
        [HttpPost]
        public ActionResult BookingTour(FormCollection form)
        {
            string contactName = form["ContactName"].ToString().Trim();
            string contactPhoneNumber = form["ContactPhoneNumber"].ToString().Trim();
            string note = form["Note"].ToString().Trim();
            int idTour = int.Parse(form["IdTour"]);
            int idUser = int.Parse(form["IdUser"]);
            List<AdultInfor> adultsLst = this.GetAdultCusList();
            List<ChildrenInfor> childsLst = this.GetChildCusList();

            var tour = db.Tours.FirstOrDefault(t => t.ID == idTour);

            if (this.GetFinalSlot() > tour.SlotNumber)
            {
                ViewBag.ThongBao = "Số lượng chỗ ngồi không đủ đáp ứng";
                ViewBag.LstAdultCus = GetAdultCusList();
                ViewBag.LstChildCus = GetChildCusList();
                ViewBag.Tour = tour;
                ViewBag.ChildTicketPrice = tour.Price * 0.7;
                ViewBag.FinalPrice = this.GetFinalPrice();
                ViewBag.FinalSlot = this.GetFinalSlot();
                return View();
            }

            Book book = new Book();
            book.TourID = idTour;
            book.CustomerID = idUser;
            book.BookingDay = DateTime.Now;
            book.Status = "Chưa thanh toán";
            book.SlotNumber = this.GetFinalSlot();
            book.Note = note;
            book.ContactPhoneNumber = contactPhoneNumber;
            book.ContactName = contactName;
            db.Books.Add(book);


            foreach(var item in adultsLst)
            {
                TravelerInformation travelerInformation = new TravelerInformation();
                travelerInformation.IDBooking = book.ID;
                travelerInformation.CodeTicket = item.CodeTicket;
                travelerInformation.Gender = item.Gender;
                travelerInformation.PassengerAge = 0;
                db.TravelerInformations.Add(travelerInformation);
            }

            foreach (var item in childsLst)
            {
                TravelerInformation travelerInformation = new TravelerInformation();
                travelerInformation.IDBooking = book.ID;
                travelerInformation.CodeTicket = item.CodeTicket;
                travelerInformation.Gender = item.Gender;
                travelerInformation.PassengerAge = 1;
                db.TravelerInformations.Add(travelerInformation);
            }

            tour.SlotNumber -= this.GetFinalSlot();

            Session["AdultCusList"] = null;
            Session["ChildCusList"] = null;
            db.SaveChanges();


            return RedirectToAction("ThankYouPage","Tours");
        }

        public ActionResult ThankYouPage()
        {
            return View();
        }

        //list custommer
        public List<AdultInfor> GetAdultCusList()
        {
            List<AdultInfor> adultCusList = Session["AdultCusList"] as List<AdultInfor>;
            if (adultCusList == null)
            {
                adultCusList = new List<AdultInfor>();
                Session["AdultCusList"] = adultCusList;

            }
            return adultCusList;
        }

        public List<ChildrenInfor> GetChildCusList()
        {
            List<ChildrenInfor> childCusList = Session["ChildCusList"] as List<ChildrenInfor>;


            if (childCusList == null)
            {
                childCusList = new List<ChildrenInfor>();
                Session["ChildCusList"] = childCusList;
            }
            return childCusList;
        }

        
        //add cus
        public ActionResult AddAdultCus(int? idTour)
        {
            List<AdultInfor> adultsLst = this.GetAdultCusList();
            double priceTicket = (double)db.Tours.FirstOrDefault(t => t.ID == idTour).Price;
            AdultInfor adultCus = new AdultInfor();
            adultCus.PriceTicket = priceTicket;
            adultsLst.Add(adultCus);

            return RedirectToAction("BookingTour","Tours", new {idTour = idTour});
        }

        public ActionResult AddChildCus(int? idTour)
        {
            List<ChildrenInfor> childsLst = this.GetChildCusList();
            double priceTicket = (double)db.Tours.FirstOrDefault(t => t.ID == idTour).Price;

            ChildrenInfor childCus = new ChildrenInfor();
            childCus.PriceTicket = priceTicket*0.7;

            childsLst.Add(childCus);

            return RedirectToAction("BookingTour", "Tours", new { idTour = idTour });
        }

        //minute
        public ActionResult MinuteAdultCus(int? idTour)
        {
            List<AdultInfor> adultsLst = this.GetAdultCusList();  

            adultsLst.RemoveAt(adultsLst.Count - 1);

            return RedirectToAction("BookingTour", "Tours", new { idTour = idTour });
        }

        public ActionResult MinuteChildCus(int? idTour)
        {
            List<ChildrenInfor> childsLst = this.GetChildCusList();

            childsLst.RemoveAt(childsLst.Count - 1);

            return RedirectToAction("BookingTour", "Tours", new { idTour = idTour });
        }

        private double GetFinalPrice()
        {
            double price = 0;
            List<AdultInfor> adultsLst = this.GetAdultCusList();
            List<ChildrenInfor> childsLst = this.GetChildCusList();

            foreach (var item in adultsLst)
            {
                price += item.PriceTicket;
            }

            if(childsLst.Count > 0)
            {
                foreach (var item in childsLst)
                {
                    price += item.PriceTicket; 
                }
            }

            return price;
        }

        private int GetFinalSlot()
        {
            return this.GetAdultCusList().Count + this.GetChildCusList().Count;
        }
    }
}