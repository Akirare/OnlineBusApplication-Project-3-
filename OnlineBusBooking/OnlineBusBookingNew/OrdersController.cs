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
    public class OrdersController : Controller
    {
        private OnlineBusDBEntities db = new OnlineBusDBEntities();

        // GET: Orders
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
            var order = db.Order.Include(o => o.User);
            return View(order.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
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
            ViewBag.UserID = new SelectList(db.User, "UserID", "UserName");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderID,OrderDate,UserID,OrderStatus,RefundAmount")] Order order)
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
                db.Order.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.User, "UserID", "UserName", order.UserID);
            return View(order);
        }

        // GET: Orders/Edit/5
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
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.User, "UserID", "UserName", order.UserID);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderID,OrderDate,UserID,OrderStatus,RefundAmount")] Order order)
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
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.User, "UserID", "UserName", order.UserID);
            return View(order);
        }

        // GET: Orders/Delete/5
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
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Order order = db.Order.Find(id);
            db.Order.Remove(order);
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
