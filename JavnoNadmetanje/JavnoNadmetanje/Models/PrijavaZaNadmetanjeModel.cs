using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Models
{
    public class PrijavaZaNadmetanjeModel
    {
        public Guid PrijavaZaNadmetanjeId { get; set; }

        public DateTime DatumPrijave { get; set; }

        public String MestoPrijave { get; set; }
    }
}
