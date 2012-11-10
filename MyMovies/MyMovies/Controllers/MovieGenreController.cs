using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyMovies.Controllers
{
    public class MovieGenreController : Controller
    {
        private MyMoviesEntities db = new MyMoviesEntities();
        //
        // GET: /MovieGenre/

        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult GetMovieGenre(int id = 0)
        {
            List<MovieGenre> mgs = db.MovieGenres.Where(m => m.Movies_ID == id).ToList();
            List<Genre> gs = new List<Genre>();
            foreach (MovieGenre mg in mgs)
            {
                Genre g = db.Genres.FirstOrDefault(s => s.Id == mg.Genre_ID);
                gs.Add(g);
            }
            return PartialView("GetMovieGenre", gs);
        }

    }
}
