using JavnoNadmetanje.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Models
{
    public class JavnoNadmetanjeCreateDto
    {
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
        /// ID parcele
        /// </summary>
        public Guid ParcelaId { get; set; }

        /// <summary>
        /// ID kupca
        /// </summary>
        public Guid KupacId { get; set; }
    }
}
