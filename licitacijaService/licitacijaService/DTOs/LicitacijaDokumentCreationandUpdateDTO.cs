using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace licitacijaService.DTOs
{
    public class LicitacijaDokumentCreationandUpdateDto
    {
        /// <summary>
        /// Identifikator licitacije
        /// </summary>
        [Required(ErrorMessage = "Obavezan identifikator licitacije")]
        public Guid licitacijaId { get; set; }

        /// <summary>
        /// Identifikator dokumenta
        /// </summary>
        [Required(ErrorMessage = "Obavezan identifikator dokumenta")]
        public Guid dokumentId { get; set; }

        /// <summary>
        /// Indikator vrste podnosioca dokumenta
        ///P pravno lice, F fizicko lice
        /// </summary>
        [StringLength(1)]
        public String vrstaPodnosiocaDokumenta { get; set; }

    }
}
