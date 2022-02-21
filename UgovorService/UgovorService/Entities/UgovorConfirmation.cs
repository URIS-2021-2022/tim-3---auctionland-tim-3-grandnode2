using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UgovorService.Entities
{
    public class UgovorConfirmation
    {
        /// <summary>
        /// ID ugovora
        /// </summary>
        public Guid UgovorId { get; set; }

        /// <summary>
        /// ID kupca
        /// </summary>
        public Guid LiceId { get; set; }
    }
}
