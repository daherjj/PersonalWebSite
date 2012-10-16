using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MyMoviesMVC.Models
{
    public class Genre
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Type { get; set; }
    }
    
}