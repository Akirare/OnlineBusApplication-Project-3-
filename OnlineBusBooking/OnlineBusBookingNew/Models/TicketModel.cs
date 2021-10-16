using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineBusBookingNew.Models
{
    public class TicketModel
    {
        [Key]
        public string UserName { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string StartPlace { get; set; }
        public string EndPlace { get; set; }
        public string BusName { get; set; }
        public int SeatNumber { get; set; }
        public string SeatCode { get; set; }
        public Nullable<decimal> Price { get; set; }
    }
}