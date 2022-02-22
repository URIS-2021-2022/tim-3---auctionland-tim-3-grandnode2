using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoggerServis.Database.Entities
{
    /// <summary>
    /// Logger entity
    /// </summary>
    public class Logger
    {
        /// <summary>
        /// LoggerID primarni kljuc
        /// </summary>
        public int LoggerID { get; set; }

        /// <summary>
        /// Opis aktivnosti 
        /// </summary>
        public string OpisAktivnosti { get; set; }

        /// <summary>
        /// Datum i vreme
        /// </summary>
        public DateTime Datum { get; set; }
    }
}
