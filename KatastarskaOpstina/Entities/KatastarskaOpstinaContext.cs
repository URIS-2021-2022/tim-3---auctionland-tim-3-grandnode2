using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KatastarskaOpstina.Entities
{
    public class KatastarskaOpstinaContext : DbContext
    {
        public KatastarskaOpstinaContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<KatastarskaOpstinaE> KatastarskaOpstinas { get; set; }
        public DbSet<StatutOpstineE> StatutOpstines { get; set; }

        /// <summary>
        /// Popunjava bazu sa nekim inicijalnim podacima
        /// </summary>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<StatutOpstineE>()
                .HasData(new
                {
                    StatutOpstineID = Guid.Parse("644f3de0-a9dd-4c2e-b745-89976a1b2a36"),
                    SadrzajStatuta = "...",
                    DatumKreiranjaStatuta = DateTime.Now
                });

            builder.Entity<StatutOpstineE>()
                .HasData(new
                {
                    StatutOpstineID = Guid.Parse("044f3de0-a9dd-4c2e-b745-89976a1b2a36"),
                    SadrzajStatuta = "...",
                    DatumKreiranjaStatuta = DateTime.Now
                });


            builder.Entity<KatastarskaOpstinaE>()
                .HasData(new
                {
                    KatastarskaOpstinaID = Guid.Parse("6b411c13-a295-48f7-8dbd-67886c3974c0"),
                    StatutOpstineID = Guid.Parse("044f3de0-a9dd-4c2e-b745-89976a1b2a36"),
                    NazivOpstine = "Bikovo"
                });

            builder.Entity<KatastarskaOpstinaE>()
                .HasData(new
                {
                    KatastarskaOpstinaID = Guid.Parse("1b411c13-a295-48f7-8dbd-67886c3974c0"),
                    StatutOpstineID = Guid.Parse("644f3de0-a9dd-4c2e-b745-89976a1b2a36"),
                    NazivOpstine = "Novi Grad"
                });
        }

    }
}
