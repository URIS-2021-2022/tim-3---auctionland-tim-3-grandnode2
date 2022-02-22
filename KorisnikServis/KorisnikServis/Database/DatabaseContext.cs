using KorisnikServis.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KorisnikServis.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<TipKorisnika> TipKorisnika { get; set; }
        public DbSet<Korisnik> Korisnik { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-A7T4B1V\SQLEXPRESS;Initial Catalog=KorisnikServis; Integrated Security=True;");
        }
    }
}
