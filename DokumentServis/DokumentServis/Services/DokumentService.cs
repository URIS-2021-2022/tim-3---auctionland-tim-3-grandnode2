using DokumentServis.Database;
using DokumentServis.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DokumentServis.Services
{
    public class DokumentService
    {
        private readonly DatabaseContext db;

        public DokumentService()
        {
            db = new DatabaseContext();
        }

        public IEnumerable<Dokument> GetAll()
        {
            return db.Dokument.ToList();
        }

        public Dokument GetById(Guid id)
        {
            return db.Dokument.Find(id);
        }

        public void Save(Dokument dokument)
        {
            db.Dokument.Add(dokument);
            db.SaveChanges();
        }

        public void Update(Dokument dokument)
        {
            db.Entry(dokument).State = EntityState.Modified;
            db.SaveChanges();
        }

        public bool DokumentExists(Guid id)
        {
            return db.Dokument.Any(e => e.DokumentID == id);
        }

        public void Delete(Dokument dokument)
        {
            db.Dokument.Remove(dokument);
            db.SaveChanges();
        }
    }
}
