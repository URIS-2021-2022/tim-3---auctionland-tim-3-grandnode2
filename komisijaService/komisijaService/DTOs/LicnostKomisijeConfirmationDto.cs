using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace komisijaService.DTOs
{
    public class LicnostKomisijeConfirmationDto
    {
   

        /// <summary>
        /// Prezime i ime licnosti komisije
        /// </summary>
        [StringLength(100)]
        [Required(ErrorMessage = "Prezime i ime licnosti komisije je obavezno")]
        public string licnostKomisije { get; set; }

        /// <summary>
        /// Funkcija licnosti komisije
        /// </summary>
        [StringLength(50)]
        [Required(ErrorMessage = "Funkicja licnosti komisije je obavezna!")]
        public string funkcijaLicnostiKomisije { get; set; }

        /// <summary>
        /// Broj telefona licnosti komisije
        /// </summary>
        [Phone]
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


    }
}
