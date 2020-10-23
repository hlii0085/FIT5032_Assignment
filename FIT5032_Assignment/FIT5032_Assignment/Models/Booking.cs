using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FIT5032_Assignment.Models
{
    public class Booking
    {
        public int Id { get; set; }

        public Hospital Hospital { get; set; }

        public int HospitalId { get; set; }

        public DateTime Time { get; set; }

    }
}