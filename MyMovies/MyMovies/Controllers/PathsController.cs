using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyMovies.Controllers
{
    public class PathsController : Controller
    {
        private MyMoviesEntities db = new MyMoviesEntities();

        //
        // GET: /Paths/

        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult GetMoviePaths(int id = 0)
        {
            List<MoviePath> mps = db.MoviePaths.Where(m => m.Movies_ID == id).ToList();
            foreach (MoviePath mp in mps)
            {
                //mp.path = mp.path.Substring(0, mp.path.LastIndexOf('\\'));
                //mp.path = mp.path.Replace("\\", "");
            }
            return PartialView("GetMoviePaths", mps);
        }
    }
}
