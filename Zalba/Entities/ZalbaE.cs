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
        [Key]
        public Guid ZalbaID { get; set; }
        #region

        [ForeignKey("TipZalbeE")]
        public Guid TipZalbeID { get; set; }
        public TipZalbeE TipZalbe {get; set;}
        [Required]
        public Guid PodnosilacZalbeID { get; set; } //Kupac
        [NotMapped]
        public PodnosilacZalbeDto PodnosilacZalbe { get; set; }
        [Required]
        public Guid LicitacijaID { get; set; }
        [NotMapped]
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
