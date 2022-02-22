using DokumentServis.Database.Entities;
using DokumentServis.Logger;
using DokumentServis.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DokumentServis.Controllers
{
    /// <summary>
    /// Verzija dokumenta controller pomocu kojeg se vrse sve potrebne funkcionalnosti vezane za controller
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrator, Prva komisija")]
    [ApiController]
    [Produces("application/json")]
    public class VerzijaDokumentaController : ControllerBase
    {
        private readonly VerzijaDokumentaService verzijaDokumentaService;
        private readonly LoggerService logger;

        /// <summary>
        /// Verzija dokumenta konstruktor
        /// </summary>
        public VerzijaDokumentaController()
        {
            verzijaDokumentaService = new VerzijaDokumentaService();
            logger = new LoggerService();
        }

        /// <summary>
        /// Pristup svim verzijama dokumenta
        /// </summary>
        /// <returns>Vraca listu svih verzija dokumenata</returns>
        /// <response code = "200">Pristup svim verzijama dokumenata</response>
        // GET: api/<VerzijaDokumentaController>
        [HttpGet]
        public IEnumerable<VerzijaDokumenta> Get()
        {
            #pragma warning disable CS4014
            logger.PostLogger("Pristup svim verzijama dokumenta." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
            #pragma warning restore CS4014
            return verzijaDokumentaService.GetAll();
        }

        /// <summary>
        /// Pristup verzijama dokumenata na osnovu zadatog id-a
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Vraca verziju dokumenta ciji id je zadat u putanji</returns>
        /// <response code = "200">Dobijanje verzija dokumenata na osnovu zadatog id-a</response>
        /// <response code = "404">Ne postoji verzija dokumenta sa zadatim id-em</response>
        // GET api/<VerzijaDokumentaController>/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            #pragma warning disable CS4014
            logger.PostLogger("Pristup verziji dokumenta putem id-a." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
            #pragma warning restore CS4014
            VerzijaDokumenta verzijaDokumenta = verzijaDokumentaService.GetById(id);
            if (verzijaDokumenta != null)
            {
                return StatusCode(StatusCodes.Status200OK, verzijaDokumenta);
            }
            return StatusCode(StatusCodes.Status404NotFound, new { message = "VerzijaDokumenta with this id: " + id + "doesnt exist" });
        }

        /// <summary>
        /// Kreiranje nove verzije dokumenta
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Vraca novu verziju dokumenta</returns>
        /// <response code = "201">Kreirana je nova verzija dokumenta</response>
        /// <response code = "500">Greska prilikom pokusaja kreiranja nove verzije dokumenta</response>
        // POST api/<VerzijaDokumentaController>
        [HttpPost]
        public IActionResult Post([FromBody] VerzijaDokumenta model)
        {
            #pragma warning disable CS4014
            logger.PostLogger("Kreiranje nove verzije dokumenta." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
            #pragma warning restore CS4014
            try
            {
                verzijaDokumentaService.Save(model);
                return StatusCode(StatusCodes.Status201Created, model);
            }
            catch (Exception exp)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, exp);
            }
        }

        /// <summary>
        /// Modifikacija postojece verzije dokumenta
        /// </summary>
        /// <param name="id"></param>
        /// <param name="verzijaDokumenta"></param>
        /// <returns>Vraca modifikovanu verziju dokumenta</returns>
        /// <response code = "200">Dobijanje modifikovane verzije dokumenta</response>
        /// <response code = "404">Ne postoji verzija dokumenta sa zadatim id-em</response>
        // PUT api/<VerzijaDokumentaController>/5
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] VerzijaDokumenta verzijaDokumenta)
        {
            #pragma warning disable CS4014
            logger.PostLogger("Modifikacija postojece verzije dokumenta." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
            #pragma warning restore CS4014
            if (id != verzijaDokumenta.VerzijaDokumentaID)
            {
                return BadRequest();
            }

            try
            {
                verzijaDokumentaService.Update(verzijaDokumenta);
            }
            catch (Exception exp)
            {
                if (!verzijaDokumentaService.VerzijaDokumentaExists(id))
                {
                    return StatusCode(StatusCodes.Status404NotFound, exp);
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(StatusCodes.Status200OK, verzijaDokumenta);
        }

        /// <summary>
        /// Brisanje postojece verzije dokumenta
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Brise zadatu verziju dokumenta</returns>
        /// <response code = "200">Obrisana je verzija dokumenta</response>
        /// <response code = "404">Ne postoji verzija dokumenta za kojeg se izvrsava brisanje</response>
        // DELETE api/<VerzijaDokumentaController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            #pragma warning disable CS4014
            logger.PostLogger("Brisanje postojece verzije dokumenta." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
            #pragma warning restore CS4014
            VerzijaDokumenta verzijaDokumenta = verzijaDokumentaService.GetById(id);
            if (verzijaDokumenta == null)
            {
                return NotFound();
            }
            verzijaDokumentaService.Delete(verzijaDokumenta);

            return Ok(verzijaDokumenta);
        }
    }
}
