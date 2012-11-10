using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoadMovies;


namespace LoadMoviesTest
{
    class Program
    {
        static void Main(string[] args)
        {
            LoadMovies.LoadMovies.MovieDirectoryList();
            LoadMovies.LoadMovies.TVShowsDirectoryList();
            //Utility.PopulateGenre();

            //   using(ServerManager serverManager = new ServerManager()) { 
            //Configuration config = serverManager.GetApplicationHostConfiguration();

            //ConfigurationSection anonymousAuthenticationSection = config.GetSection("system.webServer/security/authentication/anonymousAuthentication", "Contoso");
            //anonymousAuthenticationSection["enabled"] = false;

            //ConfigurationSection windowsAuthenticationSection = config.GetSection("system.webServer/security/authentication/windowsAuthentication", "Contoso");
            //windowsAuthenticationSection["enabled"] = true;

            //serverManager.CommitChanges();



            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}
