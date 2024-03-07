using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Traveler.Models;

namespace Traveler.Controllers
{
    public class UsersController : Controller
    {
        TravelBookingEntities db = new TravelBookingEntities();
        // GET: Users
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Customer cus)
        {
            if (ModelState.IsValid)
            {
                var adminAccount = db.AdminAccounts.FirstOrDefault(k => k.Email == cus.Email && k.Password == cus.Password);

                if (adminAccount != null)
                {
                    Session["Account"] = adminAccount;
                    return RedirectToAction("Index", "Admin/ToursManage");
                }
                var account = db.Customers.FirstOrDefault(k => k.Email == cus.Email && k.Password == cus.Password);
                if (account != null)
                {
                    Session["Account"] = account;
                    return RedirectToAction("Index", "Home");
                }
                else
                    ViewBag.ThongBao = "*Tên đăng nhập hoặc mật khẩu không đúng";
            }
            return View();
        }


        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(FormCollection customer)
        {
            if (customer["Password"] != customer["rePassword"])
            {
                @ViewBag.Notification = "Mật khẩu xác nhận không chính xác";
                return View();
            }
            else
            {
                string email = customer["Email"].ToString();

                var cus = db.Customers.FirstOrDefault(k => k.Email == email);

                if (cus != null)
                {
                    ViewBag.Notification = "Đã có người đăng ký bằng email này";
                    return View();
                }
                string phoneNum = customer["PhoneNumber"].ToString();

                var cusByPhoneNum = db.Customers.FirstOrDefault(c => c.PhoneNumber == phoneNum);
                if (cusByPhoneNum != null)
                {
                    ViewBag.Notification = "Đã có người đăng ký bằng số điện thoại này";
                    return View();
                }


                else
                {
                    Customer accout = new Customer();
                    accout.Name = customer["UserName"];
                    accout.Email = customer["Email"];
                    accout.PhoneNumber = customer["PhoneNumber"];
                    accout.Password = customer["Password"];
                    accout.AvatarImage = "";

                    db.Customers.Add(accout);
                    db.SaveChanges();
                    return RedirectToAction("Login", "Users");
                }
            }
        }

        public ActionResult Logout()
        {
            Session["Account"] = null;
            return RedirectToAction("Login", "Users");
        }
    }
}