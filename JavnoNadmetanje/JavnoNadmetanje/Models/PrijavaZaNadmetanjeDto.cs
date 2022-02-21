using JavnoNadmetanje.Models.UplataService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Models
{
    /// <summary>
    /// DTO za Prijavu za nadmetanje
    /// </summary>
    public class PrijavaZaNadmetanjeDto
    {
        /// <summary>
        /// ID prijave za nadmetanje
        /// </summary>
        public Guid PrijavaZaNadmetanjeId { get; set; }

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
