using KatastarskaOpstina.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KatastarskaOpstina.Models
{
    public class KatastarskaOpstinaModelDto
    {
        /// <summary>
        /// ID katastarske opstine
        /// </summary>
        public Guid KatastarskaOpstinaID { get; set; }
        #region
        /// <summary>
        /// Parcela
        /// </summary>
        public ParcelaDto Parcela { get; set; }
        /// <summary>
        /// ID statuta opstine
        /// </summary>
        public Guid StatutOpstineID { get; set; }

        /// <summary>
        /// Naziv opstine
        /// </summary>
        public string NazivOpstine { get; set; }

        #endregion
    }
}
