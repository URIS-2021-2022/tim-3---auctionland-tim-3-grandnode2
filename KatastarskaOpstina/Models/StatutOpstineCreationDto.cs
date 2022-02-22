using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KatastarskaOpstina.Models
{
    public class StatutOpstineCreationDto
    {
        /// <summary>
        /// Sadrzaj statuta opstine
        /// </summary>
        public string SadrzajStatuta { get; set; }

        /// <summary>
        /// Datum kreiranja statuta opstine
        /// </summary>
        public DateTime DatumKreiranjaStatuta { get; set; }
    }
}
