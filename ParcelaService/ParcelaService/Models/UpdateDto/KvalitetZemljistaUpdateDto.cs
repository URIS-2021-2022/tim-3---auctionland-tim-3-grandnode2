using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelaService.Models.UpdateDto
{
    public class KvalitetZemljistaUpdateDto
    {
        public Guid KvalitetZemljistaId { get; set; }
        public string OznakaKvaliteta { get; set; }
        public string Opis { get; set; }
    }
}
