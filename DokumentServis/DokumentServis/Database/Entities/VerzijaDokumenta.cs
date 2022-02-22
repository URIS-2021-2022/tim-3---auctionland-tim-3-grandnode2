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
        /// VerzijaDokumenta ID
        /// </summary>
        public Guid VerzijaDokumentaID { get; set; }

        /// <summary>
        /// Verzija
        /// </summary>
        public string Verzija { get; set; }

        /// <summary>
        /// Revizija
        /// </summary>
        public string Revizija { get; set; }

        /// <summary>
        /// Datum
        /// </summary>
        public DateTime Datum { get; set; }
    }
}
