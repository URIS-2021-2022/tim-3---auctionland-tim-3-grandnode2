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
    /// <summary>
    /// Tip korisnika controller pomocu kojeg se vrse sve potrebne funkcionalnosti iz specifikacije vezane za tip korisnika
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrator")]
    [Produces("application/json")]
    public class TipKorisnikaController : ControllerBase
    {

        private readonly TipKorisnikaService tipKorisnikaService;
        private readonly LoggerService logger;

        /// <summary>
        /// Tip korisnika controller konstruktor
        /// </summary>
        public TipKorisnikaController()
        {
            tipKorisnikaService = new TipKorisnikaService();
            logger = new LoggerService();
        }

        /// <summary>
        /// Pristup svim tipovima korisnika, koji je omogucen od strane prethodno ulogovanog korisnika, 
        /// uz logovanje navedene aktivnosti, kao i korisnickog imena korisnika koji je izvrsio tu aktivnost u okviru loggera
        /// </summary>
        /// <returns>Vraca listu svih tipova korisnika</returns>
        /// <response code = "200">Pristup svim tipovima korisnika</response>
        // GET: api/<TipKorisnikaController>
        [HttpGet]
        public IEnumerable<TipKorisnika> Get()
        {
            #pragma warning disable CS4014
            logger.PostLogger("Pristup svim tipovima korisnika." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
            #pragma warning restore CS4014
            return tipKorisnikaService.GetAll();
        }

        /// <summary>
        /// Pristup svim tipovima korisnika na osnovu id-a, 
        /// uz logovanje navedene aktivnosti, kao i korisnickog imena korisnika koji je izvrsio tu aktivnost u okviru loggera
        /// </summary>
        /// <param name="id">Id tipa korisnika</param>
        /// <returns>Vraca tip korisnika sa zadatim id-em</returns>
        /// <response code = "200">Dobijanje tipa korisnika na osnovu zadatog id-a</response>
        /// <response code = "404">Ne postoji tip korisnika sa zadatim id-em</response>
        // GET api/<TipKorisnikaController>/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            #pragma warning disable CS4014
            logger.PostLogger("Pristup svim tipovima korisnika po id-u." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
            #pragma warning restore CS4014
            TipKorisnika tipKorisnika = tipKorisnikaService.GetById(id);
            if (tipKorisnika != null)
            {
                return StatusCode(StatusCodes.Status200OK, tipKorisnika);
            }
            return StatusCode(StatusCodes.Status404NotFound, new { message = "TipKorisnika with this id: " + id + " doesnt exist" });
        }

        /// <summary>
        /// Kreiranje novog tipa korisnika, 
        /// uz logovanje navedene aktivnosti, kao i korisnickog imena korisnika koji je izvrsio tu aktivnost u okviru loggera
        /// </summary>
        /// <param name="model">Model tipa korisnika</param>
        /// <returns>Vraca novi tip korisnika</returns>
        /// <remarks>
        /// Primer request-a za kreiranje novog tipa korisnika \
        /// POST api/TipKorisnika/ \
        ///{ \
        ///     "tipKorisnikaID": "76b67f3a-f669-4b8f-9f6a-20a66107d312", \
        ///}
        /// </remarks>
        /// <response code = "201">Kreiran je novi tip korisnika</response>
        /// <response code = "500">Greska prilikom pokusaja kreiranja novog tipa korisnika</response>
        // POST api/<TipKorisnikaController>
        [HttpPost]
        public IActionResult Post([FromBody] TipKorisnika model)
        {
            #pragma warning disable CS4014
            logger.PostLogger("Kreiranje novog tipa korisnika." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
            #pragma warning restore CS4014
            try
            {
                model.TipKorisnikaID = Guid.NewGuid();
                tipKorisnikaService.Save(model);
                return StatusCode(StatusCodes.Status201Created, model);
            }
            catch (Exception exp)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, exp);
            }
        }

        /// <summary>
        /// Modifikacija postojeceg tipa korisnika, 
        /// uz logovanje navedene aktivnosti, kao i korisnickog imena korisnika koji je izvrsio tu aktivnost u okviru loggera
        /// </summary>
        /// <param name="id">Parametar na osnovu kojeg se identifikuje tip korisnika za azuriranje</param>
        /// <param name="tipKorisnika">Model novog tipa korisnika</param>
        /// <returns>Vraca modifikovani tip korisnika</returns>
        /// /// <remarks>
        /// Primer request-a za modifikaciju korisnika \
        /// PUT api/TipKorisnika/76b67f3a-f669-4b8f-9f6a-20a66107d312 \
        ///{ \
        ///     "tipKorisnikaID": "76b67f3a-f669-4b8f-9f6a-20a66107d312", \
        ///     "nazivTipa": "Operater Nadmetanja" \
        ///}
        /// </remarks>
        /// <response code = "200">Dobijanje modifikovanog tipa korisnika</response>
        /// <response code = "404">Ne postoji tip korisnika sa zadatim id-em</response>
        // PUT api/<TipKorisnikaController>/5
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] TipKorisnika tipKorisnika)
        {
            #pragma warning disable CS4014
            logger.PostLogger("Modifikacija postojeceg tipa korisnika." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
            #pragma warning restore CS4014
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

        /// <summary>
        /// Brisanje postojeceg tipa korisnika, 
        /// uz logovanje navedene aktivnosti, kao i korisnickog imena korisnika koji je izvrsio tu aktivnost u okviru loggera
        /// </summary>
        /// <param name="id">Parametar id-a tipa korisnika za kojeg se vrsi brisanje</param>
        /// <returns>Brise zadati tip korisnika</returns>
        /// <response code = "200">Obrisan je tip korisnika</response>
        /// <response code = "404">Ne postoji tip korisnika za kojeg se izvrsava brisanje</response>
        // DELETE api/<TipKorisnikaController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            #pragma warning disable CS4014
            logger.PostLogger("Brisanje postojeceg tipa korisnika." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
            #pragma warning restore CS4014
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
