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
    public class SeatsController : Controller
    {
        private OnlineBusDBEntities db = new OnlineBusDBEntities();

        // GET: Seats
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
            var seat = db.Seat.Include(s => s.Bus);
            return View(seat.ToList());
        }

        // GET: Seats/Details/5
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
            Seat seat = db.Seat.Find(id);
            if (seat == null)
            {
                return HttpNotFound();
            }
            return View(seat);
        }

        // GET: Seats/Create
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
            ViewBag.BusID = new SelectList(db.Bus, "BusID", "BusName");
            return View();
        }

        // POST: Seats/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SeatID,SeatNumber,BusID,Status")] Seat seat)
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
                db.Seat.Add(seat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BusID = new SelectList(db.Bus, "BusID", "BusName", seat.BusID);
            return View(seat);
        }

        // GET: Seats/Edit/5
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
            Seat seat = db.Seat.Find(id);
            if (seat == null)
            {
                return HttpNotFound();
            }
            ViewBag.BusID = new SelectList(db.Bus, "BusID", "BusName", seat.BusID);
            return View(seat);
        }

        // POST: Seats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SeatID,SeatNumber,BusID,Status")] Seat seat)
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
                db.Entry(seat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BusID = new SelectList(db.Bus, "BusID", "BusName", seat.BusID);
            return View(seat);
        }

        // GET: Seats/Delete/5
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
            Seat seat = db.Seat.Find(id);
            if (seat == null)
            {
                return HttpNotFound();
            }
            return View(seat);
        }

        // POST: Seats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Seat seat = db.Seat.Find(id);
            db.Seat.Remove(seat);
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
