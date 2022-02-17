using JavnoNadmetanje.Data;
using JavnoNadmetanje.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Controllers
{
    [ApiController]
    [Route("api/oglasi")]
    public class OglasController : ControllerBase
    {
        private readonly IOglasRepository oglasRepository;
        private readonly LinkGenerator linkGenerator;

        public OglasController(IOglasRepository oglasRepository, LinkGenerator linkGenerator)
        {
            this.oglasRepository = oglasRepository;
            this.linkGenerator = linkGenerator;
        }

        [HttpGet]
        public ActionResult<List<OglasModel>> GetOglasi()
        {
            List<OglasModel> oglasi = oglasRepository.GetOglasi();

            if (oglasi == null || oglasi.Count == 0)
            {
                return NoContent();
            }
            return Ok(oglasi);
        }

        [HttpGet("{oglasId}")]
        public ActionResult<OglasModel> GetOglasById(Guid oglasId)
        {
            OglasModel oglas = oglasRepository.GetOglasById(oglasId);

            if (oglas == null)
            {
                return NotFound();
            }
            return Ok(oglas);
        }

        [HttpPost]
        public ActionResult<OglasModel> CreateOglas([FromBody] OglasModel oglas)
        {
            try
            {
                OglasModel oglas1 = oglasRepository.CreateOglas(oglas);
                string location = linkGenerator.GetPathByAction("GetOglasi", "Oglas", new { oglasId = oglas1.OglasId });
                return Created(location, oglas1);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska prilikom kreiranja oglasa!");
            }
        }

        [HttpDelete("{oglasId}")]
        public IActionResult DeleteOglas(Guid oglasId)
        {
            try
            {
                OglasModel oglas = oglasRepository.GetOglasById(oglasId); ;
                if (oglas == null)
                {
                    return NotFound();
                }

                oglasRepository.DeleteOglas(oglasId);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska prilikom brisanja oglasa!");
            }
        }

        [HttpPut]
        public ActionResult<OglasModel> UpdateOglas(OglasModel oglas)
        {
            try
            {
                if (oglasRepository.GetOglasById(oglas.OglasId) == null)
                {
                    return NotFound();
                }

                return Ok(oglasRepository.UpdateOglas(oglas));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska prilikom azuriranja oglasa!");
            }
        }
    }
}
