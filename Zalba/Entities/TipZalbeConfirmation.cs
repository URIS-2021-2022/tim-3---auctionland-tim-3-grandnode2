using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zalba.Entities
{
    public class TipZalbeConfirmation
    {
        /// <summary>
        /// ID tipa zalbe
        /// </summary>
        public Guid TipZalbeID { get; set; }

        /// <summary>
        /// Naziv tipa
        /// </summary>
        public string NazivTipa { get; set; }
    }
}
