using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FIT5032_Assignment.Models
{
    public class Hospital
    {
        public int Id { get; set; }

        public string HospitalName { get; set; }

        public string Description { get; set; }

        public string Lattitude { get; set; }

        public string Longitude { get; set; }

        public string Address { get; set; }

        public int PostCode { get; set; }

        public float Rating { get; set; }

    }
}