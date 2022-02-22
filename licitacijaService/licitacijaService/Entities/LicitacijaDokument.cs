using licitacijaService.DTOs.Mock;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace licitacijaService.Entities
{
    public class LicitacijaDokument
    {
        /// <summary>
        /// Identifikator licitacije
        /// </summary>
        [Required]
        [ForeignKey("licitacijaId")]
        public Guid licitacijaId { get; set; }

        /// <summary>
        /// Identifikator dokumenta
        /// </summary>
        [Required]
        public Guid dokumentId { get; set; }

        /// <summary>
        /// Dokument
        /// </summary>
        [NotMapped]
        public ResponseDokument dokument { get; set; }

        /// <summary>
        /// Indikator vrste podnosioca dokumenta
        ///P pravno lice, F fizicko lice
        /// </summary>
        [StringLength(1)]
        public String vrstaPodnosiocaDokumenta { get; set; }

        /// <summary>
        /// Datum podnosenja dokumenta
        ///P pravno lice, F fizicko lice
        /// </summary>
        [Required]
        public DateTime datumPodnosenjaDokumenta { get; set; }

       
    }
}
