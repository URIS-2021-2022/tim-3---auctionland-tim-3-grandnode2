using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Zalba.Models
{
    public class TipZalbeCreationDto
    {
        #region
        /// <summary>
        /// Naziv tipa zalbe
        /// </summary>
        public string NazivTipa { get; set; }

        /// <summary>
        /// Opis tipa zalbe
        /// </summary>
        public string OpisTipa { get; set; }

        #endregion

        /*public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (NazivTipa != "Žalba na tok javnog nadmetanja" || 
                NazivTipa != "Zalba na tok javnog nadmetanja" ||
                NazivTipa != "Žalba na Odluku o davanju u zakup" ||
                NazivTipa != "Zalba na Odluku o davanju u zakup" ||
                NazivTipa != "Žalba na Odluku o davanju na korišćenje" ||
                NazivTipa != "Zalba na Odluku o davanju na koriscenje")
            {
                yield return new ValidationResult(
                    "Tip zahteva nije validan",
                    new[] { "TipZalbeCreationDto" });
            }
        }*/
    }
}
