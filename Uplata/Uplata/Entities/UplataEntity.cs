using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Uplata.Entities
{
    public class UplataEntity
    {
        /// <summary>
        /// ID uplate
        /// </summary>
        [Key]
        public Guid UplataId { get; set; }

        /// <summary>
        /// Broj računa sa kog je izvršena uplata
        /// </summary>
        [Required]
        public int BrojRacuna { get; set; }

        /// <summary>
        /// Poziv na broj u okviru uplate
        /// </summary>
        [Required]
        public int PozivNaBroj { get; set; }

        /// <summary>
        /// Iznos uplate
        /// </summary>
        [Required]
        public int Iznos { get; set; }

        /// <summary>
        /// Svrha uplate
        /// </summary>
        [MaxLength(50)]
        public String SvrhaUplate { get; set; }

        /// <summary>
        /// Datum uplate
        /// </summary>
        [Required]
        public DateTime Datum { get; set; }

        /// <summary>
        /// Strani ključ banke
        /// </summary>
        [ForeignKey("BankaEntity")]
        public  Guid BankaId { get; set; }

        /// <summary>
        /// Entitet banka
        /// </summary>
        public BankaEntity Banka { get; set; }

        /// <summary>
        /// ID prijave za nadmetanje
        /// </summary>
        [Required]
        public Guid PrijavaZaNadmetanjeId { get; set; }

        /// <summary>
        /// ID kupca
        /// </summary>
        [Required]
        public Guid KupacId { get; set; }
    }
}
