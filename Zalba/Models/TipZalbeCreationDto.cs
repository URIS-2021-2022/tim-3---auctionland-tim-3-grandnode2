using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Zalba.Models
{
    public class TipZalbeCreationDto
    {
        #region
        /// <summary>
        /// Naziv tipa zalbe
        /// </summary>
        public string NazivTipa { get; set; }

        /// <summary>
        /// Opis tipa zalbe
        /// </summary>
        public string OpisTipa { get; set; }

        #endregion

        
    }
}
