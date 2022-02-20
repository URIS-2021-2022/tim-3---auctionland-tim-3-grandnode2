using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelaService.Models
{
    public class KvalitetZemljistaDto
    {
        public Guid KvalitetZemljistaId { get; set; }
        public string OznakaKvaliteta { get; set; }
        public string Opis { get; set; }
    }
}
