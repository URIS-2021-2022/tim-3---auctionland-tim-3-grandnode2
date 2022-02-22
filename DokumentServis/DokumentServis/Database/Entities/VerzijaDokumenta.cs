using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DokumentServis.Database.Entities
{
    public class VerzijaDokumenta
    {
        public Guid VerzijaDokumentaID { get; set; }

        public string Verzija { get; set; }

        public string Revizija { get; set; }

        public DateTime Datum { get; set; }
    }
}
