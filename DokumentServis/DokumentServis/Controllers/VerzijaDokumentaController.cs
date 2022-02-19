using DokumentServis.Database.Entities;
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

        public VerzijaDokumentaController()
        {
            verzijaDokumentaService = new VerzijaDokumentaService();
        }

        // GET: api/<VerzijaDokumentaController>
        [HttpGet]
        public IEnumerable<VerzijaDokumenta> Get()
        {
            return verzijaDokumentaService.GetAll();
        }

        // GET api/<VerzijaDokumentaController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
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
        public IActionResult Put(int id, [FromBody] VerzijaDokumenta verzijaDokumenta)
        {
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
        public IActionResult Delete(int id)
        {
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
