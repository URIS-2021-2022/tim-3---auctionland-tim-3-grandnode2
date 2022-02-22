using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KupacService.DTO
{
    public class FizickoLiceUpdateDto
    {
        public Guid FizickoLiceId { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Jmbg { get; set; }
        public Guid AdresaId { get; set; }
        public string BrojTelefona_1 { get; set; }
        public string BrojTelefona_2 { get; set; }
        public string Email { get; set; }
        public string BrojRacuna { get; set; }
    }
}
