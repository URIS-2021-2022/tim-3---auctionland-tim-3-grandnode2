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

        /// <summary>
        /// Vraca sve Zalbe
        /// </summary>
        /// <returns> Lista zalbi </returns>
        /// <response code="200">Vraca listu zalbi</response>
        /// <response code="204">Ne postoji nijedna zalba</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
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

        /// <summary>
        /// Vraca zalbu po ID-u
        /// </summary>
        /// <param name="zalbaID">ID zalbe</param>
        /// <returns>Odgovarajuca zalba</returns>
        /// <response code="200">Vraca trazenu zalbu</response>
        /// <response code="404">Nije pronadjena trazena zalba</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
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

        /// <summary>
        /// Kreiranje nove zalbe
        /// </summary>
        /// <param name="zalba">Model zalbe</param>
        /// <param name="key">Kljuc kojim se proverava autorizacija(key vrednost: MajaCetic)</param>
        /// <returns>Potvrda o kreiranju zalbe</returns>
        /// <response code="201">Vraca kreiranu zalbu</response>
        /// <response code="401">Lice koje zeli da izvrsi kreiranje zalbe nije autorizovani korisnik</response>
        /// <response code="500">Doslo je do greske na serveru prikilom kreiranja zalba</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Consumes("application/json")]
        [HttpPost]
        public ActionResult<ZalbaCreationDto> CreateZalba([FromBody] ZalbaCreationDto zalba, [FromHeader] string key)
        {
            if (!authService.Authorize(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Korisnik nije autorizovan!");
            }
            try
            {
                

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

        /// <summary>
        /// Brisanje zalbe
        /// </summary>
        /// <param name="zalbaId">ID zalbe</param>
        /// <param name="key">Kljuc kojim se proverava autorizacija(key vrednost: MajaCetic)</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Zalba uspesno obrisala</response>
        /// <response code="401">Lice koje zeli da izvrsi brisanje nije autorizovani korisnik</response>
        /// <response code="404">Nije pronadjena zalba za brisanje</response>
        /// <response code="500">Doslo je do greske na serveru prikilom brisanja zalbe</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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

        /// <summary>
        /// Azuriranje zalbe
        /// </summary>
        /// <param name="zalba">Model zalbe</param>
        /// <param name="key">Kljuc kojim se proverava autorizacija(key vrednost: MajaCetic)</param>
        /// <returns>Potvrda o izmenama u zalbi</returns>
        /// <response code="200">Vraca azuziranu zalbu</response>
        /// <response code="401">Lice koje zeli da izvrsi azuriranje nije autorizovani korisnik</response>
        /// <response code="404">Nije pronadjena zalba za azuriranje</response>
        /// <response code="500">Doslo je do greske na serveru prikilom azuriranja zalbe</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPut]
        public ActionResult<ZalbaConfirmationDto> UpdateZalba(ZalbaModelDto zalba, [FromHeader] string key)
        {
            if (!authService.Authorize(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Korisnik nije autorizovan!");
            }
            try
            {
               
               
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
        
        /// <summary>
        /// Vraca opcije za rad sa zalbama
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetZalbasOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }

       

    }
}
