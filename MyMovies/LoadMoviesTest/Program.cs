using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoadMovies;
using TheMovieDb;
using LoadMovies;
namespace LoadMoviesTest
{
    class Program
    {
        static void Main(string[] args)
        {
            LoadMovies.LoadMovies.MovieDirectoryList();

            //Utility.PopulateGenre();

            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}
