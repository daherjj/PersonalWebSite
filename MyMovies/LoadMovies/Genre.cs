//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LoadMovies
{
    using System;
    using System.Collections.Generic;
    
    public partial class Genre
    {
        public Genre()
        {
            this.MovieGenres = new HashSet<MovieGenre>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    
        public virtual ICollection<MovieGenre> MovieGenres { get; set; }
    }
}