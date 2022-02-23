using LoggerServis.Database.Entities;
using LoggerServis.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LoggerServis.Controllers
{
    /// <summary>
    /// Logger controller pomocu kojeg se vrse sve potrebne funkcionalnosti vezane za logger
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class LoggerController : ControllerBase
    {

        private readonly LoggerService loggerService;

        /// <summary>
        /// Logger controller konstruktor
        /// </summary>
        public LoggerController()
        {
            loggerService = new LoggerService();
        }

        /// <summary>
        /// Pristup svim loggerima
        /// </summary>
        /// <returns>Vraca listu svih logger-a</returns>
        /// <response code = "200">Pristup svim logger-ima</response>
        // GET: api/<LoggerController>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public IEnumerable<Logger> Get()
        {
            return loggerService.GetAll();
        }

        /// <summary>
        /// Pristup loggeru na osnovu zadatog id-a
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Vraca logger ciji je id zadat u putanji</returns>
        /// <response code = "200">Dobijanje loggera na osnovu zadatog id-a</response>
        /// <response code = "404">Ne postoji logger sa zadatim id-em</response>
        // GET api/<LoggerController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Logger logger = loggerService.GetById(id);
            if (logger != null)
            {
                return StatusCode(StatusCodes.Status200OK, logger);
            }
            return StatusCode(StatusCodes.Status404NotFound, new { message = "Logger with this id: " + id + " doesnt exist" });
        }

        /// <summary>
        /// Kreiranje novog loggera
        /// </summary>
        /// <param name="model">Model loggera</param>
        /// <returns>Vraca novi logger</returns>
        /// <remarks>
        /// <strong>
        /// Primer request-a za kreiranje novog loggera \
        /// !!!!!! Ovaj json je potrebno kopirati u request body kako bi uspesno testirali!!!!! \
        /// POST api/Logger/ \
        /// </strong>
        ///{
        ///     "opisAktivnosti": "Pristup svim tipovima korisnika po id-u.*********Korisnicko ime: markoo", \
        ///     "datum": "2022-02-22T00:59:54.371502" \
        ///}
        /// </remarks>
        /// <response code = "201">Kreiran je novi logger</response>
        /// <response code = "500">Greska prilikom pokusaja kreiranja logger</response>
        // POST api/<LoggerController>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public IActionResult Post([FromBody] Logger model)
        {
            try
            {
                loggerService.Save(model);
                return StatusCode(StatusCodes.Status201Created, model);
            }
            catch (Exception exp)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, exp);
            }
        }

        /// <summary>
        /// Modifikacija postojeceg loggera
        /// </summary>
        /// <param name="id">Parametar na osnovu kojeg se identifikuje logger za azuriranje</param>
        /// <param name="logger">Model loggera</param>
        /// <returns>Vraca modifikovan logger</returns>
        /// <remarks>
        /// <strong>
        /// Primer request-a za modifikovanje loggera \
        /// !!!!!! Ovaj json je potrebno kopirati u request body kako bi uspesno testirali!!!!! \
        /// PUT api/Logger/156 \
        /// </strong>
        ///{
        ///     "loggerID": 156, \
        ///     "opisAktivnosti": "Pristup svim tipovima korisnika po id-u.*********Korisnicko ime: markoo", \
        ///     "datum": "2022-02-22T00:59:54.371502" \
        ///}
        /// </remarks>
        /// <response code = "200">Dobijanje modifikovanog loggera</response>
        /// <response code = "400">Nisu uneti dobri podaci</response>
        /// <response code = "404">Ne postoji logger sa zadatim id-em</response>
        // PUT api/<LoggerController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Logger logger)
        {
            if (id != logger.LoggerID)
            {
                return BadRequest();
            }

            try
            {
                loggerService.Update(logger);
            }
            catch (Exception exp)
            {
                if (!loggerService.LoggerExists(id))
                {
                    return StatusCode(StatusCodes.Status404NotFound, exp);
                }
                else
                {
                    throw;
                }
            }
            return StatusCode(StatusCodes.Status200OK, logger);
        }

        /// <summary>
        /// Brisanje postojeceg loggera
        /// </summary>
        /// <param name="id">Parametar na osnovu kojeg se identifikuje logger za brisanje</param>
        /// <returns>Brise zadati logger</returns>
        /// <response code = "200">Obrisan je logger</response>
        /// <response code = "404">Ne postoji logger za kojeg se izvrsava brisanje</response>
        // DELETE api/<LoggerController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Logger logger = loggerService.GetById(id);
            if (logger == null)
            {
                return NotFound();
            }
            loggerService.Delete(logger);

            return Ok(logger);
        }
    }
}
