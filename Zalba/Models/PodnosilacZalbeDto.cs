using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zalba.Models
{
    public class PodnosilacZalbeDto
    {
        public Guid KupacId { get; set; }
        public int OstvarenaPovrsina { get; set; }
        public bool ImaZabranu { get; set; }

    }
}
