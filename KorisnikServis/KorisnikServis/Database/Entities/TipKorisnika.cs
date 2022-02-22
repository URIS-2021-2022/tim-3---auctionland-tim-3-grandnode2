using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KorisnikServis.Database.Entities
{
    public class TipKorisnika
    {
        public Guid TipKorisnikaID { get; set; }
        public string NazivTipa { get; set; }
    }
}
