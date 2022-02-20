using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelaService.Entities
{
    public class DeoParcele
    {
        [Key]
        public Guid DeoParceleId { get; set; }
        [ForeignKey("Parcela")]
        public Guid ParcelaId { get; set; }
        public Parcela Parcela { get; set; }
        [Required]
        public int RedniBrojDelaParcele { get; set; }
        [Required]
        public int PovrsinaDelaParcele { get; set; }
        [ForeignKey("KvalitetZemljista")]
        public Guid KvalitetZemljistaId { get; set; }
        public KvalitetZemljista KvalitetZemljista { get; set; }
    }
}
