using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KatastarskaOpstina.Entities
{
    public class KatastarskaOpstinaConfirmation
    {
        /// <summary>
        /// ID katastarske opstine
        /// </summary>
        public Guid KatastarskaOpstinaID { get; set; }
        #region

        /// <summary>
        /// Naziv opstine
        /// </summary>
        public string NazivOpstine { get; set; }
        #endregion
    }
}
