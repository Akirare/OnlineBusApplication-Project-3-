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
    public class RoutingsController : Controller
    {
        private OnlineBusDBEntities db = new OnlineBusDBEntities();

        // GET: Routings
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
            var routing = db.Routing.Include(r => r.Bus);
            return View(routing.ToList());
        }

        // GET: Routings/Details/5
        public ActionResult Details(int? id)
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
            Routing routing = db.Routing.Find(id);
            if (routing == null)
            {
                return HttpNotFound();
            }
            return View(routing);
        }

        // GET: Routings/Create
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

        // POST: Routings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RoutingID,BusID,StartPlace,EndPlace,StartDate,Price")] Routing routing)
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
                db.Routing.Add(routing);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BusID = new SelectList(db.Bus, "BusID", "BusName", routing.BusID);
            return View(routing);
        }

        // GET: Routings/Edit/5
        public ActionResult Edit(int? id)
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
            Routing routing = db.Routing.Find(id);
            if (routing == null)
            {
                return HttpNotFound();
            }
            ViewBag.BusID = new SelectList(db.Bus, "BusID", "BusName", routing.BusID);
            return View(routing);
        }

        // POST: Routings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RoutingID,BusID,StartPlace,EndPlace,StartDate,Price")] Routing routing)
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
                db.Entry(routing).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BusID = new SelectList(db.Bus, "BusID", "BusName", routing.BusID);
            return View(routing);
        }

        // GET: Routings/Delete/5
        public ActionResult Delete(int? id)
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
            Routing routing = db.Routing.Find(id);
            if (routing == null)
            {
                return HttpNotFound();
            }
            return View(routing);
        }

        // POST: Routings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Routing routing = db.Routing.Find(id);
            db.Routing.Remove(routing);
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
