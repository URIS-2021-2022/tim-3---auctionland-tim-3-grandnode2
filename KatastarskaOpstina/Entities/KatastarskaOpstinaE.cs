using KatastarskaOpstina.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KatastarskaOpstina.Entities
{
    public class KatastarskaOpstinaE
    {
        /// <summary>
        /// ID katastarske opstine
        /// </summary>
        [Key]
        public Guid KatastarskaOpstinaID { get; set; }

        /// <summary>
        /// ID statuta opstine
        /// </summary>
        [ForeignKey("StatutOpstineE")]
        public Guid StatutOpstineID { get; set; }
       
        public StatutOpstineE StatutOpstine { get; set; }
        /// <summary>
        /// Naziv opstine katastra
        /// </summary>
        public string NazivOpstine { get; set; }
        /// <summary>
        /// Parcele 
        /// </summary>
        [NotMapped]
        public List<ParcelaDto> Parcele { get; set; }

    }
}
