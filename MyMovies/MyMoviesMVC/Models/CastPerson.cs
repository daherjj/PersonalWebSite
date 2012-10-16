﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MyMoviesMVC.Models
{
    public class CastPerson
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Character { get; set; }
    }
    public class CastDBContext : DbContext
    {
        public DbSet<CastPerson> Cast { get; set; }
    }
}