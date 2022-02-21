using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using UgovorService.Data;
using UgovorService.Models;

namespace UgovorService.Entities
{
    public class Ugovor
    {
        /// <summary>
        /// ID ugovora
        /// </summary>
        [Key]
        public Guid UgovorId { get; set; }

        /// <summary>
        /// ID licitacije
        /// </summary>
        [Required]
        public Guid LicitacijaId { get; set; }

        /// <summary>
        /// Model licitacije
        /// </summary>
        [NotMapped]
        public LicitacijaDto Licitacija { get; set; }

        /// <summary>
        /// TIp garancije
        /// </summary>
        public TipGarancije TipGarancije { get; set; }

        /// <summary>
        /// ID kupca
        /// </summary>
        [Required]
        public Guid LiceId { get; set; }
        /// <summary>
        /// Model kupca
        /// </summary>
        [NotMapped]
        public LiceDto Lice { get; set; }

        /// <summary>
        /// Rok dospeća
        /// </summary>
        public int RokDospeca { get; set; }

        /// <summary>
        /// Zavodni broj
        /// </summary>
        [Required]
        public string ZavodniBroj { get; set; }

        /// <summary>
        /// Datum zavođenja
        /// </summary>
        public DateTime DatumZavodjenja { get; set; }

        /// <summary>
        /// Rok za vraćanje zemljišta
        /// </summary>
        [Required]
        public DateTime RokZaVracanjeZemljista { get; set; }

        /// <summary>
        /// Mesto potpisivanja
        /// </summary>
        public string MestoPotpisivanja { get; set; }

        /// <summary>
        /// Datum potpisa
        /// </summary>
        public DateTime DatumPotpisa { get; set; }
    }
}
