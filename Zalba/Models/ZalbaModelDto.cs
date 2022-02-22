using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Zalba.Models
{
    public class ZalbaModelDto
    {
        /// <summary>
        /// ID zalbe
        /// </summary>
        public Guid ZalbaID { get; set; }
        #region
        /// <summary>
        /// ID tipa zalbe
        /// </summary>
        public Guid TipZalbeID { get; set; }
        /// <summary>
        /// ID podnosioca zalbe
        /// </summary>
        public PodnosilacZalbeDto PodnosilacZalbe { get; set; }
        /// <summary>
        /// Licitacija
        /// </summary>
        public LicitacijaDto Licitacija { get; set; }
        /// <summary>
        /// Datum podnosenja zalbe
        /// </summary>
        public DateTime DatPodnosenjaZalbe { get; set; }
        /// <summary>
        /// Obrazlozenje zalbe
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
        /// Broj odluka zalbe
        /// </summary>
        public int BrojOdluke { get; set; }
        /// <summary>
        /// Radnja zalbe
        /// </summary>
        public string RadnjaZalbe { get; set; }

        #endregion

    }
}
