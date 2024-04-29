using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Question_2.Models
{
    public class Movie
    {
        [Key]
        public int mid { get; set; }
        public string Moviename { get; set; }
        public string DateofRelease { get; set; }
    }
}