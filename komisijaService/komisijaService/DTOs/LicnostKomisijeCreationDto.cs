using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace komisijaService.DTOs
{
    public class LicnostKomisijeCreationDto
    {

        /// <summary>
        /// Ime licnosti komisije
        /// </summary>
        [StringLength(50)]
        [Required(ErrorMessage = "Ime licnosti komisije je obavezno")]
        public string imeLicnostiKomisije { get; set; }

        /// <summary>
        /// Prezime licnosti komisije
        /// </summary>
        [StringLength(50)]
        [Required(ErrorMessage = "Prezime licnosti komisije je obavezno")]
        public string prezimeLicnostiKomisije { get; set; }

        /// <summary>
        /// Funkcija licnosti komisije
        /// </summary>
        [StringLength(50)]
        [Required(ErrorMessage = "Funkicja licnosti komisije je obavezna!")]
        public string funkcijaLicnostiKomisije { get; set; }

        /// <summary>
        /// Broj telefona licnosti komisije
        /// </summary>
        [Phone(ErrorMessage = "Unesite kontakt telefon u odgovarajucem formatu")]
        [Required(ErrorMessage = "Kontakt je obavezan!")]
        public string kontaktLicnostiKomisije { get; set; }

        /// <summary>
        /// Datum rodjenja licnosti komisije
        /// </summary>
        [Required(ErrorMessage = "Datum rodjenja licnosti komisije je obavezan!")]
        public DateTime datumRodjenjaLicnostiKomisije { get; set; }

        /// <summary>
        /// Oznaka komisije
        /// </summary>
        [Required(ErrorMessage = "Jedinstvena oznaka komisije je obavezna!")]
        public String oznakaKomisije { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (datumRodjenjaLicnostiKomisije < DateTime.Today.AddYears(-18))
            {
                yield return new ValidationResult(
                    "Clan komisije mora biti punoletan!",
                    new[] { "LicnostKomisijeCreationDto" });
            }
        }
    }
}
