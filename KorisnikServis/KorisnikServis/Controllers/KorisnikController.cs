using KorisnikServis.Database.Entities;
using KorisnikServis.Logger;
using KorisnikServis.Services;
using KorisnikServis.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KorisnikServis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KorisnikController : ControllerBase
    {

        private readonly KorisnikService korisnikService;
        private readonly IGenerateToken generateToken;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly LoggerService logger;

        public KorisnikController(IGenerateToken generateToken, IHttpContextAccessor httpContextAccessor)
        {
            korisnikService = new KorisnikService();
            this.generateToken = generateToken;
            this.httpContextAccessor = httpContextAccessor;
            logger = new LoggerService();
        }

        // GET: api/<KorisnikController>
        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public IEnumerable<Korisnik> Get()
        {
            logger.PostLogger("Pristup svim korisnicima." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
            return korisnikService.GetAll();
        }

        [Authorize(Roles = "Administrator, Superuser, Tehnicki sekretar, Prva komisija, Menadzer")]
        [HttpGet("{KorisnickoIme}/{Lozinka}")]
        public IActionResult GetKorisnikUP(string KorisnickoIme, string Lozinka)
        {
            logger.PostLogger("Pristup korisniku putem korisnickog imena i lozinke." +
                "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
            Korisnik korisnik = korisnikService.FindImeLozinka(KorisnickoIme, Lozinka);

            if (korisnik.KorisnickoIme == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok(korisnik);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult LogIn([FromBody] Korisnik korisnik)
        {
            logger.PostLogger("Logovanje korisnika." + "*********KorisnickoIme: " + korisnik.KorisnickoIme);
            Korisnik provera = korisnikService.FindImeLozinka(korisnik.KorisnickoIme, korisnik.Lozinka);
            if (provera == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "KorisnickoIme or Lozinka is incorrect");
            }
            string tokenString = generateToken.TokenInitialization(korisnik.KorisnickoIme, korisnik.Lozinka);
            return Ok(new { token = tokenString });
        }

        // GET api/<KorisnikController>/5
        [Authorize(Roles = "Administrator")]
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            logger.PostLogger("Pristup korisniku putem id-a." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
            Korisnik korisnik = korisnikService.GetById(id);
            if (korisnik != null)
            {
                return StatusCode(StatusCodes.Status200OK, korisnik);
            }
            return StatusCode(StatusCodes.Status404NotFound, new { message = "Korisnik with this id: " + id + " doesnt exist" });
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet("getTip/{nazivTipa}")]
        public IActionResult GetByNazivTipa(string nazivTipa)
        {
            logger.PostLogger("Pristup korisnicima po ulozi koju imaju." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
            List<Korisnik> korisnici = korisnikService.GetByTip(nazivTipa);
            if (korisnici != null)
            {
                return StatusCode(StatusCodes.Status200OK, korisnici);
            }

            return StatusCode(StatusCodes.Status404NotFound, new { message = "Korisnik with this type doesnt exist: " + nazivTipa });
        }

        [HttpGet("getKorisnikToken")]
        public IActionResult GetKorisnikToken()
        {
            logger.PostLogger("Pristup korisniku na osnovu tokena." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
            var identityClaims = (ClaimsIdentity)httpContextAccessor.HttpContext.User.Identity;
            Korisnik korisnik = korisnikService.GetKorisnikByToken(identityClaims);
            if (korisnik != null)
            {
                return StatusCode(StatusCodes.Status200OK, korisnik);
            }

            return StatusCode(StatusCodes.Status404NotFound, new { message = "Token is not valid"});
        }


        // POST api/<KorisnikController>
        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] Korisnik model)
        {
            logger.PostLogger("Kreiranje novog korisnika." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
            try
            {
                korisnikService.Save(model);
                return StatusCode(StatusCodes.Status201Created, model);
            }
            catch (Exception exp)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, exp);
            }
        }

        // PUT api/<KorisnikController>/5
        [Authorize(Roles = "Administrator, Superuser, Tehnicki sekretar, Prva komisija, Menadzer, Operater Nadmetanja")]
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] Korisnik korisnik)
        {
            logger.PostLogger("Modifikacija postojeceg korisnika." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
            if (id != korisnik.KorisnikID)
            {
                return BadRequest();
            }
            try
            {
                korisnikService.Update(korisnik);
            }
            catch (Exception exp)
            {
                if (!korisnikService.KorisnikExists(id))
                {
                    return StatusCode(StatusCodes.Status404NotFound, exp);
                }
                else
                {
                    throw;
                }
            }
            return StatusCode(StatusCodes.Status200OK, korisnik);
        }

        // DELETE api/<KorisnikController>/5
        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            logger.PostLogger("Brisanje postojeceg korisnika." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
            Korisnik korisnik = korisnikService.GetById(id);
            if (korisnik == null)
            {
                return NotFound();
            }

            korisnikService.Delete(korisnik);
            return Ok(korisnik);
        }
    }
}
