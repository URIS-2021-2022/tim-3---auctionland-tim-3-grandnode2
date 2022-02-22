using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Uplata.Models
{
    public class UplataCreateDto
    {
        /// <summary>
        /// Broj računa sa kog je izvršena uplata
        /// </summary>
        public int BrojRacuna { get; set; }

        /// <summary>
        /// Poziv na broj u okviru uplate
        /// </summary>
        public int PozivNaBroj { get; set; }

        /// <summary>
        /// Iznos uplate
        /// </summary>
        public int Iznos { get; set; }

        /// <summary>
        /// Svrha uplate
        /// </summary>
        public String SvrhaUplate { get; set; }

        /// <summary>
        /// Datum uplate
        /// </summary>
        public DateTime Datum { get; set; }

        /// <summary>
        /// Banka ID
        /// </summary>
        public Guid BankaId { get; set; }

        /// <summary>
        /// Prijava za nadmetanje ID
        /// </summary>
        public Guid PrijavaZaNadmetanjeId { get; set; }

        /// <summary>
        /// ID kupca
        /// </summary>
        public Guid KupacId { get; set; }
    }
}
