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
        /// <summary>
        /// ID dela parcele
        /// </summary>
        [Key]
        public Guid DeoParceleId { get; set; }
        
        /// <summary>
        /// Strani ključ parcele
        /// </summary>
        [ForeignKey("Parcela")]
        public Guid ParcelaId { get; set; }

        /// <summary>
        /// Parcela entitet
        /// </summary>
        public Parcela Parcela { get; set; }

        /// <summary>
        /// Redni broj dela parcele
        /// </summary>
        [Required]
        public int RedniBrojDelaParcele { get; set; }

        /// <summary>
        /// Površina dela parcele
        /// </summary>
        [Required]
        public int PovrsinaDelaParcele { get; set; }

        /// <summary>
        /// Strani ključ kvaliteta zemljišta
        /// </summary>
        [ForeignKey("KvalitetZemljista")]
        public Guid KvalitetZemljistaId { get; set; }

        /// <summary>
        /// Kvalitet zemljišta entitet
        /// </summary>
        public KvalitetZemljista KvalitetZemljista { get; set; }
    }
}
