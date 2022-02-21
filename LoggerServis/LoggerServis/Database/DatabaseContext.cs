using LoggerServis.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoggerServis.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Logger> Logger { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-AMVOLPE\SQLEXPRESS;Initial Catalog=LoggerServis; Integrated Security=True;");
        }
    }
}
