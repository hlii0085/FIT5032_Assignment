using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FIT5032_Assignment.Models
{
    public class Hospital
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string HospitalName { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        [MaxLength(500)]
        public string Address { get; set; }

        [Required]
        public float Rating { get; set; }

    }
}