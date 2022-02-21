using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Uplata.Models
{
    public class BankaCreateDto
    {
        /// <summary>
        /// Naziv banke
        /// </summary>
        public String NazivBanke { get; set; }

        /// <summary>
        /// Adresa banke
        /// </summary>
        public String Adresa { get; set; }

        /// <summary>
        /// Grad banke
        /// </summary>
        public String Grad { get; set; }
    }
}
