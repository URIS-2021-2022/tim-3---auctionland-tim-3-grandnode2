using JavnoNadmetanje.Models.DokumentService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Models
{
    public class DokumentPrijavaZaNadmetanjeDto
    {

        /// <summary>
        /// ID prijave za nadmetanje
        /// </summary>
        public Guid PrijavaZaNadmetanjeId { get; set; }

        /// <summary>
        /// Dokument
        /// </summary>
        public ResponseDokumentDto Dokument { get; set; }

        /// <summary>
        /// Datum donošenja dokumenta
        /// </summary>
        public DateTime DatumDonosenjaDokumenta { get; set; }
    }
}
