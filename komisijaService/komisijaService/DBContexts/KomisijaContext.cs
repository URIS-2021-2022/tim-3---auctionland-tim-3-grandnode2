using komisijaService.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace komisijaService.DBContexts
{
    public class KomisijaContext : DbContext
    {
        private readonly IConfiguration configuration;
        public KomisijaContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }

        public DbSet<LicnostKomisije> LicnostiKomisije { get; set; }
        public DbSet<Komisija> Komisija { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DatabaseForKomisija"));
        }

        /// <summary>
        /// Filling the database with data
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
             modelBuilder.Entity<Komisija>()
                .HasIndex(b => b.oznakaKomisije)
                .IsUnique();
        
            modelBuilder.Entity<Komisija>().HasData(
                new
                {
                    komisijaId = Guid.Parse("4e1f1f8d-a8f7-44b1-9abd-1c1ee122628d"),
                    nazivKomisije = "Prva komisija",
                    oznakaKomisije = "kom123ef"

                },
                new
                {
                    komisijaId = Guid.Parse("c99d5b97-6984-43ef-b0a5-89d04569466e"),
                    nazivKomisije = "Nova komisija",
                    oznakaKomisije = "kom345ef"
                });

            modelBuilder.Entity<LicnostKomisije>().HasData(
               new
               {
                   licnostKomisijeId = Guid.Parse("1f8aa5b3-a67f-45c5-b519-771a7c09a944"),
                   imeLicnostiKomisije = "Marko",
                   prezimeLicnostiKomisije = "﻿﻿﻿Markovic",
                   funkcijaLicnostiKomisije = "Pomocnik",
                   komisijaId = Guid.Parse("4e1f1f8d-a8f7-44b1-9abd-1c1ee122628d"),
                   kontaktLicnostiKomisije = "0645371333",
                   datumRodjenjaLicnostiKomisije = DateTime.Parse("1999-01-02"),
                   oznakaKomisije = "kom123ef"
               },
               new
               {
                   licnostKomisijeId = Guid.Parse("2d53fc22-eac4-43bb-8f55-d2b8495603cc"),
                   imeLicnostiKomisije = "Sonja",
                   prezimeLicnostiKomisije = "Stojanovic",
                   funkcijaLicnostiKomisije = "Prva postava",
                   komisijaId = Guid.Parse("4e1f1f8d-a8f7-44b1-9abd-1c1ee122628d"),
                   kontaktLicnostiKomisije = "0617825713",
                   datumRodjenjaLicnostiKomisije = DateTime.Parse("1989-09-18"),
                   oznakaKomisije = "kom123ef"
               },
               new
               {
                   licnostKomisijeId = Guid.Parse("4e1f1f8d-a8f7-44b1-9bda-1c1ee122628d"),
                   imeLicnostiKomisije = "Petar",
                   prezimeLicnostiKomisije = "Petrovic",
                   funkcijaLicnostiKomisije = "Obican clan",
                   komisijaId = Guid.Parse("c99d5b97-6984-43ef-b0a5-89d04569466e"),
                   kontaktLicnostiKomisije = "0672514739",
                   datumRodjenjaLicnostiKomisije = DateTime.Parse("1976-01-19"),
                   oznakaKomisije = "kom345ef"
               },
               new
               {
                   licnostKomisijeId = Guid.Parse("3f8aa5b3-a67f-45b5-b518-771a7c09a944"),
                   imeLicnostiKomisije = "Mina",
                   prezimeLicnostiKomisije = "Zlatic",
                   funkcijaLicnostiKomisije = "Predsednik",
                   komisijaId = Guid.Parse("c99d5b97-6984-43ef-b0a5-89d04569466e"),
                   kontaktLicnostiKomisije = "0651516733",
                   datumRodjenjaLicnostiKomisije = DateTime.Parse("1971-09-01"),
                   oznakaKomisije = "kom345ef"
               });

               


        }
    }
}
