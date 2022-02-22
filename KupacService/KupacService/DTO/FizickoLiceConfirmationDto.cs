using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KupacService.DTO
{
    public class FizickoLiceConfirmationDto
    {
        public Guid FizickoLiceId { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
    }
}
