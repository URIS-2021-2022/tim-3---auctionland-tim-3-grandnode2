using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelaService.Entities
{
    public class KvalitetZemljista
    {
        /// <summary>
        /// ID kvalitet zemljišta
        /// </summary>
        [Key]
        public Guid KvalitetZemljistaId { get; set; }

        /// <summary>
        /// Oznaka kvaliteta zemljišta
        /// </summary>
        [Required]
        [MaxLength(5)]
        public string OznakaKvaliteta { get; set; }

        /// <summary>
        /// Opis kvaliteta zemljišta
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Opis { get; set; }
    }
}
