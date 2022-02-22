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
    /// <summary>
    /// Korisnik servis klasa
    /// </summary>
    public class KorisnikService
    {
        private readonly DatabaseContext db;

        /// <summary>
        /// Korisnik servis konstruktor
        /// </summary>
        public KorisnikService()
        {
            db = new DatabaseContext();
        }

        /// <summary>
        /// Getovanje svih korisnika iz baze
        /// </summary>
        /// <returns>Vraca sve korisnike iz baze</returns>
        public IEnumerable<Korisnik> GetAll()
        {
            return db.Korisnik.ToList();
        }

        /// <summary>
        /// Pronalazenje korisnika iz baze po korisnickom imenu i lozinci
        /// </summary>
        /// <param name="KorisnickoIme"></param>
        /// <param name="Lozinka"></param>
        /// <returns>Vraca korisnika sa zadatim korisnickim imenom i lozinkom</returns>
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


        /// <summary>
        /// Getovanje korisnika po zadatom id-u
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Vraca korisnika sa zadatim id-em</returns>
        public Korisnik GetById(Guid id)
        {
            return db.Korisnik.Find(id);
        }

        /// <summary>
        /// Getovanje korisnika po zadatoj ulozi koju ima
        /// </summary>
        /// <param name="nazivTipa"></param>
        /// <returns>Vraca korisnika sa datom ulogom</returns>
        public List<Korisnik> GetByTip(string nazivTipa)
        {
            List<Korisnik> korisniks = new List<Korisnik>();
            Guid tipKorisnikaID = Guid.Empty;
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

        /// <summary>
        /// Getovanje korisnika po zadatom tokenu
        /// </summary>
        /// <param name="identityClaims"></param>
        /// <returns>Vraca korisnika sa zadatim tokenom</returns>
        public Korisnik GetKorisnikByToken(ClaimsIdentity identityClaims)
        {
            Korisnik korisnik = new Korisnik()
            {
                KorisnikID = Guid.Parse(identityClaims.FindFirst("KorisnikID").Value),
                ImeKorisnika = identityClaims.FindFirst("ImeKorisnika").Value,
                PrezimeKorisnika = identityClaims.FindFirst("PrezimeKorisnika").Value,
                KorisnickoIme = identityClaims.FindFirst("KorisnickoIme").Value,
                Lozinka = identityClaims.FindFirst("Lozinka").Value,
                TipKorisnikaID = Guid.Parse(identityClaims.FindFirst("TipKorisnikaID").Value)
            };
            return korisnik;
        }

        /// <summary>
        /// Dodavanje novog korisnika
        /// </summary>
        /// <param name="korisnik"></param>
        public void Save(Korisnik korisnik)
        {
            db.Korisnik.Add(korisnik);
            db.SaveChanges();
        }

        /// <summary>
        /// Modifikovanje korisnika
        /// </summary>
        /// <param name="korisnik"></param>
        public void Update(Korisnik korisnik)
        {
            db.Entry(korisnik).State = EntityState.Modified;
            db.SaveChanges();
        }

        /// <summary>
        /// Provera postojanja korisnika u bazi
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Vraca true ili false u zavisnosti od postojanja</returns>
        public bool KorisnikExists(Guid id)
        {
            return db.Korisnik.Any(e => e.KorisnikID == id);
        }

        /// <summary>
        /// Brisanje korisnika
        /// </summary>
        /// <param name="korisnik"></param>
        public void Delete(Korisnik korisnik)
        {
            db.Korisnik.Remove(korisnik);
            db.SaveChanges();
        }
    }
}
