using KorisnikServis.Database;
using KorisnikServis.Database.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KorisnikServis.Services
{
    public class KorisnikService
    {
        private readonly DatabaseContext db;

        public KorisnikService()
        {
            db = new DatabaseContext();
        }

        public IEnumerable<Korisnik> GetAll()
        {
            return db.Korisnik.ToList();
        }

        public Korisnik FindImeLozinka(string KorisnickoIme, string Lozinka)
        {
            Korisnik korisnik = new Korisnik();
            foreach (var k in db.Korisnik)
            {
                if (k.KorisnickoIme == KorisnickoIme && k.Lozinka == Lozinka)
                {
                    korisnik = k;
                }
            }
            return korisnik;
        }

        public Korisnik GetById(int id)
        {
            return db.Korisnik.Find(id);
        }

        public List<Korisnik> GetByTip(string nazivTipa)
        {
            List<Korisnik> korisniks = new List<Korisnik>();
            int tipKorisnikaID = 0;
            foreach (var k in db.TipKorisnika)
            {
                if (k.NazivTipa == nazivTipa)
                {
                    tipKorisnikaID = k.TipKorisnikaID;
                }
            }

            foreach (var k in db.Korisnik)
            {
                if (tipKorisnikaID == k.TipKorisnikaID)
                {
                    korisniks.Add(k);
                }
            }

            return korisniks;
        }

        public Korisnik GetKorisnikByToken(ClaimsIdentity identityClaims)
        {
            Korisnik korisnik = new Korisnik()
            {
                KorisnikID = Int32.Parse(identityClaims.FindFirst("KorisnikID").Value),
                ImeKorisnika = identityClaims.FindFirst("ImeKorisnika").Value,
                PrezimeKorisnika = identityClaims.FindFirst("PrezimeKorisnika").Value,
                KorisnickoIme = identityClaims.FindFirst("KorisnickoIme").Value,
                Lozinka = identityClaims.FindFirst("Lozinka").Value,
                TipKorisnikaID = Int32.Parse(identityClaims.FindFirst("TipKorisnikaID").Value)
            };
            return korisnik;
        }
        public void Save(Korisnik korisnik)
        {
            db.Korisnik.Add(korisnik);
            db.SaveChanges();
        }

        public void Update(Korisnik korisnik)
        {
            db.Entry(korisnik).State = EntityState.Modified;
            db.SaveChanges();
        }

        public bool KorisnikExists(int id)
        {
            return db.Korisnik.Any(e => e.KorisnikID == id);
        }

        public void Delete(Korisnik korisnik)
        {
            db.Korisnik.Remove(korisnik);
            db.SaveChanges();
        }
    }
}
