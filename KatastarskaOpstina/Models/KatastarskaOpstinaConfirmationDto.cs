using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KatastarskaOpstina.Models
{
    public class KatastarskaOpstinaConfirmationDto
    {
        /// <summary>
        /// ID katastarske opstine
        /// </summary>
        public Guid KatastarskaOpstinaID { get; set; }
        #region

        /// <summary>
        /// Naziv katastarske opstine
        /// </summary>
        public string NazivOpstine { get; set; }
        #endregion
    }
}
