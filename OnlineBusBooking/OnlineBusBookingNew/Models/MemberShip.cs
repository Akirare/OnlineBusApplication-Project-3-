﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineBusBookingNew.Models
{
    public class MemberShip
    {
        public long UserID { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public int Type { get; set; }

    }
}