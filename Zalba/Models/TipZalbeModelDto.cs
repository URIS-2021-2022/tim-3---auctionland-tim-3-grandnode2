using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zalba.Models
{
    public class TipZalbeModelDto
    {
        public Guid TipZalbeID { get; set; }

        public string NazivTipa { get; set; }

        public string OpisTipa { get; set; }
    }
}
