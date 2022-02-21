using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KatastarskaOpstina.Models
{
    public class StatutOpstineModelDto
    {
        public Guid StatutOpstineID { get; set; }

        #region
        public string SadrzajStatuta { get; set; }

        public DateTime DatumKreiranjaStatuta { get; set; }
        #endregion
    }
}
