using ParcelaService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelaService.Models
{
    public class DeoParceleDto
    {
        public Guid DeoParceleId { get; set; }
        public Guid ParcelaId { get; set; }
        public int RedniBrojDelaParcele { get; set; }
        public int PovrsinaDelaParcele { get; set; }
        public Guid KvalitetZemljistaId { get; set; }
    }
}
