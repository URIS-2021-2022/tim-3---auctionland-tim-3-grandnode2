using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Zalba.Entities
{
    public class TipZalbeE
    {
        /// <summary>
        /// ID tipa zalbe
        /// </summary>
        [Key]
        public Guid TipZalbeID { get; set; }

        /// <summary>
        /// Naziv tipa zalbe
        /// </summary>
        public string NazivTipa { get; set; }

        /// <summary>
        /// Opis tipa zalbe
        /// </summary>
        public string OpisTipa { get; set; }
    }

}
