using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelaService.Models
{
    public class DozvoljeniRadDto
    {
        public Guid DozvoljeniRadId { get; set; }
        public string Opis { get; set; }
        public Guid ZasticenaZonaId { get; set; }
    }
}
