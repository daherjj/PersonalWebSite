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
    
    public partial class Episode
    {
        public int Id { get; set; }
        public int ShowId { get; set; }
        public int Season { get; set; }
        public Nullable<int> Episode1 { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
    
        public virtual Show Show { get; set; }
    }
}