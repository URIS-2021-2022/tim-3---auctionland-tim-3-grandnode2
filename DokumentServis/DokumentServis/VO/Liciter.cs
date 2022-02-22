using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DokumentServis.VO
{
    public class Liciter
    {
        public Guid KupacId { get; set; }

        public int OstvarenaPovrsina { get; set; }

        public bool ImaZabranu { get; set; }

        public string BrojLicence { get; set; }
    }
}
