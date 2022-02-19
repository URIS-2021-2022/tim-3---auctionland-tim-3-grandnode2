using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace komisijaService.DTOs
{
    public class KomisijaCreationDto
    {

        /// <summary>
        /// Naziv komisije
        /// </summary>
        [StringLength(50)]
        [Required(ErrorMessage = "Naziv komisije je obavezan")]
        public String nazivKomisije { get; set; }

        /// <summary>
        /// Oznaka komisije
        /// </summary>
        [Required(ErrorMessage = "Oznaka komisije je obavezna")]
        public String oznakaKomisije { get; set; }

    }
}
