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
    [Route("api/statutiOpstine")]
    [Produces("application/json", "application/xml")]
    public class StatutOpstineController : ControllerBase
    {
        private readonly IStatutOpstineRepository statutOpstineRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly IAuthService authService;
        public StatutOpstineController(IStatutOpstineRepository statutOpstineRepository, LinkGenerator linkGenerator, IMapper mapper, IAuthService authService)
        {
            this.statutOpstineRepository = statutOpstineRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.authService = authService;
        }

        [HttpGet]
        [HttpHead]
        [AllowAnonymous]
        public ActionResult<List<StatutOpstineModelDto>> GetStatutOpstines() //ovde mi isto fale prosledjeni parametri
        {
            var statutOpstines = statutOpstineRepository.GetStatutOpstines();//ovde mi isto fale prosledjeni parametri
            if (statutOpstines == null || statutOpstines.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<StatutOpstineModelDto>>(statutOpstines));
        }

        [HttpGet("{StatutOpstineID}")]
        [AllowAnonymous]
        public ActionResult<StatutOpstineModelDto> GetStatutOpstine(Guid statutOpstineID)
        {
            var statutOpstine = statutOpstineRepository.GetStatutOpstine(statutOpstineID);

            if (statutOpstine == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<StatutOpstineModelDto>(statutOpstine));

        }


        [HttpPost]
        public ActionResult<StatutOpstineCreationDto> CreateStatutOpstine([FromBody] StatutOpstineCreationDto statutOpstine, [FromHeader] string key)
        {
            if (!authService.Authorize(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Korisnik nije autorizovan!");
            }
            try
            {
                /*bool modelValid = ValidatestatusOpstine(statusOpstine);

               if (!modelValid)
                {
                    return BadRequest("statusOpstine ne odgovara");
                }*/

                var statutOpstineE = mapper.Map<StatutOpstineE>(statutOpstine);
                var confirmation = statutOpstineRepository.CreateStatutOpstine(statutOpstineE);
                statutOpstineRepository.SaveChanges();
                string location = linkGenerator.GetPathByAction("GetStatutOpstine", "StatutOpstine", new { statutOpstineID = confirmation.StatutOpstineID });
                return Created(location, mapper.Map<StatutOpstineConfirmationDto>(confirmation));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create error");
            }

        }

        [HttpDelete("{StatutOpstineID}")]
        public IActionResult DeleteStatutOpstine(Guid statutOpstineID, [FromHeader] string key)
        {
            if (!authService.Authorize(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Korisnik nije autorizovan!");
            }
            try
            {
                var statutOpstine = statutOpstineRepository.GetStatutOpstine(statutOpstineID);

                if (statutOpstine == null)
                {
                    return NotFound();
                }

                statutOpstineRepository.DeleteStatutOpstine(statutOpstineID);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }

        [HttpPut]
        public ActionResult<StatutOpstineConfirmationDto> UpdateStatutOpstine(StatutOpstineModelDto statutOpstine, [FromHeader] string key)
        {
            if (!authService.Authorize(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Korisnik nije autorizovan!");
            }
            try
            {
                //bool modelValid = ValidatestatusOpstine(statusOpstine);

                if (statutOpstineRepository.GetStatutOpstine(statutOpstine.StatutOpstineID) == null)
                {
                    return NotFound();
                }
                StatutOpstineE statutOpstineE = mapper.Map<StatutOpstineE>(statutOpstine);
                mapper.Map(statutOpstineE, statutOpstineRepository.GetStatutOpstine(statutOpstine.StatutOpstineID));
                statutOpstineRepository.SaveChanges();


                return Ok(mapper.Map<StatutOpstineModelDto>(statutOpstineRepository.GetStatutOpstine(statutOpstine.StatutOpstineID)));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetStatutOpstinesOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
