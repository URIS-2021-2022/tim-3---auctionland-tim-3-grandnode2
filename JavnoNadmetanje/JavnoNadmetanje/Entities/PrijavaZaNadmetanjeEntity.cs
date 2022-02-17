using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Entities
{
    public class PrijavaZaNadmetanjeEntity
    {
        public Guid PrijavaZaNadmetanjeId { get; set; }

        public DateTime DatumPrijave { get; set; }

        public String MestoPrijave { get; set; }
    }
}
