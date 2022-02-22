using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Models.DokumentService
{
    public class DokumentDto
    {
        /// <summary>
        /// ID dokumenta
        /// </summary>
        public int DokumentID { get; set; }

        /// <summary>
        /// Zavodni broj
        /// </summary>
        public string ZavodniBroj { get; set; }

        /// <summary>
        /// Datum 
        /// </summary>
        public DateTime Datum { get; set; }

        /// <summary>
        /// Datum donošenja dokumenta
        /// </summary>
        public DateTime DatumDonosenja { get; set; }

        /// <summary>
        /// Šablon
        /// </summary>
        public string Sablon { get; set; }
    }
}
