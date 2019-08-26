using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using TestApp.Models;

namespace TestApp
{
    public class FilmContext : DbContext
    {
        public FilmContext() : base("FilmContext")
        {
            Database.SetInitializer<FilmContext>(null);
        }

        public DbSet<Film> Films { get; set; }
    }
}