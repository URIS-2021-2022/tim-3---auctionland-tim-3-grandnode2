using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KupacService.DTO
{
    public class PravnoLiceUpdateDto
    {
        public string MaticniBroj { get; set; }
        public Guid AdresaId { get; set; }
        public Guid KontaktOsobaId { get; set; }
        public string BrojTelefona_1 { get; set; }
        public string BrojTelefona_2 { get; set; }
        public string Faks { get; set; }
        public string Email { get; set; }
        public string BrojRacuna { get; set; }
    }
}
