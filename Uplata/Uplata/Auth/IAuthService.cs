using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Uplata.Auth
{
    public interface IAuthService
    {
        public bool Authorize(string key);
    }
}
