using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelaService.Entities
{
    public class KvalitetZemljista
    {
        [Key]
        public Guid KvalitetZemljistaId { get; set; }
        [Required]
        [MaxLength(5)]
        public string OznakaKvaliteta { get; set; }
        [Required]
        [MaxLength(100)]
        public string Opis { get; set; }
    }
}
