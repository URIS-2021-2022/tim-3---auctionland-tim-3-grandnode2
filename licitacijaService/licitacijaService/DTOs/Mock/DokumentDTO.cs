using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace licitacijaService.DTOs.Mock
{
    public class DokumentDTO
    {
        public int DokumentID { get; set; }

        public string ZavodniBroj { get; set; }

        public DateTime Datum { get; set; }

        public DateTime DatumDonosenja { get; set; }

        public string Sablon { get; set; }

        public int KorisnikID { get; set; }

        public int KupacID { get; set; }

        public int LiciterID { get; set; }

    }
}
