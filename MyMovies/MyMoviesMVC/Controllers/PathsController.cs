using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyMoviesMVC.Models;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Security;


namespace MyMoviesMVC.Controllers
{
    public class PathsController : Controller
    {
        private MovieDBContext db = new MovieDBContext();

        //
        // GET: /Paths/

        public ActionResult Index()
        {
            return View(db.MoviePaths.ToList());
        }

        //
        // GET: /Paths/Details/5

        public ActionResult Details(int id = 0)
        {
            MoviePath moviepath = db.MoviePaths.Find(id);
            if (moviepath == null)
            {
                return HttpNotFound();
            }
            return View(moviepath);
        }

        //
        // GET: /Paths/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Paths/Create

        [HttpPost]
        public ActionResult Create(MoviePath moviepath)
        {
            if (ModelState.IsValid)
            {
                db.MoviePaths.Add(moviepath);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(moviepath);
        }

        //
        // GET: /Paths/Edit/5

        public ActionResult Edit(int id = 0)
        {
            MoviePath moviepath = db.MoviePaths.Find(id);
            if (moviepath == null)
            {
                return HttpNotFound();
            }
            return View(moviepath);
        }

        //
        // POST: /Paths/Edit/5

        [HttpPost]
        public ActionResult Edit(MoviePath moviepath)
        {
            if (ModelState.IsValid)
            {
                db.Entry(moviepath).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(moviepath);
        }

        //
        // GET: /Paths/Delete/5

        public ActionResult Delete(int id = 0)
        {
            MoviePath moviepath = db.MoviePaths.Find(id);
            if (moviepath == null)
            {
                return HttpNotFound();
            }
            return View(moviepath);
        }

        //
        // POST: /Paths/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            MoviePath moviepath = db.MoviePaths.Find(id);
            db.MoviePaths.Remove(moviepath);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public PartialViewResult GetMoviePaths(int id = 0)
        {
            List<MoviePath> mps = db.MoviePaths.Where(m => m.Movies_ID == id).ToList();            
            foreach(MoviePath mp in mps)
            {
                mp.path = mp.path.Substring(0, mp.path.LastIndexOf('\\'));
                mp.path = mp.path.Replace("\\","\\\\");
            }
            return PartialView("GetMoviePaths", mps);
        }
        public ActionResult MoviePlayer(int id = 0)
        {
            MoviePath mp = db.MoviePaths.FirstOrDefault(m => m.Movies_ID == id); 
            return RedirectToActionPermanent("Details", "Movie", new { id=id });
        }
    }
}