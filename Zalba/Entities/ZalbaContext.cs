using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zalba.Entities
{
    public class ZalbaContext : DbContext
    {

        public ZalbaContext(DbContextOptions options) : base(options)
        {
           
        }

        public DbSet<ZalbaE> Zalbas { get; set; }
        public DbSet<TipZalbeE> TipZalbes { get; set; }


        /// <summary>
        /// Popunjava bazu sa nekim inicijalnim podacima
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TipZalbeE>()
                .HasData(new
                {
                    TipZalbeID = Guid.Parse("044f3de0-a9dd-4c2e-b745-89976a1b2a36"),
                    NazivTipa = "nnnnnn",
                    OpisTipa = "..."
                });
            modelBuilder.Entity<TipZalbeE>()
                .HasData(new
                {
                    TipZalbeID = Guid.Parse("32cd906d-8bab-457c-ade2-fbc4ba523029"),
                    NazivTipa = "nnnnnn",
                    OpisTipa = "..."
                });
            modelBuilder.Entity<ZalbaE>()
                .HasData(new
                {
                    ZalbaID = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    TipZalbeID = Guid.Parse("044f3de0-a9dd-4c2e-b745-89976a1b2a36"),
                    LicitacijaID = Guid.Parse("3F8AA5B3-A67F-45B5-B518-771A7C09A944"),
                    PodnosilacZalbeID = Guid.Parse("e03de167-e497-46e2-bcf2-9f22903ab55c"),
                    DatPodnosenjaZalbe = DateTime.Parse("2020-12-15T09:05:26"),
                    Obrazlozenje = "Podneta zalba je usvojena",
                    DatResenja = DateTime.Parse("2020-12-17T09:09:20"),
                    BrojResenja = 345,
                    StatusZalbe = "usvojena",
                    BrojOdluke = 23,
                    RadnjaZalbe = "JN ide u drugi krug sa novim uslovima"
                });

            modelBuilder.Entity<ZalbaE>()
                .HasData(new
                {
                    ZalbaID = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                    TipZalbeID = Guid.Parse("32cd906d-8bab-457c-ade2-fbc4ba523029"),
                    LicitacijaID = Guid.Parse("4E1F1F8D-A8F7-44B1-9BDA-1C1EE122628D"),
                    PodnosilacZalbeID = Guid.Parse("54001bad-2161-42ac-9241-54ead772ed11"),
                    DatPodnosenjaZalbe = DateTime.Parse("2021-12-15T09:05:26"),
                    Obrazlozenje = "Podneta zalba je usvojena",
                    DatResenja = DateTime.Parse("2021-12-17T09:09:20"),
                    BrojResenja = 687,
                    StatusZalbe = "usvojena",
                    BrojOdluke = 89,
                    RadnjaZalbe = "JN ide u drugi krug sa starim uslovima"
                });
        }
    }
}
