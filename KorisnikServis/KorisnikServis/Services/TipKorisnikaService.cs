﻿using KorisnikServis.Database;
using KorisnikServis.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KorisnikServis.Services
{
    public class TipKorisnikaService
    {
        private readonly DatabaseContext  db;

        public TipKorisnikaService()
        {
            db = new DatabaseContext();
        }

        public IEnumerable<TipKorisnika> GetAll()
        {
            return db.TipKorisnika.ToList();
        }

        public TipKorisnika GetById(Guid id)
        {
            return db.TipKorisnika.Find(id);
        }

        public void Save(TipKorisnika tipKorisnika)
        {
            db.TipKorisnika.Add(tipKorisnika);
            db.SaveChanges();
        }

        public void Update(TipKorisnika tipKorisnika)
        {
            db.Entry(tipKorisnika).State = EntityState.Modified;
            db.SaveChanges();
        }

        public bool TipKorisnikaExists(Guid id)
        {
            return db.TipKorisnika.Any(e => e.TipKorisnikaID == id);
        }

        public void Delete(TipKorisnika tipKorisnika)
        {
            db.TipKorisnika.Remove(tipKorisnika);
            db.SaveChanges();
        }
    }
}
