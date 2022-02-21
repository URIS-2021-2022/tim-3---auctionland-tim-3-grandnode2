using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoggerServis.Database.Entities
{
    public class Logger
    {
        public int LoggerID { get; set; }

        public string OpisAktivnosti { get; set; }

        public DateTime Datum { get; set; }
    }
}
