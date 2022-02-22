using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DokumentServis.VO
{
    public class Kupac
    {
        public Guid KupacID { get; set; }

        public long OstvarenaPovrsina { get; set; }

        public bool ImaZabranu { get; set; }
    }
}
