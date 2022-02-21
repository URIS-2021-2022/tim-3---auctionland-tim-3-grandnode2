using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelaService.Models.UpdateDto
{
    public class DozvoljeniRadUpdateDto
    {
        /// <summary>
        /// ID dozvoljenog rada
        /// </summary>
        public Guid DozvoljeniRadId { get; set; }

        /// <summary>
        /// Opis dozvoljenog rada
        /// </summary>
        public string Opis { get; set; }

        /// <summary>
        /// ID zaštićene zone
        /// </summary>
        public Guid ZasticenaZonaId { get; set; }
    }
}
