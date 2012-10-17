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
                MoviesEntities dbContext = new MoviesEntities();
                List<string> paths = new List<string>();
                Dictionary<string, List<string>> movieName = new Dictionary<string, List<string>>();
                List<Movie> CurrentMovies = new List<Movie>();
            
                CurrentMovies = dbContext.Movies.Where(m => m.Name != null).ToList();
            
                //Scan Directorys
                Utility.TreeScan(@"Z:\videos\Downloads\Movies", ref paths);
                //Put Titles and path in a map
                foreach(string str in paths)
                {
                    
                    string dir = Path.GetDirectoryName(str);
                    string[] dirParts = dir.Split(Path.DirectorySeparatorChar);
                    string name = dirParts[dirParts.Count() - 1];
                    int yearPos = name.IndexOf("(");
                    //string outh year from the title
                    if (yearPos > 0)
                    {
                        name = name.Substring(0, yearPos);
                    }
                    if (!(CurrentMovies.Where(m => m.Name == name).ToList().Count() > 0))
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

                        currentMovie.Name = MovieData.Name;
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
