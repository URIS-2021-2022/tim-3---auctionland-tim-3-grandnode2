using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UgovorService.Logger
{
    public class LoggerVO
    {
        /// <summary>
        /// Opis aktivnosti
        /// </summary>
        public string OpisAktivnosti { get; set; }
        /// <summary>
        /// Datum aktivnosti
        /// </summary>
        public DateTime Datum { get; set; }
    }
}
