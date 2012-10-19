﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyMoviesMVC.Models;


namespace MyMoviesMVC.Controllers
{
    public class MovieGenreController : Controller
    {
        private MovieDBContext db = new MovieDBContext();
        //
        // GET: /MovieGenre/

        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult GetMovieGenre(Movie mov)
        {
            List<MovieGenre> mgs = db.MovieGenres.Where(m => m.Movies_ID == mov.Id).ToList();
            List<Genre> gs = new List<Genre>();
            foreach(MovieGenre mg in mgs)
            {
                Genre g = db.Genres.FirstOrDefault(s => s.Id == mg.Genre_ID);
                gs.Add(g);
            }
            return PartialView("GetMovieGenre",gs);
        }

    }
}
