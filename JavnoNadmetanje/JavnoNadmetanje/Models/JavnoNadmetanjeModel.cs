using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Models
{
    public class JavnoNadmetanjeModel
    {
        public Guid JavnoNadmetanjeId { get; set; }

        public DateTime Datum { get; set; }

        public DateTime VremePocetka { get; set; }

        public DateTime VremeKraja { get; set; }

        public int PocetnaCenaPoHektaru { get; set; }

        public Boolean Izuzeto { get; set; }

        public Enum Tip { get; set; }

        public int IzlicitiranaCena { get; set; }

        public int PeriodZakupa { get; set; }

        public int BrojUcesnika { get; set; }

        public int VisinaDopuneDepozita { get; set; }

        public int Krug { get; set; }

        public Enum Status { get; set; }

    }
}
