using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zalba.Entities
{
    public class ZalbaConfirmation
    {
        public Guid ZalbaID { get; set; }

        public Guid PodnosilacZalbeID { get; set; }

        public DateTime DatPodnosenjaZalbe { get; set; }
    }
}
