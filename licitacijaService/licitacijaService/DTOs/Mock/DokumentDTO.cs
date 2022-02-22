using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace licitacijaService.DTOs.Mock
{
    public class DokumentDto
    {
        public string ZavodniBroj { get; set; }

        public DateTime Datum { get; set; }

        public DateTime DatumDonosenja { get; set; }

        public string Sablon { get; set; }

    }
}
