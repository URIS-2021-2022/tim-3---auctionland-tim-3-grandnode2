using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Entities
{
    /// <summary>
    /// Entity za Službeni list
    /// </summary>
    public class SluzbeniListEntity
    {

        /// <summary>
        /// ID službenog lista
        /// </summary>
        [Key]
        public Guid SluzbeniListId { get; set; }

        /// <summary>
        /// Opština u okviru koje je izdat službeni list
        /// </summary>
        [Required]
        [MaxLength(30)]
        public String Opstina { get; set; }

        /// <summary>
        /// Broj službenog lista
        /// </summary>
        [Required]
        public int BrojSluzbenogLista { get; set; }

        /// <summary>
        /// Datum izdavanja službenog lista
        /// </summary>
        [Required]
        public DateTime DatumIzdavanja { get; set; }
    }
}
