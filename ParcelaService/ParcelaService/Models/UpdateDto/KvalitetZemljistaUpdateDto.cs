using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelaService.Models.UpdateDto
{
    public class KvalitetZemljistaUpdateDto
    {
        /// <summary>
        /// ID kvaliteta zemljišta
        /// </summary>
        public Guid KvalitetZemljistaId { get; set; }

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
