using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KatastarskaOpstina.Entities
{
    public class StatutOpstineE
    {
        /// <summary>
        /// ID statuta opstine
        /// </summary>
        [Key]
        public Guid StatutOpstineID { get; set; }

        #region
        /// <summary>
        /// Sadrzaj statuta opstine
        /// </summary>
        public string SadrzajStatuta { get; set; }

        /// <summary>
        /// Datum kreiranja statuta opstine
        /// </summary>
        public DateTime DatumKreiranjaStatuta { get; set; }
        #endregion
    }
}
