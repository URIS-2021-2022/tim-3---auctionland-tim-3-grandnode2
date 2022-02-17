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
    [Route("api/prijaveZaNadmetanje")]
    public class PrijavaZaNadmetanjeController : ControllerBase
    {
        private readonly IPrijavaZaNadmetanjeRepository prijavaZaNadmetanjeRepository;
        private readonly LinkGenerator linkGenerator;

        public PrijavaZaNadmetanjeController(IPrijavaZaNadmetanjeRepository prijavaZaNadmetanjeRepository, LinkGenerator linkGenerator)
        {
            this.prijavaZaNadmetanjeRepository = prijavaZaNadmetanjeRepository;
            this.linkGenerator = linkGenerator;
        }

        [HttpGet]
        public ActionResult<List<PrijavaZaNadmetanjeModel>> GetPrijaveZaNadmetanje()
        {
            List<PrijavaZaNadmetanjeModel> prijavaZaNadmetanje = prijavaZaNadmetanjeRepository.GetPrijaveZaNadmetanje();

            if (prijavaZaNadmetanje == null || prijavaZaNadmetanje.Count == 0)
            {
                return NoContent();
            }
            return Ok(prijavaZaNadmetanje);
        }

        [HttpGet("{prijavaZaNadmetanjeId}")]
        public ActionResult<PrijavaZaNadmetanjeModel> GetPrijavaZaNadmetanjeById(Guid prijavaZaNadmetanjeId)
        {
            PrijavaZaNadmetanjeModel prijavaZaNadmetanje = prijavaZaNadmetanjeRepository.GetPrijavaZaNadmetanjeById(prijavaZaNadmetanjeId);

            if (prijavaZaNadmetanje == null)
            {
                return NotFound();
            }
            return Ok(prijavaZaNadmetanje);
        }

        [HttpPost]
        public ActionResult<PrijavaZaNadmetanjeModel> CreatePrijavaZaNadmetanje([FromBody] PrijavaZaNadmetanjeModel prijavaZaNadmetanje)
        {
            try
            {

                PrijavaZaNadmetanjeModel prijavaNadmetanje = prijavaZaNadmetanjeRepository.CreatePrijavaZaNadmetanje(prijavaZaNadmetanje);
                string location = linkGenerator.GetPathByAction("GetPrijaveZaNadmetanje", "PrijavaZaNadmetanje", new { prijavaZaNadmetanjeId = prijavaNadmetanje.PrijavaZaNadmetanjeId });
                return Created(location, prijavaNadmetanje);
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
                PrijavaZaNadmetanjeModel prijavaNadmetanje = prijavaZaNadmetanjeRepository.GetPrijavaZaNadmetanjeById(prijavaZaNadmetanjeId);
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
        public ActionResult<PrijavaZaNadmetanjeModel> UpdatePrijavaZaNadmetanje(PrijavaZaNadmetanjeModel prijavaZaNadmetanje)
        {
            try
            {

                if (prijavaZaNadmetanjeRepository.GetPrijavaZaNadmetanjeById(prijavaZaNadmetanje.PrijavaZaNadmetanjeId) == null)
                {
                    return NotFound();
                }

                return Ok(prijavaZaNadmetanjeRepository.UpdatePrijavaZaNadmetanje(prijavaZaNadmetanje));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska prilikom azuriranja prijave za nadmetanje!");
            }
        }
    }
}
