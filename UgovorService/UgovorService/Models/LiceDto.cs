using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UgovorService.Models
{
    public class LiceDto
    {
        /// <summary>
        /// Id lica- kupca
        /// </summary>
        public Guid KupacId { get; set; }
        /// <summary>
        /// Ostvarena povrsina
        /// </summary>
        public int OstvarenaPovrsina { get; set; }
        /// <summary>
        /// Da li ima zabranu
        /// </summary>
        public bool ImaZabranu { get; set; }
    }
}
