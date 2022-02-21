using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Enums
{
    public enum StatusJavnogNadmetanja
    {
        [Display(Name ="Prvi krug")] PrviKrug,
        [Display(Name = "Drugi krug sa starim uslovima")] DrugiKrugStariUslovi,
        [Display(Name = "Drugi krug sa novim uslovima")] DrugiKrugNoviUslovi  
    }
}
