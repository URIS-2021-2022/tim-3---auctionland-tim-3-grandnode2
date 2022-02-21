using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Models.ParcelaService
{
    public class DeoParceleDto
    {
        /// <summary>
        /// ID dela parcele
        /// </summary>
        public Guid DeoParceleId { get; set; }

        /// <summary>
        /// ID parcele
        /// </summary>
        public Guid ParcelaId { get; set; }

        /// <summary>
        /// Redni broj dela parcele
        /// </summary>
        public int RedniBrojDelaParcele { get; set; }

        /// <summary>
        /// Površina dela parcele
        /// </summary>
        public int PovrsinaDelaParcele { get; set; }

        /// <summary>
        /// ID kvaliteta zemljišta
        /// </summary>
        public Guid KvalitetZemljistaId { get; set; }
    }
}
