using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Models
{
    public class OglasDto
    {
        public DateTime DatumObjavljivanjaOglasa { get; set; }

        public int GodinaObjavljivanjaOglasa { get; set; }

        public List<int> TipGarantaPlacanja { get; set; }

    }
}
