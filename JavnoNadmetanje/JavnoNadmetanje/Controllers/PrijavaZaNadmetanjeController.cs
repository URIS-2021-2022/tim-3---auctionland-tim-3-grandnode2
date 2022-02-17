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
    [Route("api/prijaveZaNadmetanje")]
    public class PrijavaZaNadmetanjeController : ControllerBase
    {
        private readonly IPrijavaZaNadmetanjeRepository prijavaZaNadmetanjeRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        
        public PrijavaZaNadmetanjeController(IPrijavaZaNadmetanjeRepository prijavaZaNadmetanjeRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.prijavaZaNadmetanjeRepository = prijavaZaNadmetanjeRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [HttpGet]
        [HttpHead]
        public ActionResult<List<PrijavaZaNadmetanjeDto>> GetPrijaveZaNadmetanje()
        {
            List<PrijavaZaNadmetanjeEntity> prijaveZaNadmetanje = prijavaZaNadmetanjeRepository.GetPrijaveZaNadmetanje();

            if (prijaveZaNadmetanje == null || prijaveZaNadmetanje.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<PrijavaZaNadmetanjeDto>>(prijaveZaNadmetanje));
        }

        [HttpGet("{prijavaZaNadmetanjeId}")]
        public ActionResult<PrijavaZaNadmetanjeDto> GetPrijavaZaNadmetanjeById(Guid prijavaZaNadmetanjeId)
        {
            PrijavaZaNadmetanjeEntity prijavaZaNadmetanje = prijavaZaNadmetanjeRepository.GetPrijavaZaNadmetanjeById(prijavaZaNadmetanjeId);

            if (prijavaZaNadmetanje == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<PrijavaZaNadmetanjeDto>(prijavaZaNadmetanje));
        }

        [HttpPost]
        public ActionResult<PrijavaZaNadmetanjeDto> CreatePrijavaZaNadmetanje([FromBody] PrijavaZaNadmetanjeDto prijavaZaNadmetanje)
        {
            try
            {
                PrijavaZaNadmetanjeEntity prijavaNadmetanje = mapper.Map<PrijavaZaNadmetanjeEntity>(prijavaZaNadmetanje);
                PrijavaZaNadmetanjeEntity prijavaNadmetanje1 = prijavaZaNadmetanjeRepository.CreatePrijavaZaNadmetanje(prijavaNadmetanje);
                string location = linkGenerator.GetPathByAction("GetPrijaveZaNadmetanje", "PrijavaZaNadmetanje", new { prijavaZaNadmetanjeId = prijavaNadmetanje.PrijavaZaNadmetanjeId });
                return Created(location, mapper.Map<PrijavaZaNadmetanjeEntity>(prijavaNadmetanje1));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska prilikom kreiranja prijave za nadmetanje!");
            }
        }

        [HttpDelete("{prijavaZaNadmetanjeId}")]
        public IActionResult DeletePrijavaZaNadmetanje(Guid prijavaZaNadmetanjeId)
        {
            try
            {
                PrijavaZaNadmetanjeEntity prijavaNadmetanje = prijavaZaNadmetanjeRepository.GetPrijavaZaNadmetanjeById(prijavaZaNadmetanjeId);
                if (prijavaNadmetanje == null)
                {
                    return NotFound();
                }

                prijavaZaNadmetanjeRepository.DeletePrijavaZaNadmetanje(prijavaZaNadmetanjeId);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska prilikom brisanja prijave za nadmetanje!");
            }
        }

        [HttpPut]
        public ActionResult<PrijavaZaNadmetanjeDto> UpdatePrijavaZaNadmetanje(PrijavaZaNadmetanjeEntity prijavaZaNadmetanje)
        {
            try
            {

                if (prijavaZaNadmetanjeRepository.GetPrijavaZaNadmetanjeById(prijavaZaNadmetanje.PrijavaZaNadmetanjeId) == null)
                {
                    return NotFound();
                }

                PrijavaZaNadmetanjeEntity prijavaZaNadmetanje1 = prijavaZaNadmetanjeRepository.UpdatePrijavaZaNadmetanje(prijavaZaNadmetanje);

                return Ok(mapper.Map<PrijavaZaNadmetanjeDto>(prijavaZaNadmetanje1));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska prilikom azuriranja prijave za nadmetanje!");
            }
        }

        [HttpOptions]
        public IActionResult GetPrijavaZaNadmetanjeOptions()
        {
            Response.Headers.Add("Allow", "GET, HEAD, POST, PUT, DELETE");
            return Ok();
        }
    }
}
