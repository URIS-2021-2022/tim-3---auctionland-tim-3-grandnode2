using KatastarskaOpstina.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KatastarskaOpstina.Models
{
    public class KatastarskaOpstinaModelDto
    {
        public Guid KatastarskaOpstinaID { get; set; }
        #region
        public ParcelaDto Parcela { get; set; }
        public Guid StatutOpstineID { get; set; }

        public string NazivOpstine { get; set; }

        #endregion
    }
}
