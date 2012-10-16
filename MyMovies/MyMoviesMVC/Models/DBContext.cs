using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;


namespace MyMoviesMVC.Models
{
    public class MovieDBContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<CastPerson> Cast { get; set; }
        public DbSet<MovieCast> MovieCasts { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }
    }
}