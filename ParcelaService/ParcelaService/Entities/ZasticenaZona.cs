using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelaService.Entities
{
    public class ZasticenaZona
    {
        /// <summary>
        /// ID zaštićene zone
        /// </summary>
        [Key]
        public Guid ZasticenaZonaId { get; set; }

        /// <summary>
        /// Broj zaštićene zone
        /// </summary>
        [Required]
        public int BrojZasticeneZone { get; set; }

        /// <summary>
        /// Lista dozvoljenih radova
        /// </summary>
        [Required]
        public List<DozvoljeniRad> DozvoljeniRadovi { get; set; }
    }
}
