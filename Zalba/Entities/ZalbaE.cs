using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Zalba.Models;

namespace Zalba.Entities
{
    public class ZalbaE
    {
        /// <summary>
        /// ID zalbe
        /// </summary>
        [Key]
        public Guid ZalbaID { get; set; }
        #region
        /// <summary>
        /// ID tipa zalbe
        /// </summary>
        [ForeignKey("TipZalbeE")]
        public Guid TipZalbeID { get; set; }
        public TipZalbeE TipZalbe {get; set;}
        /// <summary>
        /// ID podnosioca zalbe
        /// </summary>
        [Required]
        public Guid PodnosilacZalbeID { get; set; } //Kupac
        [NotMapped]
        public PodnosilacZalbeDto PodnosilacZalbe { get; set; }
        /// <summary>
        /// ID licitacije
        /// </summary>
        [Required]
        public Guid LicitacijaID { get; set; }
        [NotMapped]
        public LicitacijaDto Licitacija { get; set; }

        /// <summary>
        /// Datum podnosenja zalbe
        /// </summary>
        public DateTime DatPodnosenjaZalbe { get; set; }

        /// <summary>
        /// Tekstualno obrazlozenje zalbe
        /// </summary>
        public string Obrazlozenje { get; set; }

        /// <summary>
        /// Datum resenja zalbe
        /// </summary>
        public DateTime DatResenja { get; set; }

        /// <summary>
        /// Broj resenja zalbe
        /// </summary>
        public int BrojResenja { get; set; }

        /// <summary>
        /// Status zalbe
        /// </summary>
        public string StatusZalbe { get; set; }

        /// <summary>
        /// Broj odluke zalbe
        /// </summary>
        public int BrojOdluke { get; set; }

        /// <summary>
        /// Radnja zalbe
        /// </summary>
        public string RadnjaZalbe { get; set; }
        #endregion
    }
}
