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
        /// DokumentID primarni kljuc
        /// </summary>
        public Guid DokumentID { get; set; }

        /// <summary>
        /// Zavodni broj
        /// </summary>
        public string ZavodniBroj { get; set; }

        /// <summary>
        /// Datum
        /// </summary>
        public DateTime Datum { get; set; }

        /// <summary>
        /// Datum donosenja
        /// </summary>
        public DateTime DatumDonosenja { get; set; }

        /// <summary>
        /// Sablon
        /// </summary>
        public string Sablon { get; set; }

        /// <summary>
        /// KorisnikID
        /// </summary>
        public Guid KorisnikID { get; set; }

        /// <summary>
        /// KupacID
        /// </summary>
        public Guid KupacID { get; set; }

        /// <summary>
        /// LiciterID
        /// </summary>
        public Guid LiciterID { get; set; }

        /// <summary>
        /// VerzijaDokumentaID
        /// </summary>
        [ForeignKey("VerzijaDokumentaID")]
        public Guid VerzijaDokumentaID { get; set; }



    }
}
