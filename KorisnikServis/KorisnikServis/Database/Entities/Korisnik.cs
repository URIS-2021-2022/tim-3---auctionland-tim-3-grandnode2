using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KorisnikServis.Database.Entities
{
    public class Korisnik
    {
        public int KorisnikID { get; set; }
        public string ImeKorisnika { get; set; }
        public string PrezimeKorisnika { get; set; }
        public string KorisnickoIme { get; set; }
        public string Lozinka { get; set; }

        [ForeignKey("TipKorisnikaID")]
        public int TipKorisnikaID { get; set; }

    }
}
