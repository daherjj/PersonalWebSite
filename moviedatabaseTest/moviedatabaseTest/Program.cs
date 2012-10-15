using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace moviedatabaseTest
{
    class Program
    {
        static void Main(string[] args)
        {
            TheMovieDb.TmdbApi login = new TheMovieDb.TmdbApi("d49ca7319b09616c927940697304294c");
            TheMovieDb.TmdbMovie m = login.MovieSearch("2 Broke Girls").FirstOrDefault();
            TheMovieDb.TmdbMovie movie = login.MovieSearchByImdb(m.ImdbId).FirstOrDefault();

            Console.WriteLine("Titile: " + movie.Name );
            Console.WriteLine("Year: " + movie.Released);
            foreach (TheMovieDb.TmdbGenre g in movie.Genres)
            {
            Console.WriteLine("Genres: " + g.Name);
            }
            Console.WriteLine();
            
            Console.ReadLine();
        }
    }
}
