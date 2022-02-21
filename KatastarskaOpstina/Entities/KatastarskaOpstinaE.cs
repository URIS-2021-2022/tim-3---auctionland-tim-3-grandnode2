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
        [Key]
        public Guid KatastarskaOpstinaID { get; set; }

        [ForeignKey("StatutOpstineE")]
        public Guid StatutOpstineID { get; set; }
        public StatutOpstineE StatutOpstine { get; set; }
        public string NazivOpstine { get; set; }
        [NotMapped]
        public List<ParcelaDto> Parcele { get; set; }

    }
}
