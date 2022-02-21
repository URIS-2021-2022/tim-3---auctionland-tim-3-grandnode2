using KatastarskaOpstina.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KatastarskaOpstina.Models
{
    public class KatastarskaOpstinaCreationDto 
    {

        public Guid StatutOpstineID { get; set; }

        [Required(ErrorMessage = "Obavezno je uneti naziv opstine")]
        public string NazivOpstine { get; set; }

    }
}
