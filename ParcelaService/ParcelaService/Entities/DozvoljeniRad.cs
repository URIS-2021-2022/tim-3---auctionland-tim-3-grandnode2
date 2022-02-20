using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelaService.Entities
{
    public class DozvoljeniRad
    {
        [Key]
        public Guid DozvoljeniRadId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Opis { get; set; }
        [ForeignKey("ZasticenaZona")]
        public Guid ZasticenaZonaId { get; set; }
        public ZasticenaZona ZasticenaZona { get; set; }
    }
}
