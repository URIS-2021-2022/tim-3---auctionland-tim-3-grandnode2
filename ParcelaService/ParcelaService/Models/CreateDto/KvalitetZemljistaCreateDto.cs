using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelaService.Models.CreateDto
{
    public class KvalitetZemljistaCreateDto
    {
        /// <summary>
        /// Oznaka kvaliteta zemljišta
        /// </summary>
        public string OznakaKvaliteta { get; set; }

        /// <summary>
        /// Opis kvaliteta zemljišta
        /// </summary>
        public string Opis { get; set; }
    }
}
