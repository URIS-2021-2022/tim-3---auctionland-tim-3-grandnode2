using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KatastarskaOpstina.Models
{
    public class ParcelaDto
    {
        public Guid ParcelaId { get; set; }
        public string BrojParcele { get; set; }
        public string BrojListaNepokretnosti { get; set; }

    }
}
