using licitacijaService.DTOs.Mock;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace licitacijaService.DTOs
{
    public class LicitacijaVrstaDokumentaDTO
    {
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
        public ResponseDokument dokument { get; set; }
    }
}
