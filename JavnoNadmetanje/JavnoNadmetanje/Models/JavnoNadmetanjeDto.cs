using JavnoNadmetanje.Enums;
using JavnoNadmetanje.Models.KupacService;
using JavnoNadmetanje.Models.ParcelaService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Models
{
    /// <summary>
    /// DTO za Javno nadmetanje
    /// </summary>
    public class JavnoNadmetanjeDto
    {
        /// <summary>
        /// ID javnog nadmetanja
        /// </summary>
        public Guid JavnoNadmetanjeId { get; set; }

        /// <summary>
        ///  Datum održavanja javnog nadmetanja
        /// </summary>
        public DateTime Datum { get; set; }

        /// <summary>
        ///  Vreme početka javnog nadmetanja
        /// </summary>
        public DateTime VremePocetka { get; set; }

        /// <summary>
        ///  Vreme kraja javnog nadmetanja
        /// </summary>
        public DateTime VremeKraja { get; set; }

        /// <summary>
        /// Početna cena zemljišta po hektaru
        /// </summary>
        public int PocetnaCenaPoHektaru { get; set; }

        /// <summary>
        ///  Izuzeto javno nadmetanje
        /// </summary>
        public Boolean Izuzeto { get; set; }

        /// <summary>
        /// Tip javnog nadmetanja
        /// </summary>
        public TipJavnogNadmetanja Tip { get; set; }

        /// <summary>
        /// Izlicitirana cena
        /// </summary>
        public int IzlicitiranaCena { get; set; }

        /// <summary>
        /// Vremenski period zakupa 
        /// </summary>
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
        public int Krug { get; set; }

        /// <summary>
        /// Status javnog nadmetanja 
        /// </summary>
        public StatusJavnogNadmetanja Status { get; set; }
       
        /// <summary>
        /// ID oglasa
        /// </summary>
        public Guid OglasId { get; set; }

        /// <summary>
        /// ID licitacije
        /// </summary>
        public Guid LicitacijaId { get; set; }

        /// <summary>
        /// Delovi parcele
        /// </summary>
        public List<DeoParceleDto> DeloviParcele { get; set; }

        /// <summary>
        /// Kupac
        /// </summary>
        public KupacDto Kupac { get; set; }
    }
}
