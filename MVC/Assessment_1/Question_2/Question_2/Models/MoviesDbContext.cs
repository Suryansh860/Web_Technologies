using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Question_2.Models.Models
{
    public class MoviesDbContext : DbContext
    {
        public MoviesDbContext() : base("ConnMov")
        { }
        public DbSet<Movie> movies { get; set; }
    }
}