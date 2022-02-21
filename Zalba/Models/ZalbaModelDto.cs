using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Zalba.Models
{
    public class ZalbaModelDto
    {
        public Guid ZalbaID { get; set; }
        #region

        public Guid TipZalbeID{ get; set; }
        public PodnosilacZalbeDto PodnosilacZalbe { get; set; }
        public LicitacijaDto Licitacija { get; set; }
        public DateTime DatPodnosenjaZalbe { get; set; }
        public string Obrazlozenje { get; set; }
        public DateTime DatResenja { get; set; }
        public int BrojResenja { get; set; }
        public string StatusZalbe { get; set; }
        public int BrojOdluke { get; set; }
        public string RadnjaZalbe { get; set; }

        #endregion

    }
}
