using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyMovies.Controllers
{
    public class ShowController : Controller
    {
        private MyMoviesEntities db = new MyMoviesEntities();

        //
        // GET: /Show/

        public ActionResult Index()
        {
            return View(db.Shows.ToList().OrderBy(s=> s.Title));
        }

        //
        // GET: /Show/Details/5

        public ActionResult Details(int id = 0)
        {
            Show show = db.Shows.Find(id);
            if (show == null)
            {
                return HttpNotFound();
            }
            return View(show);
        }

        //
        // GET: /Show/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Show/Create

        [HttpPost]
        public ActionResult Create(Show show)
        {
            if (ModelState.IsValid)
            {
                db.Shows.Add(show);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(show);
        }

        //
        // GET: /Show/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Show show = db.Shows.Find(id);
            if (show == null)
            {
                return HttpNotFound();
            }
            return View(show);
        }

        //
        // POST: /Show/Edit/5

        [HttpPost]
        public ActionResult Edit(Show show)
        {
            if (ModelState.IsValid)
            {
                db.Entry(show).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(show);
        }

        //
        // GET: /Show/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Show show = db.Shows.Find(id);
            if (show == null)
            {
                return HttpNotFound();
            }
            return View(show);
        }

        //
        // POST: /Show/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Show show = db.Shows.Find(id);
            db.Shows.Remove(show);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
        public PartialViewResult GetLatestEpisode(int id = 0)
        {
            List<Episode> EpisodeList = db.Episodes.Where(e => e.ShowId == id).ToList();
            Episode ep = new Episode();
            if (EpisodeList != null && EpisodeList.Count() > 0)
            {

                int maxSeason = EpisodeList.Max(e => e.Season);
                List<Episode> seasonList = EpisodeList.Where(e => e.Season == maxSeason).ToList();

                int maxEpisode = Convert.ToInt32(seasonList.Max(e => e.Episode1));
                ep = seasonList.FirstOrDefault(e => e.Episode1 == maxEpisode);
            }
            else
            {
                ep.Season = 0;
                ep.Episode1 = 0;
            }
            return PartialView("GetLatestEpisode", ep);
        }
    }
}