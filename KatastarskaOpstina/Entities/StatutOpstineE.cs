using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KatastarskaOpstina.Entities
{
    public class StatutOpstineE
    {
        [Key]
        public Guid StatutOpstineID { get; set; }

        #region
        public string SadrzajStatuta { get; set; }

        public DateTime DatumKreiranjaStatuta { get; set; }
        #endregion
    }
}
