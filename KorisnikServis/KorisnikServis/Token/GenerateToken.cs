using KorisnikServis.Database;
using KorisnikServis.Database.Entities;
using KorisnikServis.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace KorisnikServis.Token
{
    public class GenerateToken : IGenerateToken
    {

        private readonly IConfiguration configuration;
        private readonly DatabaseContext db;

        public GenerateToken(IConfiguration configuration)
        {
            this.configuration = configuration;
            db = new DatabaseContext();
        }

        public string TokenInitialization(string KorisnickoIme, string Lozinka)
        {
            KorisnikService korisnikService = new KorisnikService();
            Korisnik korisnik = korisnikService.FindImeLozinka(KorisnickoIme, Lozinka);
            if (korisnik == null)
            {
                return null;
            }
            TipKorisnika tipKorisnika = db.TipKorisnika.Find(korisnik.TipKorisnikaID);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim("KorisnikID", korisnik.KorisnikID.ToString()),
                    new Claim("ImeKorisnika", korisnik.ImeKorisnika),
                    new Claim("PrezimeKorisnika", korisnik.PrezimeKorisnika),
                    new Claim("KorisnickoIme", korisnik.KorisnickoIme),
                    new Claim("Lozinka", korisnik.Lozinka),
                    new Claim("TipKorisnikaID", korisnik.TipKorisnikaID.ToString()),
                    new Claim(ClaimTypes.Role, tipKorisnika.NazivTipa),
                    new Claim(ClaimTypes.Name, korisnik.KorisnickoIme),
                }),
                Expires = DateTime.UtcNow.AddMinutes(90),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
