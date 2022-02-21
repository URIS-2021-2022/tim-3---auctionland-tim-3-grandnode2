using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Models
{
    public class SluzbeniListCreateDto
    {
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
