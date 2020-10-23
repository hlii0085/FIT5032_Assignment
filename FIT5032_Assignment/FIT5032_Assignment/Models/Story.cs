using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FIT5032_Assignment.Models
{
    public class Story
    {

        public int Id { get; set; }

        [Required]
        public string Caption { get; set; }

        [Required]
        public string Description { get; set; }
    }
}