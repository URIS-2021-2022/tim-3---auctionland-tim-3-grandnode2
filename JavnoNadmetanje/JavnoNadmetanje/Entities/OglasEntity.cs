using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Entities
{
    /// <summary>
    /// Entity za Oglas
    /// </summary>
    public class OglasEntity
    {
        /// <summary>
        /// ID Oglasa
        /// </summary>
        [Key]
        public Guid OglasId { get; set; }

        /// <summary>
        /// Datum objavljivanja Oglasa
        /// </summary>
        [Required]
        public DateTime DatumObjavljivanjaOglasa { get; set; }

        /// <summary>
        /// Godina objavljivanja Oglasa
        /// </summary>
        [Required]
        public int GodinaObjavljivanjaOglasa { get; set; }

        /// <summary>
        /// Strani ključ službenog lista
        /// </summary>
        [ForeignKey("SluzbeniListEntity")]
        public Guid SluzbeniListId { get; set; }

        /// <summary>
        /// Službeni list entitet
        /// </summary>
        public SluzbeniListEntity SluzbeniList { get; set; }
    }
}
