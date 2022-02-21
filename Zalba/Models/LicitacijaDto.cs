using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zalba.Models
{
    public class LicitacijaDto
    {
        public Guid LicitacijaId { get; set; }
        public int BrojLicitacije { get; set; }
        public DateTime DatumLicitacije { get; set; }
    }
}
