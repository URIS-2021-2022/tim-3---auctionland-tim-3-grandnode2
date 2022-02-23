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
    /// Verzija dokumenta controller pomocu kojeg se vrse sve potrebne funkcionalnosti iz specifikacije vezane za verziju dokumenta
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
        /// Pristup svim verzijama dokumenta, koji omogucen od strane prethodno ulogovanog korisnika koji ima ulogu Administratora ili Prve komisije,
        /// uz logovanje navedene aktivnosti, kao i korisnickog imena korisnika koji je izvrsio tu aktivnost u okviru loggera
        /// </summary>
        /// <returns>Vraca listu svih verzija dokumenata</returns>
        /// <response code = "200">Pristup svim verzijama dokumenata</response>
        /// <response code = "401">Korisnik nije ulogovan</response>
        // GET: api/<VerzijaDokumentaController>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet]
        public IEnumerable<VerzijaDokumenta> Get()
        {
            #pragma warning disable CS4014
            logger.PostLogger("Pristup svim verzijama dokumenta." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
            #pragma warning restore CS4014
            return verzijaDokumentaService.GetAll();
        }

        /// <summary>
        /// Pristup verzijama dokumenata na osnovu zadatog id-a, od strane prethodno ulogovanog korisnika koji ima ulogu Administratora ili Prve komisije,
        /// uz logovanje navedene aktivnosti, kao i korisnickog imena korisnika koji je izvrsio tu aktivnost u okviru loggera
        /// </summary>
        /// <param name="id">Id verzije dokumenta primer: b35b4aa3-9ff8-49a1-a7a3-132be69397e3</param>
        /// <returns>Vraca verziju dokumenta ciji id je zadat u putanji</returns>
        /// <response code = "200">Dobijanje verzija dokumenata na osnovu zadatog id-a</response>
        /// <response code = "401">Korisnik nije ulogovan</response>
        /// <response code = "404">Ne postoji verzija dokumenta sa zadatim id-em</response>
        // GET api/<VerzijaDokumentaController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        /// Kreiranje nove verzije dokumenta, od strane prethodno ulogovanog korisnika koji ima ulogu Administratora ili Prve komisije, 
        /// uz logovanje navedene aktivnosti, kao i korisnickog imena korisnika koji je izvrsio tu aktivnost u okviru loggera
        /// </summary>
        /// <param name="model">Model verzije dokumenta</param>
        /// <returns>Vraca novu verziju dokumenta</returns>
        /// <remarks>
        /// <strong>
        /// Primer request-a za kreiranje nove verzije dokumenta \
        /// !!!!!! Ovaj json je potrebno kopirati u request body kako bi uspesno testirali!!!!! \
        /// POST api/VerzijaDokumenta/ \
        /// </strong>
        ///{ \
        ///     "verzija": "v1.1", \
        ///     "revizija": "Uvid u dokument", \
        ///     "datum": "2021-12-09T00:00:00" \
        ///}
        /// </remarks>
        /// <response code = "201">Kreirana je nova verzija dokumenta</response>
        /// <response code = "401">Korisnik nije ulogovan</response>
        /// <response code = "500">Greska prilikom pokusaja kreiranja nove verzije dokumenta</response>
        // POST api/<VerzijaDokumentaController>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public IActionResult Post([FromBody] VerzijaDokumenta model)
        {
            #pragma warning disable CS4014
            logger.PostLogger("Kreiranje nove verzije dokumenta." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
            #pragma warning restore CS4014
            try
            {
                model.VerzijaDokumentaID = Guid.NewGuid();
                verzijaDokumentaService.Save(model);
                return StatusCode(StatusCodes.Status201Created, model);
            }
            catch (Exception exp)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, exp);
            }
        }

        /// <summary>
        /// Modifikacija postojece verzije dokumenta, od strane prethodno ulogovanog korisnika koji ima ulogu Administratora ili Prve komisije, 
        /// uz logovanje navedene aktivnosti, kao i korisnickog imena korisnika koji je izvrsio tu aktivnost u okviru loggera
        /// </summary>
        /// <param name="id">Parametar na osnovu kojeg se identifikuje verzija dokumenta za azuriranje primer: b35b4aa3-9ff8-49a1-a7a3-132be69397e3</param>
        /// <param name="verzijaDokumenta">Model verzije dokumenta</param>
        /// <returns>Vraca modifikovanu verziju dokumenta</returns>
        /// <remarks>
        /// Primer request-a za modifikaciju v \
        /// !!!!!! Ovaj json je potrebno kopirati u request body kako bi uspesno testirali!!!!! \
        /// PUT api/VerzijaDokumenta/b35b4aa3-9ff8-49a1-a7a3-132be69397e3 \
        ///{ \
        ///     "verzijaDokumentaID": "b35b4aa3-9ff8-49a1-a7a3-132be69397e3", \
        ///     "verzija": "v1.1", \
        ///     "revizija": "Uvid u dokument", \
        ///     "datum": "2021-12-09T00:00:00" \
        ///}
        /// </remarks>
        /// <response code = "200">Dobijanje modifikovane verzije dokumenta</response>
        /// <response code = "400">Nisu uneti dobri podaci</response>
        /// <response code = "401">Korisnik nije ulogovan</response>
        /// <response code = "404">Ne postoji verzija dokumenta sa zadatim id-em</response>
        // PUT api/<VerzijaDokumentaController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        /// Brisanje postojece verzije dokumenta, , od strane prethodno ulogovanog korisnika koji ima ulogu Administratora ili Prve komisije, 
        /// uz logovanje navedene aktivnosti, kao i korisnickog imena korisnika koji je izvrsio tu aktivnost u okviru loggera
        /// </summary>
        /// <param name="id">Parametar na osnovu kojeg se identifikuje verzija dokumenta za brisanje primer: b35b4aa3-9ff8-49a1-a7a3-132be69397e3</param>
        /// <returns>Brise zadatu verziju dokumenta</returns>
        /// <response code = "200">Obrisana je verzija dokumenta</response>
        /// <response code = "401">Korisnik nije ulogovan</response>
        /// <response code = "404">Ne postoji verzija dokumenta za kojeg se izvrsava brisanje</response>
        // DELETE api/<VerzijaDokumentaController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
