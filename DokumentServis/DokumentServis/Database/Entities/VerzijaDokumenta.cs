using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DokumentServis.Database.Entities
{
    /// <summary>
    /// Verzija dokumenta klasa
    /// </summary>
    public class VerzijaDokumenta
    {
        /// <summary>
        /// Id verzije dokumenta, primarni kljuc
        /// </summary>
        public Guid VerzijaDokumentaID { get; set; }

        /// <summary>
        /// Verzija dokumenta
        /// </summary>
        public string Verzija { get; set; }

        /// <summary>
        /// Revizija dokumenta
        /// </summary>
        public string Revizija { get; set; }

        /// <summary>
        /// Datum verzije dokumenta
        /// </summary>
        public DateTime Datum { get; set; }
    }
}
