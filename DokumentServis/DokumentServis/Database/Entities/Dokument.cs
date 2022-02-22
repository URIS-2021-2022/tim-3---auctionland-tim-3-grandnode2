using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DokumentServis.Database.Entities
{
    public class Dokument
    {
        public Guid DokumentID { get; set; }

        public string ZavodniBroj { get; set; }

        public DateTime Datum { get; set; }

        public DateTime DatumDonosenja { get; set; }

        public string Sablon { get; set; }

        public Guid KorisnikID { get; set; }

        public Guid KupacID { get; set; }

        public Guid LiciterID { get; set; }

        [ForeignKey("VerzijaDokumentaID")]
        public Guid VerzijaDokumentaID { get; set; }



    }
}
