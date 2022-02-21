using JavnoNadmetanje.Enums;
using JavnoNadmetanje.Models.KupacService;
using JavnoNadmetanje.Models.ParcelaService;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Entities
{
    /// <summary>
    /// Entity za Javno nadmetanje
    /// </summary>
    public class JavnoNadmetanjeEntity
    {
        /// <summary>
        /// ID javnog nadmetanja
        /// </summary>
        [Key]
        public Guid JavnoNadmetanjeId { get; set; }

        /// <summary>
        ///  Datum održavanja javnog nadmetanja
        /// </summary>
        [Required]
        public DateTime Datum { get; set; }

        /// <summary>
        ///  Vreme početka javnog nadmetanja
        /// </summary>
        [Required]
        public DateTime VremePocetka { get; set; }

        /// <summary>
        ///  Vreme kraja javnog nadmetanja
        /// </summary>
        [Required]
        public DateTime VremeKraja { get; set; }

        /// <summary>
        /// Početna cena zemljišta po hektaru
        /// </summary>
        [Required]
        public int PocetnaCenaPoHektaru { get; set; }

        /// <summary>
        ///  Izuzeto javno nadmetanje
        /// </summary>
        [Required]
        public Boolean Izuzeto { get; set; }

        /// <summary>
        /// Tip javnog nadmetanja
        /// </summary>
        [Required]
        public TipJavnogNadmetanja Tip { get; set; }

        /// <summary>
        /// Izlicitirana cena
        /// </summary>
        [Required]
        public int IzlicitiranaCena { get; set; }

        /// <summary>
        /// Vremenski period zakupa 
        /// </summary>
        [Required]
        public int PeriodZakupa { get; set; }

        /// <summary>
        /// Broj učesnika u javnom nadmetanju
        /// </summary>
        public int BrojUcesnika { get; set; }

        /// <summary>
        /// Visina dopune depozita
        /// </summary>
        public int VisinaDopuneDepozita { get; set; }

        /// <summary>
        /// Krug javnog nadmetanja 
        /// </summary>
        [Required]
        public int Krug { get; set; }

        /// <summary>
        /// Status javnog nadmetanja 
        /// </summary>
        [Required]
        public StatusJavnogNadmetanja Status { get; set; }

        /// <summary>
        /// Strani ključ oglas
        /// </summary>
        [ForeignKey("OglasEntity")]
        public Guid OglasId { get; set; }

        /// <summary>
        /// Oglas entitet
        /// </summary>
        public OglasEntity Oglas { get; set; }

        /// <summary>
        /// ID licitacije
        /// </summary>
        [Required]
        public Guid LicitacijaId { get; set; }

        /// <summary>
        /// ID parcele
        /// </summary>
        [Required]
        public Guid ParcelaId { get; set; }

        /// <summary>
        /// ID kupca
        /// </summary>
        [Required]
        public Guid KupacId { get; set; }

        [NotMapped]
        public List<DeoParceleDto> DeloviParcele { get; set; }

        [NotMapped]
        public KupacDto Kupac{ get; set; }
    }
}
