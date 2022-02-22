using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KorisnikServis.Database.Entities
{
    /// <summary>
    /// Korisnik entity
    /// </summary>
    public class Korisnik
    {
        /// <summary>
        /// KorisnikID primarni kljuc
        /// </summary>
        public Guid KorisnikID { get; set; }

        /// <summary>
        /// ImeKorisnika
        /// </summary>
        public string ImeKorisnika { get; set; }

        /// <summary>
        /// PrezimeKorisnika
        /// </summary>
        public string PrezimeKorisnika { get; set; }

        /// <summary>
        /// KorisnickoIme
        /// </summary>
        public string KorisnickoIme { get; set; }

        /// <summary>
        /// Lozinka
        /// </summary>
        public string Lozinka { get; set; }

        /// <summary>
        /// TipKorisnikaID
        /// </summary>
        [ForeignKey("TipKorisnikaID")]
        public Guid TipKorisnikaID { get; set; }

    }
}
