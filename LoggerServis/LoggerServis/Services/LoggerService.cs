using LoggerServis.Database;
using LoggerServis.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoggerServis.Services
{
    /// <summary>
    /// Logger servis klasa
    /// </summary>
    public class LoggerService
    {
        private readonly DatabaseContext db;

        /// <summary>
        /// Logger servis konstruktor
        /// </summary>
        public LoggerService()
        {
            db = new DatabaseContext();
        }

        /// <summary>
        /// Getovanje svih loggera iz baze 
        /// </summary>
        /// <returns>Vraca sve loggere iz baze</returns>
        public IEnumerable<Logger> GetAll()
        {
            return db.Logger.ToList();
        }

        /// <summary>
        /// Getovanje svih loggera iz baze po zadatom idu
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Vraca logger sa zadatim id-em</returns>
        public Logger GetById(int id)
        {
            return db.Logger.Find(id);
        }

        /// <summary>
        /// Kreiranje loggera
        /// </summary>
        /// <param name="logger"></param>
        public void Save(Logger logger)
        {
            db.Logger.Add(logger);
            db.SaveChanges();
        }

        /// <summary>
        /// Modifikovanje loggera
        /// </summary>
        /// <param name="logger"></param>
        public void Update(Logger logger)
        {
            db.Entry(logger).State = EntityState.Modified;
            db.SaveChanges();
        }

        /// <summary>
        /// Provera da li logger postoji u bazi
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Vraca true ili false u zavisnosti od toga da li postoji ili ne</returns>
        public bool LoggerExists(int id)
        {
            return db.Logger.Any(e => e.LoggerID == id);
        }

        /// <summary>
        /// Brisanje loggera
        /// </summary>
        /// <param name="logger"></param>
        public void Delete(Logger logger)
        {
            db.Logger.Remove(logger);
            db.SaveChanges();
        }
    }
}
