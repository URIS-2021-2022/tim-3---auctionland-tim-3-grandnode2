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
        /// <summary>
        /// ID dozvoljenog rada
        /// </summary>
        [Key]
        public Guid DozvoljeniRadId { get; set; }

        /// <summary>
        /// Opis dozvoljenog rada
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Opis { get; set; }

        /// <summary>
        /// Strani ključ zaštićene zone
        /// </summary>
        [ForeignKey("ZasticenaZona")]
        public Guid ZasticenaZonaId { get; set; }

        /// <summary>
        /// Zaštićena zona entitet
        /// </summary>
        public ZasticenaZona ZasticenaZona { get; set; }
    }
}
