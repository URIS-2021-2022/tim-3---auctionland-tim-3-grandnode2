using AutoMapper;
using JavnoNadmetanje.Data;
using JavnoNadmetanje.Entities;
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
        private readonly IMapper mapper;

        public OglasController(IOglasRepository oglasRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.oglasRepository = oglasRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [HttpGet]
        [HttpHead]
        public ActionResult<List<OglasDto>> GetOglasi()
        {
            List<OglasEntity> oglasi = oglasRepository.GetOglasi();

            if (oglasi == null || oglasi.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<OglasDto >>(oglasi));
        }

        [HttpGet("{oglasId}")]
        public ActionResult<OglasDto> GetOglasById(Guid oglasId)
        {
            OglasEntity oglas = oglasRepository.GetOglasById(oglasId);

            if (oglas == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<OglasDto>(oglas));
        }

        [HttpPost]
        public ActionResult<OglasDto> CreateOglas([FromBody] OglasDto oglas)
        {
            try
            {
                OglasEntity oglas1 = mapper.Map<OglasEntity>(oglas);
                OglasEntity oglas2 = oglasRepository.CreateOglas(oglas1);
                string location = linkGenerator.GetPathByAction("GetOglasi", "Oglas", new { oglasId = oglas1.OglasId });
                return Created(location, mapper.Map<OglasEntity>(oglas2));
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
                OglasEntity oglas = oglasRepository.GetOglasById(oglasId); ;
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
        public ActionResult<OglasDto> UpdateOglas(OglasEntity oglas)
        {
            try
            {
                if (oglasRepository.GetOglasById(oglas.OglasId) == null)
                {
                    return NotFound();
                }

                OglasEntity oglas1 = oglasRepository.UpdateOglas(oglas);

                return Ok(mapper.Map<OglasDto>(oglas1));

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska prilikom azuriranja oglasa!");
            }
        }

        [HttpOptions]
        public IActionResult GetOglasOptions()
        {
            Response.Headers.Add("Allow", "GET, HEAD, POST, PUT, DELETE");
            return Ok();
        }
    }
}
