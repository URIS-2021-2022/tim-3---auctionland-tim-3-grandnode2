using komisijaService.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace komisijaService.Entites
{
    public class Komisija
    {

        /// <summary>
        /// Identifikator komisije
        /// </summary>
        [Key]
        [Required]
        public Guid komisijaId { get; set; }

        /// <summary>
        /// Naziv komisije
        /// </summary>
        [StringLength(50)]
        [Required]
        public String nazivKomisije { get; set; }

        /// <summary>
        /// Oznaka komisije
        /// </summary>
        [Required]
        public String oznakaKomisije { get; set; }


        [NotMapped]
        public LicnostKomisije predsednikKomisije { get; set; }

        /// <summary>
        /// Clanovi komisije
        /// </summary>
      
        public ICollection<LicnostKomisije> clanoviKomisije { get; set; }
    }
}
