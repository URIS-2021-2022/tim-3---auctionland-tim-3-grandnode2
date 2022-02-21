using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Uplata.Entities
{
    public class BankaEntity
    {
        /// <summary>
        /// ID banke
        /// </summary>
        [Key]
        public Guid BankaId { get; set; }

        /// <summary>
        /// Naziv banke
        /// </summary>
        [Required]
        [MaxLength(30)]
        public String NazivBanke { get; set; }

        /// <summary>
        /// Adresa banke
        /// </summary>
        [Required]
        [MaxLength(30)]
        public String Adresa { get; set; }

        /// <summary>
        /// Grad banke
        /// </summary>
        [Required]
        [MaxLength(30)]
        public String Grad { get; set; }

    }
}
