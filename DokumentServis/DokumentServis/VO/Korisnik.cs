using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DokumentServis.VO
{
    public class Korisnik
    {
        public Guid KorisnikID { get; set; }

        public string ImeKorisnika { get; set; }

        public string PrezimeKorisnika { get; set; }

        public string KorisnickoIme { get; set; }

        public string Lozinka { get; set; }

        public Guid TipKorisnikaID { get; set; }
    }
}
