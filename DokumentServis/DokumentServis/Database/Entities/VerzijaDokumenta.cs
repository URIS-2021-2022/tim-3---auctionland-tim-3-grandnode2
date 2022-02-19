using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DokumentServis.Database.Entities
{
    public class VerzijaDokumenta
    {
        public int VerzijaDokumentaID { get; set; }

        public string Verzija { get; set; }

        public string Revizija { get; set; }

        public DateTime Datum { get; set; }
    }
}
