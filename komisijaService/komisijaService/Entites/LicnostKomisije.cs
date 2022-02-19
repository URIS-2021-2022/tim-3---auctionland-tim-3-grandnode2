using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace komisijaService.Entites
{
    public class LicnostKomisije
    {
        /// <summary>
        /// Identifikator licnosti komisije
        /// </summary>
        [Key]
        [Required]
        public Guid licnostKomisijeId { get; set; }

        /// <summary>
        /// Ime licnosti komisije
        /// </summary>
        [StringLength(50)]
        [Required]
        public string imeLicnostiKomisije { get; set; }

        /// <summary>
        /// Prezime licnosti komisije
        /// </summary>
        [StringLength(50)]
        [Required]
        public string prezimeLicnostiKomisije { get; set; }

        /// <summary>
        /// Funkcija licnosti komisije
        /// </summary>
        [StringLength(50)]
        [Required]
        public string funkcijaLicnostiKomisije { get; set; }

        /// <summary>
        /// Broj telefona licnosti komisije
        /// </summary>
        [Phone]
        [Required]
        public string kontaktLicnostiKomisije { get; set; }

        /// <summary>
        /// Datum rodjenja licnosti komisije
        /// </summary>
        [Required]
        public DateTime datumRodjenjaLicnostiKomisije { get; set; }

        /// <summary>
        /// Identifikator komisije
        /// </summary>
        [ForeignKey("komisijaId")]
        public Guid komisijaId { get; set; }
        public String oznakaKomisije { get; set; }
    }
}
