using JavnoNadmetanje.Models.DokumentService;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Entities
{
    public class DokumentPrijavaZaNadmetanjeEntity
    {
        /// <summary>
        /// Strani ključ prijave za nadmetanje
        /// </summary>
        [ForeignKey("PrijavaZaNadmetanjeEntity")]
        public Guid PrijavaZaNadmetanjeId { get; set; }

        /// <summary>
        /// Prijava za nadmetanje entitet
        /// </summary>
        public PrijavaZaNadmetanjeEntity PrijavaZaNadmetanje { get; set; }

        /// <summary>
        /// ID dokumenta
        /// </summary>
        [Required]
        public Guid DokumentId { get; set; }

        /// <summary>
        /// Datum donošenja dokumenta
        /// </summary>
        [Required]
        public DateTime DatumDonosenjaDokumenta { get; set; }

        [NotMapped]
        public ResponseDokumentDto Dokument { get; set; }

    }
}
