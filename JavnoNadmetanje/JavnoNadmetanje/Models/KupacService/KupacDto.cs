using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Models.KupacService
{
    public class KupacDto
    {
        /// <summary>
        /// ID kupca
        /// </summary>
        public Guid KupacId { get; set; }

        /// <summary>
        /// Ostvarena površina
        /// </summary>
        public int OstvarenaPovrsina { get; set; }

        /// <summary>
        /// Da li kupac ima zabranu
        /// </summary>        
        public bool ImaZabranu { get; set; }

        /// <summary>
        /// Datum početka zabrane
        /// </summary>
        public DateTime? DatumPocetkaZabrane { get; set; }

        /// <summary>
        /// Dužina trajanja zabrane u godinama
        /// </summary>
        public int? DuzinaTrajanjaZabraneUGodinama { get; set; }
    }
}
