using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UgovorService.Models
{
    public class LicitacijaDto
    {
        /// <summary>
        /// Broj licitacije
        /// </summary>
        public int brojLicitacije { get; set; }

        /// <summary>
        /// Godina licitacije
        /// </summary>
        public int goidna { get; set; }

        /// <summary>
        /// Datum licitacije
        /// </summary>
        public DateTime datumLicitacije { get; set; }

        /// <summary>
        /// Ogranicenje licitacije
        /// </summary>
        public int ogranicenjeLicitacije { get; set; }

        /// <summary>
        /// Korak cene
        /// </summary>
        public int korakCene { get; set; }

        /// <summary>
        /// Rok za dostavu prijava
        /// </summary>
        public DateTime rokZaDostavuPrijava { get; set; }
    }
}
