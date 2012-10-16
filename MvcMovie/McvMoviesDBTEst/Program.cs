using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McvMoviesDBTEst
{
    class Program
    {
        static void Main(string[] args)
        {
            MoviesEntities dbContext = new MoviesEntities();

            Movie test = dbContext.Movies.FirstOrDefault(m => m.ID == 1);
            Console.WriteLine("Title: " + test.Title);
            Console.ReadLine();
        }
    }
}
