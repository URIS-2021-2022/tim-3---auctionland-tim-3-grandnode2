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
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrator, Prva komisija")]
    [ApiController]
    public class VerzijaDokumentaController : ControllerBase
    {
        private readonly VerzijaDokumentaService verzijaDokumentaService;
        private readonly LoggerService logger;

        public VerzijaDokumentaController()
        {
            verzijaDokumentaService = new VerzijaDokumentaService();
            logger = new LoggerService();
        }

        // GET: api/<VerzijaDokumentaController>
        [HttpGet]
        public IEnumerable<VerzijaDokumenta> Get()
        {
            logger.PostLogger("Pristup svim verzijama dokumenta." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
            return verzijaDokumentaService.GetAll();
        }

        // GET api/<VerzijaDokumentaController>/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            logger.PostLogger("Pristup verziji dokumenta putem id-a." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
            VerzijaDokumenta verzijaDokumenta = verzijaDokumentaService.GetById(id);
            if (verzijaDokumenta != null)
            {
                return StatusCode(StatusCodes.Status200OK, verzijaDokumenta);
            }
            return StatusCode(StatusCodes.Status404NotFound, new { message = "VerzijaDokumenta with this id: " + id + "doesnt exist" });
        }

        // POST api/<VerzijaDokumentaController>
        [HttpPost]
        public IActionResult Post([FromBody] VerzijaDokumenta model)
        {
            logger.PostLogger("Kreiranje nove verzije dokumenta." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
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

        // PUT api/<VerzijaDokumentaController>/5
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] VerzijaDokumenta verzijaDokumenta)
        {
            logger.PostLogger("Modifikacija postojece verzije dokumenta." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
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

        // DELETE api/<VerzijaDokumentaController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            logger.PostLogger("Brisanje postojece verzije dokumenta." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
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
