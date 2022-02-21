using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Zalba.Models
{
    public class ZalbaCreationDto : IValidatableObject
    {
        #region

        public Guid LicitacijaID { get; set; }
        public DateTime DatPodnosenjaZalbe { get; set; }

        [Required(ErrorMessage ="Obavezno je uneti id podnosioca zalbe")]
        public Guid PodnosilacZalbeID { get; set; } //Kupac
        public Guid TipZalbeID { get; set; }
        public DateTime DatResenja { get; set; }

        public int BrojResenja { get; set; }

        public string StatusZalbe { get; set; }

        public string RadnjaZalbe { get; set; }



        #endregion
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DatResenja < DatPodnosenjaZalbe)
            {
                yield return new ValidationResult(
                    "Zalba ne moze imati resenje ako prethodno nije kreirana",
                    new[] { "KreiranjeZalbeDto" });
            }
        }
    }
}
