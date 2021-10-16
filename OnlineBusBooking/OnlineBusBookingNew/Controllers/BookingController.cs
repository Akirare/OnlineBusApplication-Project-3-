using OnlineBusBookingNew.EF;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.Entity;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineBusBookingNew.Models;

namespace OnlineBusBookingNew.Controllers
{
    [Authorize]
    public class BookingController : Controller
    {
        private OnlineBusDBEntities db = new OnlineBusDBEntities();

        // GET: Booking
        public ActionResult Index()
        {

            var routing = db.Routing.Include(r => r.Bus);

            return View(routing.ToList());
        }

        // GET: Booking/Details/5
        public ActionResult Details(int id)
        {

            return View();
        }

        // GET: Booking/Create
        public ActionResult Create(int id)
        {

            int routingId = id;
            Routing objRouting = db.Routing.FirstOrDefault(p => p.RoutingID == routingId);
            if (objRouting != null)
            {
                var lstSeat = db.Seat.Where(p => p.BusID == objRouting.BusID).ToList(); //SEAT LIST
                ViewBag.ListSeat = lstSeat;

                var bus = db.Bus.FirstOrDefault(p => p.BusID == objRouting.BusID);
                ViewBag.BusName = bus.BusName;
                ViewBag.RoutingId = routingId;
                ViewBag.BusId = bus.BusID;

                //ViewBag.BusID = new SelectList(db.Bus, "BusID", "BusName");
                //ViewBag.SeatNumber = new SelectList(lstSeat, "BusID", "SeatNumber"); 
            }
            return View();
        }

        // POST: Booking/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {


            try
            {
                

                // TODO: Add insert logic here
                var order = new Order();
                order.OrderDate = DateTime.Now;
                order.OrderStatus = 1; //1: BOOKED; 0: CANCEL
                order.UserID = Convert.ToInt32(Session["UserLoggedId"]);
                order.RefundAmount = 0; //cANCEL  day > 2 : 100% , 2 > day > 1 : 85% , day = 1 : 70%

                var orderDetail = new OrderDetail();
                orderDetail.Order = order;

                var birthdateUserOrder = (DateTime)db.User.FirstOrDefault(d => d.UserID == order.UserID).BirthDate;
                int yearUserOrder = birthdateUserOrder.Year;
                int yearNow = DateTime.Now.Year;

                var busID = Convert.ToString(collection["BusId"]);
                var routingID = Convert.ToInt32(collection["RoutingId"]);
                var price = (Decimal)db.Routing.FirstOrDefault(p => p.RoutingID == routingID).Price;

                var lstSeat = db.Seat.Where(p => p.BusID == busID).ToList(); //SEAT LIST
                ViewBag.ListSeat = lstSeat;

                if ((yearNow - yearUserOrder) < 5)
                {
                    orderDetail.PriceCalculated = 0;
                }
                else if ((yearNow - yearUserOrder) >= 5 && (yearNow - yearUserOrder) <= 12)
                {
                    orderDetail.PriceCalculated = price / 2; // 50%
                }
                else if ((yearNow - yearUserOrder) > 50)
                {
                    orderDetail.PriceCalculated = (price * 70) / 100; // 70%
                }
                else
                {
                    orderDetail.PriceCalculated = price;
                }

                // orderDetail.PriceCalculated = (yEAR 5=0, 5<= y <=12: 50%  ,  y>50 : 70%)
                //var ticket = new TicketModel();

                orderDetail.RoutingID = Convert.ToInt32(collection["RoutingId"]);
                orderDetail.SeatNumber = Convert.ToInt16(collection["SeatNumber"]);
                Session["order"] = order;
                Session["RoutingId"] = orderDetail.RoutingID;
                Session["busID"] = busID;

                db.Order.Add(order);
                db.OrderDetail.Add(orderDetail);
                db.SaveChanges();

               

                


                return RedirectToAction("Ticket", "Booking");
            }
            catch (Exception ex)
            {
                Session["order"] = null;
                Session["RoutingId"] = null;
                Session["busID"] = null;
                return View();
            }

        }

        // Ticket View
        public ActionResult Ticket()
        {
            var order = Session["order"] as Order;
            var routingID = Convert.ToInt32(Session["RoutingId"]);
            var busID = Convert.ToString(Session["busID"]);

            var userID = Convert.ToInt32((Session["UserLoggedId"]));
            ViewBag.ticketUserName = (Session["UserLoggedId"]);
            ViewBag.ticketFullName = db.User.FirstOrDefault(x => x.UserID == order.UserID).FullName;
            ViewBag.ticketBirthDate = (DateTime)db.User.FirstOrDefault(x => x.UserID == order.UserID).BirthDate;
            ViewBag.ticketAddress = db.User.FirstOrDefault(x => x.UserID == order.UserID).Address;
            ViewBag.ticketPhone = db.User.FirstOrDefault(x => x.UserID == order.UserID).Phone;
            ViewBag.ticketStartPlace = db.Routing.FirstOrDefault(x => x.RoutingID == routingID).StartPlace;
            ViewBag.ticketEndPlace = db.Routing.FirstOrDefault(x => x.RoutingID == routingID).EndPlace;
            ViewBag.ticketBusName = db.Bus.FirstOrDefault(x => x.BusID == busID).BusName;

            var orderID = db.Order.FirstOrDefault(x => x.UserID == userID).OrderID;
            ViewBag.ticketSeatNumber = db.OrderDetail.FirstOrDefault(x => x.OrderID == orderID).SeatNumber;

            //var orderDetailSeatNumber = db.OrderDetail.FirstOrDefault(x => x.OrderID == orderID).SeatNumber;
            //ViewBag.ticketSeatCode = collection["BusID"] + orderDetailSeatNumber.ToString();

            ViewBag.ticketPrice = db.OrderDetail.FirstOrDefault(x => x.RoutingID == routingID).PriceCalculated;

            return View();
        }

        // GET: Booking/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Booking/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Booking/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Booking/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
