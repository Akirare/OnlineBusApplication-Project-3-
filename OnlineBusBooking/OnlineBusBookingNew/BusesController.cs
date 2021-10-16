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
    public class BusesController : Controller
    {
        private OnlineBusDBEntities db = new OnlineBusDBEntities();

        // GET: Buses
        public ActionResult Index()
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
            return View(db.Bus.ToList());
        }

        // GET: Buses/Details/5
        public ActionResult Details(string id)
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
            Bus bus = db.Bus.Find(id);
            if (bus == null)
            {
                return HttpNotFound();
            }
            return View(bus);
        }

        // GET: Buses/Create
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

        // POST: Buses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BusID,BusName,Type,Seats")] Bus bus)
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
                db.Bus.Add(bus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bus);
        }

        // GET: Buses/Edit/5
        public ActionResult Edit(string id)
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
            Bus bus = db.Bus.Find(id);
            if (bus == null)
            {
                return HttpNotFound();
            }
            return View(bus);
        }

        // POST: Buses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BusID,BusName,Type,Seats")] Bus bus)
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
                db.Entry(bus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bus);
        }

        // GET: Buses/Delete/5
        public ActionResult Delete(string id)
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
            Bus bus = db.Bus.Find(id);
            if (bus == null)
            {
                return HttpNotFound();
            }
            return View(bus);
        }

        // POST: Buses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Bus bus = db.Bus.Find(id);
            db.Bus.Remove(bus);
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
