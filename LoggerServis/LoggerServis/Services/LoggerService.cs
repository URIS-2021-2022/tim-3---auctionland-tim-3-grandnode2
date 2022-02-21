using LoggerServis.Database;
using LoggerServis.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoggerServis.Services
{
    public class LoggerService
    {
        private readonly DatabaseContext db;

        public LoggerService()
        {
            db = new DatabaseContext();
        }

        public IEnumerable<Logger> GetAll()
        {
            return db.Logger.ToList();
        }

        public Logger GetById(int id)
        {
            return db.Logger.Find(id);
        }

        public void Save(Logger logger)
        {
            db.Logger.Add(logger);
            db.SaveChanges();
        }

        public void Update(Logger logger)
        {
            db.Entry(logger).State = EntityState.Modified;
            db.SaveChanges();
        }

        public bool LoggerExists(int id)
        {
            return db.Logger.Any(e => e.LoggerID == id);
        }

        public void Delete(Logger logger)
        {
            db.Logger.Remove(logger);
            db.SaveChanges();
        }
    }
}
