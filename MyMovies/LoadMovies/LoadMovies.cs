using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using TheMovieDb;
using System.Threading;
using System.Text.RegularExpressions;

namespace LoadMovies
{
    public class LoadMovies
    {
        public static void MovieDirectoryList()
        {
            try
            {
                //declare variables
                MyMoviesEntities dbContext = new MyMoviesEntities();
                List<string> paths = new List<string>();
                Dictionary<string, List<string>> movieName = new Dictionary<string, List<string>>();
                //get list of movies in db
                List<Movie> CurrentMovies = new List<Movie>();

                CurrentMovies = dbContext.Movies.Select(s => s).ToList();

                //Scan Directorys
                Utility.TreeScan(@"\\MEDIAHUB\media\videos\Downloads\Movies", ref paths);
                //Put Titles and path in a map
                foreach (string str in paths)
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
                    List<Movie> dbmovielist = CurrentMovies.Where(m => name.Contains(m.Name) || name.Equals(m.Name)).ToList();
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
                        currentMovie.Released = Convert.ToDateTime(MovieData.Released);
                        currentMovie.Runtime = Convert.ToInt32(MovieData.Runtime);
                        currentMovie.ImdbId = MovieData.ImdbId;
                        dbContext.Movies.Add(currentMovie);
                        dbContext.SaveChanges();

                        foreach (TmdbGenre genreData in MovieData.Genres)
                        {
                            MovieGenre mg = new MovieGenre();
                            mg.Genre_ID = dbContext.Genres.FirstOrDefault(g => g.Name == genreData.Name).Id;
                            mg.Movies_ID = currentMovie.Id;
                            dbContext.MovieGenres.Add(mg);
                            dbContext.SaveChanges();
                        }
                        foreach (String pathData in currentPaths)
                        {
                            MoviePath mp = new MoviePath();
                            mp.path = pathData;
                            mp.Movies_ID = currentMovie.Id;
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

        public static void TVShowsDirectoryList()
        {
            try
            {
                //declare variables
                MyMoviesEntities dbContext = new MyMoviesEntities();
                List<string> paths = new List<string>();
                Dictionary<string, List<string>> showName = new Dictionary<string, List<string>>();
                //get list of movies in db
                List<Show> Currentshows = new List<Show>();

                Currentshows = dbContext.Shows.Select(s => s).ToList();

                //Scan Directorys
                Utility.TreeScan(@"\\MEDIAHUB\media\videos\Downloads\tv", ref paths);
                //Put Titles and path in a map
                //List<string> showTitles = new List<string>();
                foreach (string str in paths)
                {
                    //format movie name
                    string dir = Path.GetDirectoryName(str);
                    string[] dirParts = dir.Split(Path.DirectorySeparatorChar);
                    for(int i = 0; i < dirParts.Count(); i++)
                    {
                        //Console.WriteLine(str);
                        if(dirParts[i].ToLower().Contains("tv"))
                        {                            
                            if (showName.ContainsKey(dirParts[i+1]))
                            {
                                showName[dirParts[i + 1]].Add(str);
                            }
                            else
                            {
                                showName.Add(dirParts[i + 1], new List<string>());
                                showName[dirParts[i + 1]].Add(str);
                            }
                        }//end if
                    }//end for
                    
                }//end for
                //foreach (string str in paths)
                //{
                //    Regex regStyleOne = new Regex(@"[0-9]x[0-9][0-9]");
                //    Regex regStyleTwo = new Regex(@"[0-9][0-9]\w[0-9][0-9]");
                //    string[] dirParts = str.Split(Path.DirectorySeparatorChar);
                //    string name = dirParts.Last();

                //    
                //    {
                //        //Console.WriteLine(name);
                //    }
                //    //Console.WriteLine(reg.Match(name));
                //}
                foreach(string showTitle in showName.Keys)
                {
                    Show currentShow = Currentshows.FirstOrDefault(s=> s.Title == showTitle);

                    if (currentShow == null)
                    {
                        string nezbinStart = @"https://www.newzbin2.es/search/query/?q=+%09";
                        string[] titlePart = showTitle.Split(' ');
                        for(int i = 0; i < titlePart.Count(); i++)
                        {
                            if(i == titlePart.Count()-1)
                            {
                                nezbinStart += titlePart[i];
                            }
                            else
                            {
                                nezbinStart += titlePart[i] + "+";
                            }
                        }
                        nezbinStart += @"&area=ss.630722&fpn=p&u_search_system=0&searchaction=Go&btnG_x=0&btnG_y=0&ss=630722&go=1&areadone=ss.630722";
                        currentShow = new Show();
                        currentShow.Title = showTitle;
                        currentShow.NewzbinSearch = nezbinStart;
                        dbContext.Shows.Add(currentShow);
                        dbContext.SaveChanges();
                    }
                    
                    foreach (String path in showName[showTitle])
                    {
                        int season = 0;
                        int episode = 0;

                        Regex regStyleOne = new Regex(@"[0-9]x[0-9][0-9]");
                        Regex regStyleTwo = new Regex(@"[0-9][0-9]\w[0-9][0-9]");
                        if (regStyleOne.IsMatch(path))
                        {
                            
                            string episodeReg = regStyleOne.Match(path).Value.ToLower();
                            string[] strSplit = episodeReg.Split('x');
                            season = Convert.ToInt32(strSplit[0]);
                            episode = Convert.ToInt32(strSplit[1]);
                        }
                        else if (regStyleTwo.IsMatch(path))
                        {
                            string episodeReg = regStyleTwo.Match(path).Value.ToLower();
                            string[] strSplit = episodeReg.Split('e');
                            season = Convert.ToInt32(strSplit[0]);
                            episode = Convert.ToInt32(strSplit[1]);
                        }
                        Episode currentEpisode = dbContext.Episodes.FirstOrDefault(e => e.Episode1 == episode && e.Season == season && e.ShowId == currentShow.Id);
                        if(currentEpisode ==null)
                        {
                            currentEpisode = new Episode();
                            currentEpisode.Season = season;
                            currentEpisode.Episode1 = episode;
                            currentEpisode.Path = path;
                            currentEpisode.ShowId = currentShow.Id;
                            dbContext.Episodes.Add(currentEpisode);
                            dbContext.SaveChanges();
                        }//end if
                    }//end for
                }
                
            }//end try
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }//end catch
        }//end method

    }
}
