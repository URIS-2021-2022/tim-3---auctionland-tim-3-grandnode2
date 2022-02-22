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
    /// Verzija dokumenta servis klasa
    /// </summary>
    public class VerzijaDokumentaService
    {
        private readonly DatabaseContext db;

        /// <summary>
        /// Verzija dokumenta servis konstruktor
        /// </summary>
        public VerzijaDokumentaService()
        {
            db = new DatabaseContext();
        }

        /// <summary>
        /// Getovanje svih verzija dokumenata
        /// </summary>
        /// <returns>Vraca sve verzije dokumenata</returns>
        public IEnumerable<VerzijaDokumenta> GetAll()
        {
            return db.VerzijaDokumenta.ToList();
        }

        /// <summary>
        /// Getovanje sve verzije dokumenata po zadatom id-u
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Vraca sve verzije dokumenata po id-u</returns>
        public VerzijaDokumenta GetById(Guid id)
        {
            return db.VerzijaDokumenta.Find(id);
        }

        /// <summary>
        /// Dodavanje nove verzije dokumenta
        /// </summary>
        /// <param name="verzijaDokumenta"></param>
        public void Save(VerzijaDokumenta verzijaDokumenta)
        {
            db.VerzijaDokumenta.Add(verzijaDokumenta);
            db.SaveChanges();
        }

        /// <summary>
        /// Modifikacija verzije dokumenta
        /// </summary>
        /// <param name="verzijaDokumenta"></param>
        public void Update(VerzijaDokumenta verzijaDokumenta)
        {
            db.Entry(verzijaDokumenta).State = EntityState.Modified;
            db.SaveChanges();
        }

        /// <summary>
        /// Provera da li verzija dokumenta postoji ili ne
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Vraca true ili false u zavisnosti od postojanja</returns>
        public bool VerzijaDokumentaExists(Guid id)
        {
            return db.VerzijaDokumenta.Any(e => e.VerzijaDokumentaID == id);
        }

        /// <summary>
        /// Brisanje verzije dokumenta
        /// </summary>
        /// <param name="verzijaDokumenta"></param>
        public void Delete(VerzijaDokumenta verzijaDokumenta)
        {
            db.VerzijaDokumenta.Remove(verzijaDokumenta);
            db.SaveChanges();
        }
    }
}
