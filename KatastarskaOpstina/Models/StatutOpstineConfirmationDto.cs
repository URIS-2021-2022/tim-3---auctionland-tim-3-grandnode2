using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KatastarskaOpstina.Models
{
    public class StatutOpstineConfirmationDto
    {
        /// <summary>
        /// ID statuta opstine
        /// </summary>
        public Guid StatutOpstineID { get; set; }

        #region
        /// <summary>
        /// Sadrzaj statuta opstine
        /// </summary>
        public string SadrzajStatuta { get; set; }

        #endregion
    }
}
