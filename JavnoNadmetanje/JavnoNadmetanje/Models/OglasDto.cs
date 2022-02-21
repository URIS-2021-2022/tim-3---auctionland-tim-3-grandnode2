using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Models
{
    /// <summary>
    /// DTO za Oglas
    /// </summary>
    public class OglasDto
    {
        /// <summary>
        /// ID Oglasa
        /// </summary>
        public Guid OglasId { get; set; }

        /// <summary>
        /// Datum objavljivanja Oglasa
        /// </summary>
        public DateTime DatumObjavljivanjaOglasa { get; set; }

        /// <summary>
        /// Godina objavljivanja Oglasa
        /// </summary>
        public int GodinaObjavljivanjaOglasa { get; set; }

        /// <summary>
        ///ID službenog lista
        /// </summary>
        public Guid SluzbeniListId { get; set; }
    }
}
