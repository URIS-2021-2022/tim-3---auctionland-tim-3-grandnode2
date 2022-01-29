using KorisnikServis.Database.Entities;
using KorisnikServis.Services;
using KorisnikServis.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KorisnikServis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KorisnikController : ControllerBase
    {

        private KorisnikService korisnikService;
        private readonly IGenerateToken generateToken;

        public KorisnikController(IGenerateToken generateToken)
        {
            korisnikService = new KorisnikService();
            this.generateToken = generateToken;
        }

        // GET: api/<KorisnikController>
        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public IEnumerable<Korisnik> Get()
        {
            return korisnikService.GetAll();
        }

        [Authorize(Roles = "Administrator, Superuser, Tehnicki sekretar, Prva komisija, Menadzer")]
        [HttpGet("{KorisnickoIme}/{Lozinka}")]
        public IActionResult GetKorisnikUP(string KorisnickoIme, string Lozinka)
        {
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
        public IActionResult Get(int id)
        {
            Korisnik korisnik = korisnikService.GetById(id);
            if (korisnik != null)
            {
                return StatusCode(StatusCodes.Status200OK, korisnik);
            }
            return StatusCode(StatusCodes.Status404NotFound, new { message = "Korisnik with this id: " + id + " doesnt exist" });
        }

        // POST api/<KorisnikController>
        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] Korisnik model)
        {
            try
            {
                korisnikService.Save(model);
                return StatusCode(StatusCodes.Status201Created, model);
            }
            catch (Exception exp)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, exp);
            };
        }

        // PUT api/<KorisnikController>/5
        [Authorize(Roles = "Administrator, Superuser, Tehnicki sekretar, Prva komisija, Menadzer, Operater Nadmetanja")]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Korisnik korisnik)
        {
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
        public IActionResult Delete(int id)
        {
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
