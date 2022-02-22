using licitacijaService.DTOs.Mock;
using licitacijaService.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace licitacijaService.DTOs
{
    public class LicitacijaConfirmationDto
    {
        /// <summary>
        /// Broj licitacije
        /// </summary>
        [Required(ErrorMessage = "Obavezan broj licitacije")]
        public int brojLicitacije { get; set; }

        /// <summary>
        /// Godina licitacije
        /// </summary>
        [Required(ErrorMessage = "Obavezana godina licitacije")]
        public int goidna { get; set; }

        /// <summary>
        /// Datum licitacije
        /// </summary>
        [Required(ErrorMessage = "Obavezan datum licitacije")]
        public DateTime datumLicitacije { get; set; }

        /// <summary>
        /// Ogranicenje licitacije
        /// </summary>
        [Required(ErrorMessage = "Obavezno ogranicenje licitacije")]
        public int ogranicenjeLicitacije { get; set; }

        /// <summary>
        /// Korak cene
        /// </summary>
        [Required(ErrorMessage = "Obavezan korak cene")]
        public int korakCene { get; set; }

        /// <summary>
        /// Rok za dostavljanje dokumenata za prijavu
        /// </summary>
        [Required(ErrorMessage = "Obavezan rok za prijavu")]
        public DateTime rokZaDostavuPrijava { get; set; }

        public ICollection<LicitacijaVrstaDokumentaDto> dokumnetacijaPravnaLica { get; set; }

        /// <summary>
        /// Lista dokumentacije za fizickih lica
        /// </summary>

        [Required]
        public ICollection<LicitacijaVrstaDokumentaDto> dokumentacijaFizickaLica { get; set; }

        /// <summary>
        /// Lista javnih nadmetanja licitacije
        /// </summary>

        [Required]
        public ICollection<JavnoNadmetanjeConfirmationDto> javnaNadmetanja { get; set; }

        /// <summary>
        /// Komisija licitacije
        /// </summary>

        [Required]
        public KomisijaConfirmationDto komisija { get; set; }

        
    }
}
