using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineBusBookingNew.EF;

namespace OnlineBusBookingNew
{
    [Authorize]
    public class UsersController : Controller
    {
        private OnlineBusDBEntities db = new OnlineBusDBEntities();

        // GET: Users
        public ActionResult Index()
        {
            if(!String.IsNullOrEmpty(Convert.ToString(Session["UserLoggedType"])))
            {
                string type = Convert.ToString(Session["UserLoggedType"]);
                if(! (type == "1" || type == "2") )
                {
                    return Redirect("/Account/Error403");
                }
            }
            else
            {
                return Redirect("/Account/Login");
            }

            return View(db.User.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(long? id)
        {
            if (!String.IsNullOrEmpty(Convert.ToString(Session["UserLoggedType"])))
            {
                string type = Convert.ToString(Session["UserLoggedType"]);
                if (!(type == "1" || type == "2"))
                {
                    return Redirect("/Account/Error403");
                }
            }
            else
            {
                return Redirect("/Account/Login");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            if (!String.IsNullOrEmpty(Convert.ToString(Session["UserLoggedType"])))
            {
                string type = Convert.ToString(Session["UserLoggedType"]);
                if (!(type == "1" || type == "2"))
                {
                    return Redirect("/Account/Error403");
                }
            }
            else
            {
                return Redirect("/Account/Login");
            }
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,UserName,Password,FullName,BirthDate,Address,Phone,Type")] User user)
        {
            if (!String.IsNullOrEmpty(Convert.ToString(Session["UserLoggedType"])))
            {
                string type = Convert.ToString(Session["UserLoggedType"]);
                if (!(type == "1" || type == "2"))
                {
                    return Redirect("/Account/Error403");
                }
            }
            else
            {
                return Redirect("/Account/Login");
            }

            if (ModelState.IsValid)
            {
                db.User.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }


        // GET: Users/Edit/5
        public ActionResult Edit(long? id)
        {
            if (!String.IsNullOrEmpty(Convert.ToString(Session["UserLoggedType"])))
            {
                string type = Convert.ToString(Session["UserLoggedType"]);
                if (!(type == "1" || type == "2"))
                {
                    return Redirect("/Account/Error403");
                }
            }
            else
            {
                return Redirect("/Account/Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,UserName,Password,FullName,BirthDate,Address,Phone,Type")] User user)
        {
            if (!String.IsNullOrEmpty(Convert.ToString(Session["UserLoggedType"])))
            {
                string type = Convert.ToString(Session["UserLoggedType"]);
                if (!(type == "1" || type == "2"))
                {
                    return Redirect("/Account/Error403");
                }
            }
            else
            {
                return Redirect("/Account/Login");
            }
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(long? id)
        {
            if (!String.IsNullOrEmpty(Convert.ToString(Session["UserLoggedType"])))
            {
                string type = Convert.ToString(Session["UserLoggedType"]);
                if (!(type == "1" || type == "2"))
                {
                    return Redirect("/Account/Error403");
                }
            }
            else
            {
                return Redirect("/Account/Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            User user = db.User.Find(id);
            db.User.Remove(user);
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
