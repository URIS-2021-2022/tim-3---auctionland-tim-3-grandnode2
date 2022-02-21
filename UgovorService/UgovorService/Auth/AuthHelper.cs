using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UgovorService.Auth
{
    public class AuthHelper : IAuthHelper
    {
        private readonly IConfiguration configuration;

        public AuthHelper(IConfiguration configuration)
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
