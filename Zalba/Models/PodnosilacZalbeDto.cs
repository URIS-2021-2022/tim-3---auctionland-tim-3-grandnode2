using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zalba.Models
{
    public class PodnosilacZalbeDto
    {
        /// <summary>
        /// ID podnosioca zalbe
        /// </summary>
        public Guid PodnosilacZalbeID { get; set; }
        /// <summary>
        /// Ime podnosioca zalbe
        /// </summary>
        public string Ime { get; set; }
        /// <summary>
        /// Prezime podnosioca zalbe
        /// </summary>
        public string Prezime { get; set; }
    }
}
