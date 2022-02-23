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
        /// Id korisnika, primarni kljuc
        /// </summary>
        public Guid KorisnikID { get; set; }

        /// <summary>
        /// Ime korisnika 
        /// </summary>
        public string ImeKorisnika { get; set; }

        /// <summary>
        /// Prezime korisnika
        /// </summary>
        public string PrezimeKorisnika { get; set; }

        /// <summary>
        /// Korisnicko ime korisnika
        /// </summary>
        public string KorisnickoIme { get; set; }

        /// <summary>
        /// Lozinka korisnika
        /// </summary>
        public string Lozinka { get; set; }

        /// <summary>
        /// Tip korisnika
        /// </summary>
        [ForeignKey("TipKorisnikaID")]
        public Guid TipKorisnikaID { get; set; }

    }
}
