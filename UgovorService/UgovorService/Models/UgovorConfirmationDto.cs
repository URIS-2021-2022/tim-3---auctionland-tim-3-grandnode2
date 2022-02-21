using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UgovorService.Models
{
    public class UgovorConfirmationDto
    {
        /// <summary>
        /// ID ugovora
        /// </summary>
        public Guid UgovorId { get; set; }

        /// <summary>
        /// ID kupca
        /// </summary>
        public Guid LiceId { get; set; } //ime prezime ako moze
    }
}
