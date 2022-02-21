using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Zalba.Entities
{
    public class TipZalbeE
    {
        [Key]
        public Guid TipZalbeID { get; set; }

        public string NazivTipa { get; set; }

        public string OpisTipa { get; set; }
    }

}
