using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MyMoviesMVC.Models
{
    public class Movie
    {
        public int Id { get; set; }

        public string ImdbId { get; set; }
        public string Name { get; set; }
        public string Runtime { get; set; }
        public string Released { get; set; }
        
    }

    public class MovieGenre
    {
        public int Id { get; set; }
        public int Genre_ID { get; set; }
        public int Movies_ID { get; set; }
    }
    public class MovieCast
    {
        public int Id { get; set; }
        public CastPerson Cast { get; set; }
        public Movie Movies { get; set; }
    }
    public class MoviePath
    {
        public int Id { get; set; }
        public int Movies_ID { get; set; }
        public string path { get; set; }
    }

   
}