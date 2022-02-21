using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zalba.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration configuration;

        public AuthService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public bool Authorize(string key)
        {
            if (key == null || !key.StartsWith("Bearer"))
            {
                return false;
            }

            var keyOnly = key.Substring(key.IndexOf("Bearer") + 7);

            var storedKey = configuration.GetValue<string>("Authorization:Key");

            if (storedKey != keyOnly)
            {
                return false;
            }
            return true;
        }
    }
}
