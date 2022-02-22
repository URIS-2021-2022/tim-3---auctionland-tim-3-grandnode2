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

        /// <summary>
        /// ID licitacije
        /// </summary>
        public Guid LicitacijaID { get; set; }
        /// <summary>
        /// Datum podnosenja zalbe
        /// </summary>
        public DateTime DatPodnosenjaZalbe { get; set; }

        /// <summary>
        /// ID podnosioca zalbe
        /// </summary>
        [Required(ErrorMessage ="Obavezno je uneti id podnosioca zalbe")]
        public Guid PodnosilacZalbeID { get; set; } //Kupac
        /// <summary>
        /// ID tipa zalbe
        /// </summary>
        public Guid TipZalbeID { get; set; }
        /// <summary>
        /// Datum resenja zalbe
        /// </summary>
        public DateTime DatResenja { get; set; }

        /// <summary>
        /// Broj resenja zalbe
        /// </summary>
        public int BrojResenja { get; set; }

        /// <summary>
        /// Status zalbe 
        /// </summary>
        public string StatusZalbe { get; set; }

        /// <summary>
        /// Radnja zalbe 
        /// </summary>
        public string RadnjaZalbe { get; set; }



        #endregion
        /// <summary>
        /// Validacija kreiranja zalbe
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
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
