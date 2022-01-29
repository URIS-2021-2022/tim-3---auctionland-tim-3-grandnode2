using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KorisnikServis.Token
{
    public interface IGenerateToken
    {
        public string TokenInitialization(string KorisnickoIme, string Lozinka);
    }
}
