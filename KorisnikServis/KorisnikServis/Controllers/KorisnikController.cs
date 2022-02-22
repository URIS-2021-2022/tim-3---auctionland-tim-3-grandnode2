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
    /// <summary>
    /// Korisnik Controller pomocu kojeg se vrse sve potrebne funkcionalnosti vezane za controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class KorisnikController : ControllerBase
    {

        private readonly KorisnikService korisnikService;
        private readonly IGenerateToken generateToken;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly LoggerService logger;

        /// <summary>
        /// Korisnik Controller konstruktor
        /// </summary>
        public KorisnikController(IGenerateToken generateToken, IHttpContextAccessor httpContextAccessor)
        {
            korisnikService = new KorisnikService();
            this.generateToken = generateToken;
            this.httpContextAccessor = httpContextAccessor;
            logger = new LoggerService();
        }

        /// <summary>
        /// Pristup svim korisnicima
        /// </summary>
        /// <returns>Vraca listu svih korisnika</returns>
        /// <response code = "200">Pristup svim korisnicima</response>
        // GET: api/<KorisnikController>
        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public IEnumerable<Korisnik> Get()
        {
            #pragma warning disable CS4014
            logger.PostLogger("Pristup svim korisnicima." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
            #pragma warning restore CS4014
            return korisnikService.GetAll();
        }

        /// <summary>
        /// Pristup korisniku na osnovu korisnickog imena i lozinke 
        /// </summary>
        /// <param name="KorisnickoIme"></param>
        /// <param name="Lozinka"></param>
        /// <returns>Vraca korisnika sa zadatim korisnickim imenom i lozinkom</returns>
        /// <response code = "200">Pristup korisniku putem korisnickog imena i lozinke </response>
        /// <response code = "404">Ne postoji korisnik sa zadatim korisnickim imenom ili lozinkom</response>
        [Authorize(Roles = "Administrator, Superuser, Tehnicki sekretar, Prva komisija, Menadzer")]
        [HttpGet("{KorisnickoIme}/{Lozinka}")]
        public IActionResult GetKorisnikUP(string KorisnickoIme, string Lozinka)
        {
            #pragma warning disable CS4014 
            logger.PostLogger("Pristup korisniku putem korisnickog imena i lozinke." +
                "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
            #pragma warning restore CS4014
            Korisnik korisnik = korisnikService.FindImeLozinka(KorisnickoIme, Lozinka);

            if (korisnik.KorisnickoIme == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            return Ok(korisnik);
        }

        /// <summary>
        /// Logovanje korisnika putem korisnickog imena i lozinke
        /// </summary>
        /// <param name="korisnik"></param>
        /// <returns>Vraca token na osnovu logovanja korisnika</returns>
        /// <response code = "200">Dobijanje tokena na osnovu korisnickog imena i lozinke </response>
        /// <response code = "404">Ne postoji korisnik sa zadatim korisnickim imenom ili lozinkom</response>
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult LogIn([FromBody] Korisnik korisnik)
        {
            #pragma warning disable CS4014
            logger.PostLogger("Logovanje korisnika." + "*********KorisnickoIme: " + korisnik.KorisnickoIme);
            #pragma warning restore CS4014
            Korisnik provera = korisnikService.FindImeLozinka(korisnik.KorisnickoIme, korisnik.Lozinka);
            if (provera == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "KorisnickoIme or Lozinka is incorrect");
            }
            string tokenString = generateToken.TokenInitialization(korisnik.KorisnickoIme, korisnik.Lozinka);
            return Ok(new { token = tokenString });
        }

        /// <summary>
        /// Pristup korisniku na osnovu zadatog id-a
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Vraca korisnika ciji id je zadat u putanji</returns>
        /// <response code = "200">Dobijanje korisnika na osnovu zadatog id-a</response>
        /// <response code = "404">Ne postoji korisnik sa zadatim id-em</response>
        // GET api/<KorisnikController>/5
        [Authorize(Roles = "Administrator")]
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            #pragma warning disable CS4014
            logger.PostLogger("Pristup korisniku putem id-a." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
            #pragma warning restore CS4014
            Korisnik korisnik = korisnikService.GetById(id);
            if (korisnik != null)
            {
                return StatusCode(StatusCodes.Status200OK, korisnik);
            }
            return StatusCode(StatusCodes.Status404NotFound, new { message = "Korisnik with this id: " + id + " doesnt exist" });
        }

        /// <summary>
        /// Pristup korisniku na osnovu naziva uloge koju ima
        /// </summary>
        /// <param name="nazivTipa"></param>
        /// <returns>Vraca korisnika sa zadatom ulogom</returns>
        /// <response code = "200">Dobijanje korisnika na osnovu zadate uloge koju ima</response>
        /// <response code = "404">Ne postoji korisnik sa zadatom ulogom</response>
        [Authorize(Roles = "Administrator")]
        [HttpGet("getTip/{nazivTipa}")]
        public IActionResult GetByNazivTipa(string nazivTipa)
        {
            #pragma warning disable CS4014 
            logger.PostLogger("Pristup korisnicima po ulozi koju imaju." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
            #pragma warning restore CS4014
            List<Korisnik> korisnici = korisnikService.GetByTip(nazivTipa);
            if (korisnici != null)
            {
                return StatusCode(StatusCodes.Status200OK, korisnici);
            }

            return StatusCode(StatusCodes.Status404NotFound, new { message = "Korisnik with this type doesnt exist: " + nazivTipa });
        }

        /// <summary>
        /// Dobijanje informacija o korisniku na osnovu tokena
        /// </summary>
        /// <returns>Vraca korisnika na osnovu zadatog tokena</returns>
        /// <response code = "200">Dobijanje korisnika na osnovu zadatog tokena</response>
        /// <response code = "404">Ne postoji korisnik sa zadatim tokenom</response>
        [HttpGet("getKorisnikToken")]
        public IActionResult GetKorisnikToken()
        {
            #pragma warning disable CS4014
            logger.PostLogger("Pristup korisniku na osnovu tokena." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
            #pragma warning restore CS4014
            var identityClaims = (ClaimsIdentity)httpContextAccessor.HttpContext.User.Identity;
            Korisnik korisnik = korisnikService.GetKorisnikByToken(identityClaims);
            if (korisnik != null)
            {
                return StatusCode(StatusCodes.Status200OK, korisnik);
            }

            return StatusCode(StatusCodes.Status404NotFound, new { message = "Token is not valid"});
        }

        /// <summary>
        /// Kreiranje novog korisnika
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Vraca novog korisnika</returns>
        /// <response code = "201">Kreiran je novi korisnik</response>
        /// <response code = "500">Greska prilikom pokusaja kreiranja korisnika</response>
        // POST api/<KorisnikController>
        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] Korisnik model)
        {
            #pragma warning disable CS4014
            logger.PostLogger("Kreiranje novog korisnika." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
            #pragma warning restore CS4014
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

        /// <summary>
        /// Modifikacija postojeceg korisnika
        /// </summary>
        /// <param name="id"></param>
        /// <param name="korisnik"></param>
        /// <returns>Vraca modifikovanog korisnika</returns>
        /// <response code = "200">Dobijanje modifikovanog korisnika</response>
        /// <response code = "404">Ne postoji korisnik sa zadatim id-em</response>
        // PUT api/<KorisnikController>/5
        [Authorize(Roles = "Administrator, Superuser, Tehnicki sekretar, Prva komisija, Menadzer, Operater Nadmetanja")]
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] Korisnik korisnik)
        {
            #pragma warning disable CS4014
            logger.PostLogger("Modifikacija postojeceg korisnika." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
            #pragma warning restore CS4014
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

        /// <summary>
        /// Brisanje postojeceg korisnika
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Brise zadatog korisnika</returns>
        /// <response code = "200">Obrisan je korisnik</response>
        /// <response code = "404">Ne postoji korisnik za kojeg se izvrsava brisanje</response>
        // DELETE api/<KorisnikController>/5
        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            #pragma warning disable CS4014
            logger.PostLogger("Brisanje postojeceg korisnika." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
            #pragma warning restore CS4014
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
