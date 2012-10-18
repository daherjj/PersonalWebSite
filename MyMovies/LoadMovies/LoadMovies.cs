using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using TheMovieDb;
using System.Threading;

namespace LoadMovies
{
    public class LoadMovies
    {
        public static void MovieDirectoryList()
        {
            try
            {
                //declare variables
                MoviesEntities dbContext = new MoviesEntities();
                List<string> paths = new List<string>();
                Dictionary<string, List<string>> movieName = new Dictionary<string, List<string>>();
                //get list of movies in db
                List<Movie> CurrentMovies = new List<Movie>();
            
                CurrentMovies = dbContext.Movies.Select(s => s).ToList();
            
                //Scan Directorys
                Utility.TreeScan(@"Z:\videos\Downloads\Movies", ref paths);
                //Put Titles and path in a map
                foreach(string str in paths)
                {
                    //format movie name
                    string dir = Path.GetDirectoryName(str);
                    string[] dirParts = dir.Split(Path.DirectorySeparatorChar);
                    string name = dirParts[dirParts.Count() - 1];
                    //string out year from the title
                    int yearPos = name.IndexOf("(");
                    if (yearPos > 0)
                    {
                        name = name.Substring(0, yearPos);
                        name = name.Trim();
                    }
                    //string out dash from the title
                    name = name.Replace('-', ':');
                    List<Movie> dbmovielist= CurrentMovies.Where(m => name.Contains(m.Name) || name.Equals(m.Name) ).ToList();
                    if (!(dbmovielist.Count() > 0))
                    {
                        if (movieName.ContainsKey(name))
                        {
                            movieName[name].Add(str);
                        }
                        else
                        {
                            movieName.Add(name, new List<string>());
                            movieName[name].Add(str);
                        }
                    }
                }//enf for 
                using (System.IO.StreamWriter file = new System.IO.StreamWriter("Movies Not added.txt"))
                {
                    foreach (var pair in movieName)
                    {
                        TheMovieDb.TmdbMovie MovieData = Utility.LookupMovieOnLine(pair.Key.ToString());
                        if (MovieData != null && MovieData.Name == null)
                        {
                            file.WriteLine(pair.Key.ToString() + "---- Not Added");
                            continue;
                        }


                        Movie currentMovie = new Movie();
                        List<String> currentPaths = pair.Value.ToList();

                        currentMovie.Name = pair.Key.ToString();
                        currentMovie.Released = MovieData.Released;
                        currentMovie.Runtime = MovieData.Runtime;
                        currentMovie.ImdbId = MovieData.ImdbId;
                        dbContext.Movies.Add(currentMovie);
                        dbContext.SaveChanges();

                        foreach (TmdbGenre genreData in MovieData.Genres)
                        {
                            MovieGenre mg = new MovieGenre();
                            mg.Genres_Id = dbContext.Genres.FirstOrDefault(g => g.Name == genreData.Name).Id;
                            mg.Movies_Id = currentMovie.Id;
                            dbContext.MovieGenres.Add(mg);
                            dbContext.SaveChanges();
                        }
                        foreach (String pathData in currentPaths)
                        {
                            MoviePath mp = new MoviePath();
                            mp.path = pathData;
                            mp.Movies_Id = currentMovie.Id;
                            dbContext.MoviePaths.Add(mp);
                            dbContext.SaveChanges();
                        }

                        Thread.Sleep(100);

                    }//end for
                }//end using

                
            }//end try
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }//end catch
        }//end method
         
    }
}
