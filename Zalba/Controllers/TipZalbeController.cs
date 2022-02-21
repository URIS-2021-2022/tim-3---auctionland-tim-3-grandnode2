using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zalba.Auth;
using Zalba.Data;
using Zalba.Entities;
using Zalba.Models;

namespace Zalba.Controllers
{
    [ApiController]
    [Route("api/tipZalbas")]
    [Produces("application/json", "application/xml")]
    public class TipZalbeController : ControllerBase
    {
        private readonly ITipZalbeRepository tipZalbeRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly IAuthService authService;
        public TipZalbeController(ITipZalbeRepository zalbaRepository, LinkGenerator linkGenerator, IMapper mapper, IAuthService authService)
        {
            this.tipZalbeRepository = zalbaRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.authService = authService;
        }

        [HttpGet]
        [HttpHead]
        [AllowAnonymous]
        public ActionResult<List<TipZalbeModelDto>> GetTipZalbes() //ovde mi isto fale prosledjeni parametri
        {
            var tipZalbes = tipZalbeRepository.GetTipZalbes();//ovde mi isto fale prosledjeni parametri
            if (tipZalbes == null || tipZalbes.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<TipZalbeModelDto>>(tipZalbes));
        }

        [HttpGet("{tipZalbeID}")]
        [AllowAnonymous]
        public ActionResult<TipZalbeModelDto> GetTipZalbe(Guid tipZalbeID)
        {
            var tipZalbe = tipZalbeRepository.GetTipZalbe(tipZalbeID);

            if (tipZalbe == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<TipZalbeModelDto>(tipZalbe));

        }

        [HttpPost]
        public ActionResult<TipZalbeCreationDto> CreateTipZalbe([FromBody] TipZalbeCreationDto tipZalbe, [FromHeader] string key)
        {
            if (!authService.Authorize(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Korisnik nije autorizovan!");
            }
            try
            {
                /*bool modelValid = ValidateZalba(zalba);

               if (!modelValid)
                {
                    return BadRequest("Zalba ne odgovara");
                }*/

                var tipZalbeE = mapper.Map<TipZalbeE>(tipZalbe);
                var confirmation = tipZalbeRepository.CreateTipZalbe(tipZalbeE);
                tipZalbeRepository.SaveChanges();
                string location = linkGenerator.GetPathByAction("GetTipZalbe", "TipZalbe", new { tipZalbeID = confirmation.TipZalbeID });
                return Created(location, mapper.Map<TipZalbeConfirmationDto>(confirmation));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create error");
            }

        }
        [HttpDelete("{tipZalbeId}")]
        public IActionResult DeleteTipZalbe(Guid tipZalbeId, [FromHeader] string key)
        {
            if (!authService.Authorize(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Korisnik nije autorizovan!");
            }
            try
            {
                var zalba = tipZalbeRepository.GetTipZalbe(tipZalbeId);

                if (zalba == null)
                {
                    return NotFound();
                }

                tipZalbeRepository.DeleteTipZalbe(tipZalbeId);
                tipZalbeRepository.SaveChanges();
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }

        [HttpPut]
        public ActionResult<TipZalbeConfirmationDto> UpdateTipZalbe(TipZalbeModelDto tipZalbe, [FromHeader] string key)
        {
            if (!authService.Authorize(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Korisnik nije autorizovan!");
            }
            try
            {
                //bool modelValid = ValidateZalba(zalba);

                if (tipZalbeRepository.GetTipZalbe(tipZalbe.TipZalbeID) == null)
                {
                    return NotFound();
                }
                TipZalbeE tipZalbeE = mapper.Map<TipZalbeE>(tipZalbe);
                mapper.Map(tipZalbeE, tipZalbeRepository.GetTipZalbe(tipZalbe.TipZalbeID));
                tipZalbeRepository.SaveChanges();


                return Ok(mapper.Map<TipZalbeModelDto>(tipZalbeRepository.GetTipZalbe(tipZalbe.TipZalbeID)));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetTipZalbesOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }

       /* private bool ValidateZalba(KreiranjeZalbeDto zalba)
        {
            if (zalba.DatResenja < zalba.DatPodnosenjaZalbe)
            {
                return false;
            }
            if (zalba.DatResenja > DateTime.Now || zalba.DatPodnosenjaZalbe > DateTime.Now)
            {
                return false;
            }

            return true;
        }*/
    }
}
