using KorisnikServis.Database;
using KorisnikServis.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KorisnikServis.Services
{
    /// <summary>
    /// Tip korisnika servis klasa
    /// </summary>
    public class TipKorisnikaService
    {
        private readonly DatabaseContext  db;

        /// <summary>
        /// Tip korisnika servis konstruktor
        /// </summary>
        public TipKorisnikaService()
        {
            db = new DatabaseContext();
        }

        /// <summary>
        /// Getovanje svih tipova korisnika
        /// </summary>
        /// <returns>Vraca sve tipove korisnika</returns>
        public IEnumerable<TipKorisnika> GetAll()
        {
            return db.TipKorisnika.ToList();
        }

        /// <summary>
        /// Getovanje svih tipova korisnika po zadatom id-u
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Vraca tip korisnika sa zadatim id-em</returns>
        public TipKorisnika GetById(Guid id)
        {
            return db.TipKorisnika.Find(id);
        }

        /// <summary>
        /// Dodavanje novog tipa korisnika
        /// </summary>
        /// <param name="tipKorisnika"></param>
        public void Save(TipKorisnika tipKorisnika)
        {
            db.TipKorisnika.Add(tipKorisnika);
            db.SaveChanges();
        }

        /// <summary>
        /// Modifikovanje tipa korisnika
        /// </summary>
        /// <param name="tipKorisnika"></param>
        public void Update(TipKorisnika tipKorisnika)
        {
            db.Entry(tipKorisnika).State = EntityState.Modified;
            db.SaveChanges();
        }

        /// <summary>
        /// Provera da li tip korisnika postoji u bazi
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Vraca true ili false u zavisnosti od postojanja</returns>
        public bool TipKorisnikaExists(Guid id)
        {
            return db.TipKorisnika.Any(e => e.TipKorisnikaID == id);
        }

        /// <summary>
        /// Brisanje tipa korisnika
        /// </summary>
        /// <param name="tipKorisnika"></param>
        public void Delete(TipKorisnika tipKorisnika)
        {
            db.TipKorisnika.Remove(tipKorisnika);
            db.SaveChanges();
        }
    }
}
