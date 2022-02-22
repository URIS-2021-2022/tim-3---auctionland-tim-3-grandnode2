using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KorisnikServis.Database.Entities
{
    /// <summary>
    /// TipKorisnika entity
    /// </summary>
    public class TipKorisnika
    {
        /// <summary>
        /// TipKorisnikaID primarni kljuc
        /// </summary>
        public Guid TipKorisnikaID { get; set; }

        /// <summary>
        /// NazivTipa
        /// </summary>
        public string NazivTipa { get; set; }
    }
}
