using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using TheMovieDb;

namespace LoadMovies
{
    public class Utility
    {
        internal static void TreeScan(string sDir, ref List<string> paths)
        {
            foreach (string f in Directory.GetFiles(sDir))
            {
                string ext = Path.GetExtension(f);
                if (!f.Contains("sample") && (ext == ".avi" || ext == ".mkv"))
                {
                    paths.Add(Path.GetFullPath(f));
                }

            }
            foreach (string d in Directory.GetDirectories(sDir))
            {
                TreeScan(d, ref paths);

            }
        }

        internal static TheMovieDb.TmdbMovie LookupMovieOnLine(string str)
        {
            TmdbMovie currentMovie = new TheMovieDb.TmdbMovie();
            try
            {
                TmdbApi login = new TheMovieDb.TmdbApi("d49ca7319b09616c927940697304294c");
                TmdbMovie m= login.MovieSearch(str).FirstOrDefault();
                //if(
                currentMovie = login.MovieSearchByImdb(m.ImdbId).FirstOrDefault();
            }
            catch(Exception ex)
            {
                //do nothing    
            }
            return currentMovie;
        }
        public static void PopulateGenre()
        {
            MoviesEntities dbcontext = new MoviesEntities();
            TheMovieDb.TmdbApi login = new TheMovieDb.TmdbApi("d49ca7319b09616c927940697304294c");
            List<TmdbGenre> m = login.GetGenres().ToList();
            foreach (TmdbGenre g in m)
            {
                
                if (!(dbcontext.Genres.Where(w => w.Name == g.Name).ToList().Count() > 0))
                {
                    Genre currentGenre = new Genre();
                    currentGenre.Name = g.Name;
                    currentGenre.Type = g.Type;
                    dbcontext.Genres.Add(currentGenre);
                    dbcontext.SaveChanges();
                    Console.WriteLine(g.Name);
                }
            }
        }//end method
    }
}
