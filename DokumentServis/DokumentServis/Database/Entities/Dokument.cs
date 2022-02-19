using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DokumentServis.Database.Entities
{
    public class Dokument
    {
        public int DokumentID { get; set; }

        public string ZavodniBroj { get; set; }

        public DateTime Datum { get; set; }

        public DateTime DatumDonosenja { get; set; }

        public string Sablon { get; set; }

        public int KorisnikID { get; set; }

        public int KupacID { get; set; }

        public int LiciterID { get; set; }

        [ForeignKey("VerzijaDokumentaID")]
        public int VerzijaDokumentaID { get; set; }


    }
}
