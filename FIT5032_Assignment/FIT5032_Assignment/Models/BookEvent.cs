using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FIT5032_Assignment.Models
{
    public class BookEvent
    {
        public int Id { get; set; }

        public Booking Booking { get; set; }

        public int BookingId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public string ApplicationUserId { get; set; }
    }
}