using licitacijaService.DTOs.Mock;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace licitacijaService.DTOs
{
    public class LicitacijaDokumentConfirmationDto
    {

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

        /// <summary>
        /// Dokument
        /// </summary>
        [Required]
        public ResponseDokument dokument{ get; set; }
    }
}
