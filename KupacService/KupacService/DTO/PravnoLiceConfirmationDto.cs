using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KupacService.DTO
{
    public class PravnoLiceConfirmationDto
    {
        public Guid PravnoLiceId { get; set; }
        public string MaticniBroj { get; set; }
    }
}
