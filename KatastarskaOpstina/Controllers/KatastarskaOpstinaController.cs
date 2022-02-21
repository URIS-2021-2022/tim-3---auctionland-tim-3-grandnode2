using AutoMapper;
using KatastarskaOpstina.Auth;
using KatastarskaOpstina.Data;
using KatastarskaOpstina.Entities;
using KatastarskaOpstina.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KatastarskaOpstina.Controllers
{
    [ApiController]
    [Route("api/katastarskeOpstine")]
    [Produces("application/json", "application/xml")]
    public class KatastarskaOpstinaController : ControllerBase
    {
        private readonly IKatastarskaOpstinaRepository katastarskaOpstinaRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly IAuthService authService;
        public KatastarskaOpstinaController(IKatastarskaOpstinaRepository katastarskaOpstinaRepository, LinkGenerator linkGenerator, IMapper mapper, IAuthService authService)
        {
            this.katastarskaOpstinaRepository = katastarskaOpstinaRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.authService = authService;
        }

        [HttpGet]
        [HttpHead]
        [AllowAnonymous]
        public ActionResult<List<KatastarskaOpstinaModelDto>> GetKatastarskaOpstinas() 
        {
            var katastarskaOpstinas = katastarskaOpstinaRepository.GetKatastarskaOpstinas();
            if (katastarskaOpstinas == null || katastarskaOpstinas.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<KatastarskaOpstinaModelDto>>(katastarskaOpstinas));
        }

        [HttpGet("{katastarskaOpstinaID}")]
        [AllowAnonymous]
        public ActionResult<KatastarskaOpstinaModelDto> GetKatastarskaOpstina(Guid katastarskaOpstinaID)
        {
            var katastarskaOpstinas = katastarskaOpstinaRepository.GetKatastarskaOpstinaById(katastarskaOpstinaID);

            if (katastarskaOpstinas == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<KatastarskaOpstinaModelDto>(katastarskaOpstinas));

        }


        [HttpPost]
        public ActionResult<KatastarskaOpstinaCreationDto> CreateKatastarskaOpstina([FromBody] KatastarskaOpstinaCreationDto katastarskaOpstina, [FromHeader] string key)
        {
            if (!authService.Authorize(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Korisnik nije autorizovan!");
            }
            try
            {
                /*bool modelValid = ValidatekatastarskaOpstina(katastarskaOpstina);

               if (!modelValid)
                {
                    return BadRequest("katastarskaOpstina ne odgovara");
                }*/

                var katastarskaOpstinaE = mapper.Map<KatastarskaOpstinaE>(katastarskaOpstina);
                var confirmation = katastarskaOpstinaRepository.CreateKatastarskaOpstina(katastarskaOpstinaE);
                katastarskaOpstinaRepository.SaveChanges();
                string location = linkGenerator.GetPathByAction("GetKatastarskaOpstina", "katastarskaOpstina", new { katastarskaOpstinaID = confirmation.KatastarskaOpstinaID });
                return Created(location, mapper.Map<KatastarskaOpstinaConfirmationDto>(confirmation));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create error");
            }

        }

        [HttpDelete("{katastarskaOpstinaID}")]
        public IActionResult DeleteKatastarskaOpstina(Guid katastarskaOpstinaID, [FromHeader] string key)
        {
            if (!authService.Authorize(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Korisnik nije autorizovan!");
            }
            try
            {
                var katastarskaOpstina = katastarskaOpstinaRepository.GetKatastarskaOpstinaById(katastarskaOpstinaID);

                if (katastarskaOpstina == null)
                {
                    return NotFound();
                }

                katastarskaOpstinaRepository.DeleteKatastarskaOpstina(katastarskaOpstinaID);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }

        [HttpPut]
        public ActionResult<KatastarskaOpstinaConfirmationDto> UpdateTipZalbe(KatastarskaOpstinaModelDto katastarskaOpstina, [FromHeader] string key)
        {
            if (!authService.Authorize(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Korisnik nije autorizovan!");
            }
            try
            {
                //bool modelValid = ValidatekatastarskaOpstina(katastarskaOpstina);

                if (katastarskaOpstinaRepository.GetKatastarskaOpstinaById(katastarskaOpstina.KatastarskaOpstinaID) == null)
                {
                    return NotFound();
                }
                KatastarskaOpstinaE katastarskaOpstinaE = mapper.Map<KatastarskaOpstinaE>(katastarskaOpstina);
                mapper.Map(katastarskaOpstinaE, katastarskaOpstinaRepository.GetKatastarskaOpstinaById(katastarskaOpstina.KatastarskaOpstinaID));
                katastarskaOpstinaRepository.SaveChanges();


                return Ok(mapper.Map<KatastarskaOpstinaModelDto>(katastarskaOpstinaRepository.GetKatastarskaOpstinaById(katastarskaOpstina.KatastarskaOpstinaID)));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetKatastarskaOpstinasOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }

    }
}
