using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Enums
{
    public enum TipJavnogNadmetanja
    {
        [Display(Name = "Javna licitacija")] JavnaLicitacija,
        [Display(Name = "Otvaranje zatvorenih ponuda")] OtvaranjeZatvorenihPonuda 
    }
}
