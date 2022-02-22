using KorisnikServis.Database.Entities;
using KorisnikServis.Logger;
using KorisnikServis.Services;
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
    [Authorize(Roles = "Administrator")]
    public class TipKorisnikaController : ControllerBase
    {

        private readonly TipKorisnikaService tipKorisnikaService;
        private readonly LoggerService logger;

        public TipKorisnikaController()
        {
            tipKorisnikaService = new TipKorisnikaService();
            logger = new LoggerService();
        }

        // GET: api/<TipKorisnikaController>
        [HttpGet]
        public IEnumerable<TipKorisnika> Get()
        {
            logger.PostLogger("Pristup svim tipovima korisnika." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
            return tipKorisnikaService.GetAll();
        }

        // GET api/<TipKorisnikaController>/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            logger.PostLogger("Pristup svim tipovima korisnika po id-u." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
            TipKorisnika tipKorisnika = tipKorisnikaService.GetById(id);
            if (tipKorisnika != null)
            {
                return StatusCode(StatusCodes.Status200OK, tipKorisnika);
            }
            return StatusCode(StatusCodes.Status404NotFound, new { message = "TipKorisnika with this id: " + id + " doesnt exist" });
        }

        // POST api/<TipKorisnikaController>
        [HttpPost]
        public IActionResult Post([FromBody] TipKorisnika model)
        {
            logger.PostLogger("Kreiranje novog tipa korisnika." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
            try
            {
                tipKorisnikaService.Save(model);
                return StatusCode(StatusCodes.Status201Created, model);
            }
            catch (Exception exp)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, exp);
            }
        }

        // PUT api/<TipKorisnikaController>/5
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] TipKorisnika tipKorisnika)
        {
            logger.PostLogger("Modifikacija postojeceg tipa korisnika." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
            if (id != tipKorisnika.TipKorisnikaID)
            {
                return BadRequest();
            }

            try
            {
                tipKorisnikaService.Update(tipKorisnika);
            }
            catch (Exception exp)
            {
                if (!tipKorisnikaService.TipKorisnikaExists(id))
                {
                    return StatusCode(StatusCodes.Status404NotFound, exp);
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(StatusCodes.Status200OK, tipKorisnika);
        }

        // DELETE api/<TipKorisnikaController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            logger.PostLogger("Brisanje postojeceg tipa korisnika." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
            TipKorisnika tipKorisnika = tipKorisnikaService.GetById(id);
            if (tipKorisnika == null)
            {
                return NotFound();
            }

            tipKorisnikaService.Delete(tipKorisnika);

            return Ok(tipKorisnika);
        }
    }
}
