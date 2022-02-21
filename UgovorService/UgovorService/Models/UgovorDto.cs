using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UgovorService.Data;

namespace UgovorService.Models
{
    public class UgovorDto
    {
        /// <summary>
        /// ID ugovora
        /// </summary>
        public Guid UgovorId { get; set; }

        /// <summary>
        /// ID licitacije
        /// </summary>
        public Guid LicitacijaId { get; set; }

        /// <summary>
        /// Model licitacije
        /// </summary>
        public LicitacijaDto Licitacija { get; set; }

        /// <summary>
        /// Tip garancije
        /// </summary>
        public TipGarancije TipGarancije { get; set; }

        /// <summary>
        /// ID kupca
        /// </summary>
        public Guid LiceId { get; set; }
        /// <summary>
        /// Model kupca
        /// </summary>
        public LiceDto Lice { get; set; }

        /// <summary>
        /// Rok dospeća
        /// </summary>
        public int RokDospeca { get; set; }

        /// <summary>
        /// Zavodni broj
        /// </summary>
        public string ZavodniBroj { get; set; }

        /// <summary>
        /// Datum zavođenja
        /// </summary>
        public DateTime DatumZavodjenja { get; set; }

        /// <summary>
        /// Rok za vraćanje zemljišta
        /// </summary>
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
