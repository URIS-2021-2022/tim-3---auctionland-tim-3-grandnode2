using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelaService.Entities
{
    public class ZasticenaZona
    {
        [Key]
        public Guid ZasticenaZonaId { get; set; }
        [Required]
        public int BrojZasticeneZone { get; set; }
        [Required]
        public List<DozvoljeniRad> DozvoljeniRadovi { get; set; }
    }
}
