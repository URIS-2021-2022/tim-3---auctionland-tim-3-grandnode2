using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelaService.Models
{
    public class ZasticenaZonaDto
    {
        /// <summary>
        /// ID zaštićene zone
        /// </summary>
        public Guid ZasticenaZonaId { get; set; }

        /// <summary>
        /// Broj zaštićene zone
        /// </summary>
        public int BrojZasticeneZone { get; set; }
    }
}
