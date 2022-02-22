using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zalba.Models
{
    public class TipZalbeConfirmationDto
    {
        /// <summary>
        /// ID tipa zalbe
        /// </summary>
        public Guid TipZalbeID { get; set; }

        /// <summary>
        /// Naziv tipa zalbe
        /// </summary>
        public string NazivTipa { get; set; }
    }
}
