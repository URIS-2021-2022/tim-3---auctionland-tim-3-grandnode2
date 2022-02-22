using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KatastarskaOpstina.Entities
{
    public class StatutOpstineConfirmation
    {
        /// <summary>
        /// ID statuta opstine
        /// </summary>
        public Guid StatutOpstineID { get; set; }

        /// <summary>
        /// Sadrzaj statuta opstine
        /// </summary>
        public string SadrzajStatuta { get; set; }

    }
}
