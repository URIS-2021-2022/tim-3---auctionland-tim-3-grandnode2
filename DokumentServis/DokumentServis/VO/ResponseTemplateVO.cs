using DokumentServis.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DokumentServis.VO
{
    public class ResponseTemplateVO
    {
        public Dokument dokument { get; set; }
        public Korisnik korisnik { get; set; }

        public Kupac kupac { get; set; }

        public Liciter liciter { get; set; }
    }
}
