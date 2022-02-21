using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KupacService.Auth
{
    public class AuthHelper : IAuthHelper
    {
        private readonly IConfiguration configuration;

        public AuthHelper(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public bool AuthorizeUser(string key)
        {
            if (!key.StartsWith("Bearer"))
            {
                return false;
            }

            var secretKey = key.Substring(key.IndexOf("Bearer") + 7);
            var storedKey = configuration.GetValue<string>("Authorization:Key");

            if (storedKey != secretKey)
            {
                return false;
            }
            return true;

        }
    }
}
