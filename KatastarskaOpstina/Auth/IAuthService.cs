using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KatastarskaOpstina.Auth
{
    public interface IAuthService
    {
        public bool Authorize(string key);
    }
}
