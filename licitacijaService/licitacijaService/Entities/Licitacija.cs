using licitacijaService.DTOs.Mock;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace licitacijaService.Entities
{
    public class Licitacija
    {

        /// <summary>
        /// Identifikator licitacije
        /// </summary>
        [Key]
        [Required]
        public Guid licitacijaId { get; set; }

        /// <summary>
        /// Broj licitacije
        /// </summary>
        [StringLength(50)]
        [Required]
        public int brojLicitacije { get; set; }

        /// <summary>
        /// Godina licitacije
        /// </summary>
        [Required]
        public int goidna { get; set; }

        /// <summary>
        /// Datum licitacije
        /// </summary>
        [Required]
        public DateTime datumLicitacije { get; set; }

        /// <summary>
        /// Ogranicenje licitacije
        /// </summary>
        [Required]
        public int ogranicenjeLicitacije { get; set; }

        /// <summary>
        /// Korak cene
        /// </summary>
        [Required]
        public int korakCene { get; set; }

        /// <summary>
        /// Rok za dostavljanje dokumenata za prijavu
        /// </summary>
        [Required]
        public DateTime rokZaDostavuPrijava { get; set; }

        /// <summary>
        /// Jedinstveni identifikator komisije
        /// </summary>
        [Required]
        public string oznakaKomisije { get; set; }

        [NotMapped]
        public KomisijaConfirmationDTO komisija { get; set; }

        /// <summary>
        /// Lista dokumentacije za pravna lica
        /// </summary>
        [NotMapped]
        public ICollection<LicitacijaDokument> dokumnetacijaPravnaLica { get; set; }

        /// <summary>
        /// Lista dokumentacije za fizickih lica
        /// </summary>

        [NotMapped]
        public ICollection<LicitacijaDokument> dokumentacijaFizickaLica { get; set; }

        /// <summary>
        /// Lista javnih nadmetanja licitacije
        /// </summary>

        [NotMapped]
        public ICollection<JavnoNadmetanjeConfirmationDTO> javnaNadmetanja { get; set; }


    }
}
