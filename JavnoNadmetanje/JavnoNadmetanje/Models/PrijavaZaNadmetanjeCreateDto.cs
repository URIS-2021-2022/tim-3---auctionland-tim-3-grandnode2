using JavnoNadmetanje.Models.UplataService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Models
{
    public class PrijavaZaNadmetanjeCreateDto
    {
        /// <summary>
        /// Datum prijave na javno nadmetanje
        /// </summary>
        public DateTime DatumPrijave { get; set; }

        /// <summary>
        /// Mesto prijave na javno nadmetanje
        /// </summary>
        public String MestoPrijave { get; set; }

        /// <summary>
        /// ID javnog nadmetanja
        /// </summary>
        public Guid JavnoNadmetanjeId { get; set; }

        public ICollection<UplataDto> uplate { get; set; }
    }
}
