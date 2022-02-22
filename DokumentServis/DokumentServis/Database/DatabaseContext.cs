using DokumentServis.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DokumentServis.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<VerzijaDokumenta> VerzijaDokumenta { get; set; }

        public DbSet<Dokument> Dokument { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-A7T4B1V\SQLEXPRESS;Initial Catalog=DokumentServis; Integrated Security=True;");
        }
    }
}
