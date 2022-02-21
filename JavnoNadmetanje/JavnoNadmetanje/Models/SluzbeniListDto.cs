using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Models
{
    /// <summary>
    /// DTO za Službeni list
    /// </summary>
    public class SluzbeniListDto
    {
        /// <summary>
        /// ID službenog lista
        /// </summary>
        public Guid SluzbeniListId { get; set; }

        /// <summary>
        /// Opština u okviru koje je izdat službeni list
        /// </summary>
        public String Opstina { get; set; }

        /// <summary>
        /// Broj službenog lista
        /// </summary>
        public int BrojSluzbenogLista { get; set; }

        /// <summary>
        /// Datum izdavanja službenog lista
        /// </summary>
        public DateTime DatumIzdavanja { get; set; }

    }
}
