using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using OnlineBusBookingNew.EF;
using OnlineBusBooking.Common;

namespace OnlineBusBookingNew.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        // GET: Account
        public ActionResult Error403()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Models.MemberShip model)
        {
            using (var context = new OnlineBusDBEntities())
            {
                var enCryptPass = Encryptor.MD5Hash(model.Password);
                model.Password = enCryptPass;

                bool isValid = context.User.Any(x => x.UserName == model.UserName && x.Password == model.Password);
                if (isValid)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false);

                    var user = context.User.FirstOrDefault(x => x.UserName == model.UserName && x.Password == model.Password);

                    Session["UserLoggedId"] = user.UserID;
                    Session["UserLoggedName"] = user.UserName;
                    Session["UserLoggedType"] = user.Type;
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Invalid Username or Password");
                return View();
            }
               
        }

        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(User model)
        {
            
            using (var context = new OnlineBusDBEntities())
            {
                var enCryptPass = Encryptor.MD5Hash(model.Password);
                model.Password = enCryptPass;
                context.User.Add(model);
                context.SaveChanges();
            }
            return RedirectToAction("Login");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            Session["UserLoggedId"] = null;
            Session["UserLoggedName"] = null;
            Session["UserLoggedType"] = null;

            return RedirectToAction("Index", "Home");
        }

    }
}