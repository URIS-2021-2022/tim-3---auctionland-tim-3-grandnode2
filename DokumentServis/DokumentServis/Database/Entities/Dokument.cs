using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DokumentServis.Database.Entities
{
    /// <summary>
    /// Dokument klasa
    /// </summary>
    public class Dokument
    {
        /// <summary>
        /// Id dokumenta, primarni kljuc
        /// </summary>
        public Guid DokumentID { get; set; }

        /// <summary>
        /// Zavodni broj dokumenta
        /// </summary>
        public string ZavodniBroj { get; set; }

        /// <summary>
        /// Datum generisanja dokumenta
        /// </summary>
        public DateTime Datum { get; set; }

        /// <summary>
        /// Datum donosenja donosenja dokumenta
        /// </summary>
        public DateTime DatumDonosenja { get; set; }

        /// <summary>
        /// Sablon koji definise parametre kao sto su FontFamily, FontSize i slicno
        /// </summary>
        public string Sablon { get; set; }

        /// <summary>
        /// Id korisnika
        /// </summary>
        public Guid KorisnikID { get; set; }

        /// <summary>
        /// Id kupca
        /// </summary>
        public Guid KupacID { get; set; }

        /// <summary>
        /// Id licitera
        /// </summary>
        public Guid LiciterID { get; set; }

        /// <summary>
        /// Id verzije dokumenta
        /// </summary>
        [ForeignKey("VerzijaDokumentaID")]
        public Guid VerzijaDokumentaID { get; set; }



    }
}
