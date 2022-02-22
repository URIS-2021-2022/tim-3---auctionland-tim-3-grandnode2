using DokumentServis.Database;
using DokumentServis.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DokumentServis.Services
{
    /// <summary>
    /// Dokument servis klasa
    /// </summary>
    public class DokumentService
    {
        private readonly DatabaseContext db;

        /// <summary>
        /// Dokument servis konstruktor
        /// </summary>
        public DokumentService()
        {
            db = new DatabaseContext();
        }

        /// <summary>
        /// Getovanje svih dokumenata iz baze
        /// </summary>
        /// <returns>Vraca sve dokumente</returns>
        public IEnumerable<Dokument> GetAll()
        {
            return db.Dokument.ToList();
        }

        /// <summary>
        /// Getovanje dokumenta po id-u
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Vraca dokument sa zadatim id-em</returns>
        public Dokument GetById(Guid id)
        {
            return db.Dokument.Find(id);
        }

        /// <summary>
        /// Kreiranje novog dokumenta
        /// </summary>
        /// <param name="dokument"></param>
        public void Save(Dokument dokument)
        {
            db.Dokument.Add(dokument);
            db.SaveChanges();
        }

        /// <summary>
        /// Modifikacija dokumenta
        /// </summary>
        /// <param name="dokument"></param>
        public void Update(Dokument dokument)
        {
            db.Entry(dokument).State = EntityState.Modified;
            db.SaveChanges();
        }

        /// <summary>
        /// Provera postojanja dokumenta
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Vraca true ili false u zavisnosti od toga da li dokument postoji</returns>
        public bool DokumentExists(Guid id)
        {
            return db.Dokument.Any(e => e.DokumentID == id);
        }

        /// <summary>
        /// Brisanje postojeceg dokumenta
        /// </summary>
        /// <param name="dokument"></param>
        public void Delete(Dokument dokument)
        {
            db.Dokument.Remove(dokument);
            db.SaveChanges();
        }
    }
}
