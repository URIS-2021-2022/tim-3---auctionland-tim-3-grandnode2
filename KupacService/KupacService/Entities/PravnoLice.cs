using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KupacService.Entities
{
    public class PravnoLice
    {
        [Key]
        [Required]
        public Guid PravnoLiceId { get; set; }
        public string MaticniBroj { get; set; }
        [NotMapped]
        public Guid AdresaId { get; set; }
        [NotMapped]
        public Guid KontaktOsobaId { get; set; }
        public string BrojTelefona_1 { get; set; }
        public string BrojTelefona_2 { get; set; }
        public string Faks { get; set; }
        public string Email { get; set; }
        public string BrojRacuna { get; set; }

    }
}
