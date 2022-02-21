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
using Zalba.ServiceCalls;

namespace Zalba.Controllers
{

    [ApiController]
    [Route("api/zalbas")]
    [Produces("application/json", "application/xml")]
    public class ZalbaController : ControllerBase
    {
        private readonly IZalbaRepository zalbaRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly IAuthService authService;
        private readonly ILicitacijaService licitacijaService;
        private readonly IPodnosilacZalbeService podnosilacZalbeService;
        public ZalbaController(IZalbaRepository zalbaRepository, LinkGenerator linkGenerator, IMapper mapper, IAuthService authService, ILicitacijaService licitacijaService, IPodnosilacZalbeService podnosilacZalbeService)
        {
            this.zalbaRepository = zalbaRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.authService = authService;
            this.licitacijaService = licitacijaService;
            this.podnosilacZalbeService = podnosilacZalbeService;
        }
       
        [HttpGet]
        [HttpHead]
        [AllowAnonymous]
        public ActionResult<List<ZalbaModelDto>> GetZalbas() 
        {
            try
            {
                var zalbas = zalbaRepository.GetZalbas();
                if (zalbas == null || zalbas.Count == 0)
                {
                    return NoContent();
                }
                foreach (ZalbaE z in zalbas)
                {
                    LicitacijaDto licitacija = licitacijaService.GetLicitacijaById(z.LicitacijaID).Result;
                    PodnosilacZalbeDto podnosilacZalbe = podnosilacZalbeService.GetPodnosilacZalbeById(z.PodnosilacZalbeID).Result;
                    if(licitacija != null)
                    {
                        z.Licitacija = licitacija;
                    }
                    if (podnosilacZalbe != null)
                    {
                        z.PodnosilacZalbe = podnosilacZalbe;
                    }
                }
                return Ok(mapper.Map<List<ZalbaModelDto>>(zalbas));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska");
            }
        }

        [HttpGet("{zalbaID}")]
        [AllowAnonymous]
        public ActionResult<ZalbaModelDto> GetZalba(Guid zalbaID)
        {
            try
            {
                var zalba = zalbaRepository.GetZalba(zalbaID);
                if (zalba == null)
                {
                    return NotFound();
                }
                LicitacijaDto licitacija = licitacijaService.GetLicitacijaById(zalba.LicitacijaID).Result;
                PodnosilacZalbeDto podnosilacZalbe = podnosilacZalbeService.GetPodnosilacZalbeById(zalba.PodnosilacZalbeID).Result;
                zalba.Licitacija = licitacija;
                zalba.PodnosilacZalbe = podnosilacZalbe;
                return Ok(mapper.Map<ZalbaModelDto>(zalba));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska");
            }

        }


        [HttpPost]
        public ActionResult<ZalbaCreationDto> CreateZalba([FromBody] ZalbaCreationDto zalba, [FromHeader] string key)
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

                var zalbaE = mapper.Map<ZalbaE>(zalba);
                var confirmation = zalbaRepository.CreateZalba(zalbaE);
                zalbaRepository.SaveChanges();
                string location = linkGenerator.GetPathByAction("GetZalba", "Zalba", new { zalbaID = confirmation.ZalbaID });
                return Created(location, mapper.Map<ZalbaConfirmationDto>(confirmation));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create error");
            }
        
        }

        [HttpDelete("{zalbaId}")]
        public IActionResult DeleteZalba(Guid zalbaId, [FromHeader] string key)
        {
            if (!authService.Authorize(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Korisnik nije autorizovan!");
            }
            try
            {
                var zalba = zalbaRepository.GetZalba(zalbaId);

                if(zalba == null)
                {
                    return NotFound();
                }

                zalbaRepository.DeleteZalba(zalbaId);
                zalbaRepository.SaveChanges();
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }

        [HttpPut]
        public ActionResult<ZalbaConfirmationDto> UpdateZalba(ZalbaModelDto zalba, [FromHeader] string key)
        {
            if (!authService.Authorize(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Korisnik nije autorizovan!");
            }
            try
            {
                //bool modelValid = ValidateZalba(zalba);
               
                if (zalbaRepository.GetZalba(zalba.ZalbaID) == null)
                {
                    return NotFound();
                }
                ZalbaE zalbaE = mapper.Map<ZalbaE>(zalba);
                mapper.Map(zalbaE, zalbaRepository.GetZalba(zalba.ZalbaID));
                zalbaRepository.SaveChanges();


                return Ok(mapper.Map<ZalbaModelDto>(zalbaRepository.GetZalba(zalba.ZalbaID)));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }
        
        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetZalbasOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }

        private bool ValidateZalba(ZalbaCreationDto zalba)
        {
            if(zalba.DatResenja < zalba.DatPodnosenjaZalbe)
            {
                return false;
            }
            if(zalba.DatResenja > DateTime.Now || zalba.DatPodnosenjaZalbe > DateTime.Now)
            {
                return false;
            }

            return true;
        }

    }
}
