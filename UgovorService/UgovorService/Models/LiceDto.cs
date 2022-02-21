using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UgovorService.Models
{
    public class LiceDto
    {
        /// <summary>
        /// ID kupca
        /// </summary>
        public Guid LiceId { get; set; }

        /// <summary>
        /// Ime kupca
        /// </summary>
        public string Ime { get; set; }

        /// <summary>
        /// Prezime kupca
        /// </summary>
        public string Prezime { get; set; }
    }
}
