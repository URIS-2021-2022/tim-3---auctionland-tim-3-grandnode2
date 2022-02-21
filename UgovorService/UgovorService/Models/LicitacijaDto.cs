using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UgovorService.Models
{
    public class LicitacijaDto
    {
        /// <summary>
        /// ID licitacije
        /// </summary>
        public Guid LicitacijaId { get; set; }

        /// <summary>
        /// Broj licitacije
        /// </summary>
        public int BrojLicitacije { get; set; }

        /// <summary>
        /// Datum licitacije
        /// </summary>
        public DateTime DatumLicitacije { get; set; }
    }
}
