using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zalba.Models
{
    public class ZalbaConfirmationDto
    {
        /// <summary>
        /// ID zalbe
        /// </summary>
        public Guid ZalbaID { get; set; }

        /// <summary>
        /// ID podnosioca zalbe
        /// </summary>
        public Guid PodnosilacZalbeID { get; set; }

        /// <summary>
        /// Datum podnosenja zalbe
        /// </summary>
        public DateTime DatPodnosenjaZalbe { get; set; }
    }

}
