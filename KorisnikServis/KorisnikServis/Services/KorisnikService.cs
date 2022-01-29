using KorisnikServis.Database;
using KorisnikServis.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KorisnikServis.Services
{
    public class KorisnikService
    {
        DatabaseContext db;

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
            return db.Korisnik.Count(e => e.KorisnikID == id) > 0;
        }

        public void Delete(Korisnik korisnik)
        {
            db.Korisnik.Remove(korisnik);
            db.SaveChanges();
        }
    }
}
