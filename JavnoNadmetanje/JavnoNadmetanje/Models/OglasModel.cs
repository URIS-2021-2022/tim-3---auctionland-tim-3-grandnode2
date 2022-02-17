﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Models
{
    public class OglasModel
    {
        public Guid OglasId { get; set; }

        public DateTime DatumObjavljivanjaOglasa { get; set; }

        public int GodinaObjavljivanjaOglasa { get; set; }

        public List<int> TipGarantaPlacanja { get; set; }

    }
}
