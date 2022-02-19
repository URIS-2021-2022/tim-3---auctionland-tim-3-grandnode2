using DokumentServis.Database;
using DokumentServis.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DokumentServis.Services
{
    public class VerzijaDokumentaService
    {
        private readonly DatabaseContext db;

        public VerzijaDokumentaService()
        {
            db = new DatabaseContext();
        }

        public IEnumerable<VerzijaDokumenta> GetAll()
        {
            return db.VerzijaDokumenta.ToList();
        }

        public VerzijaDokumenta GetById(int id)
        {
            return db.VerzijaDokumenta.Find(id);
        }

        public void Save(VerzijaDokumenta verzijaDokumenta)
        {
            db.VerzijaDokumenta.Add(verzijaDokumenta);
            db.SaveChanges();
        }

        public void Update(VerzijaDokumenta verzijaDokumenta)
        {
            db.Entry(verzijaDokumenta).State = EntityState.Modified;
            db.SaveChanges();
        }

        public bool VerzijaDokumentaExists(int id)
        {
            return db.VerzijaDokumenta.Any(e => e.VerzijaDokumentaID == id);
        }

        public void Delete(VerzijaDokumenta verzijaDokumenta)
        {
            db.VerzijaDokumenta.Remove(verzijaDokumenta);
            db.SaveChanges();
        }
    }
}
