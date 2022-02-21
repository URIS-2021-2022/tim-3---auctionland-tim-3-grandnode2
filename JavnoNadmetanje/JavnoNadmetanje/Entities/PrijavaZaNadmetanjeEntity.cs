using JavnoNadmetanje.Models.UplataService;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Entities
{
    /// <summary>
    /// Entity za Prijavu za nadmetanje
    /// </summary>
    public class PrijavaZaNadmetanjeEntity
    {
        /// <summary>
        /// ID prijave za nadmetanje
        /// </summary>
        [Key]
        public Guid PrijavaZaNadmetanjeId { get; set; }

        /// <summary>
        /// Datum prijave na javno nadmetanje
        /// </summary>
        [Required]
        public DateTime DatumPrijave { get; set; }

        /// <summary>
        /// Mesto prijave na javno nadmetanje
        /// </summary>
        [Required]
        [MaxLength(30)]
        public String MestoPrijave { get; set; }

        /// <summary>
        /// Strani ključ javnog nadmetanja
        /// </summary>
        [ForeignKey("JavnoNadmetanjeEntity")]
        public Guid JavnoNadmetanjeId { get; set; }

        /// <summary>
        /// Javno nadmetanje entitet
        /// </summary>
        public JavnoNadmetanjeEntity JavnoNadmetanje { get; set; }

        [NotMapped]
        public List<UplataDto> Uplate { get; set; }
    }
}
