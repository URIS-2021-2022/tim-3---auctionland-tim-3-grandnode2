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

        /// <summary>
        /// Vraca sve tipove zalbe
        /// </summary>
        /// <returns> Lista tipova zalbi </returns>
        /// <response code="200">Vraca listu tipova zalbi</response>
        /// <response code="204">Ne postoji nijedan tip zalbe</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
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

        /// <summary>
        /// Vraca tip zalbe po ID-u
        /// </summary>
        /// <param name="zalbaID">ID tipa zalbe</param>
        /// <returns>Odgovarajuc tip zalbe</returns>
        /// <response code="200">Vraca trazen tip zalbe</response>
        /// <response code="404">Nije pronadjen trazen tip zalbe</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
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

        /// <summary>
        /// Kreiranje novog tipa zalbe
        /// </summary>
        /// <param name="zalba">Model tipa zalbe</param>
        /// <param name="key">Kljuc kojim se proverava autorizacija(key vrednost: MajaCetic)</param>
        /// <returns>Potvrda o kreiranju tipa zalbe</returns>
        /// <response code="201">Vraca kreiran tip zalbe</response>
        /// <response code="401">Lice koje zeli da izvrsi kreiranje tipa zalbe nije autorizovani korisnik</response>
        /// <response code="500">Doslo je do greske na serveru prikilom kreiranja tipa zalbe</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Consumes("application/json")]
        [HttpPost]
        public ActionResult<TipZalbeCreationDto> CreateTipZalbe([FromBody] TipZalbeCreationDto tipZalbe, [FromHeader] string key)
        {
            if (!authService.Authorize(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Korisnik nije autorizovan!");
            }
            try
            {
                

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
        /// <summary>
        /// Brisanje tipa zalbe
        /// </summary>
        /// <param name="zalbaId">ID tipa zalbe</param>
        /// <param name="key">Kljuc kojim se proverava autorizacija(key vrednost: MajaCetic)</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Tip zalbe uspesno obrisan</response>
        /// <response code="401">Lice koje zeli da izvrsi brisanje nije autorizovani korisnik</response>
        /// <response code="404">Nije pronadjen tip zalbe za brisanje</response>
        /// <response code="500">Doslo je do greske na serveru prikilom brisanja tipa zalbe</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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

        /// <summary>
        /// Azuriranje tipa zalbe
        /// </summary>
        /// <param name="zalba">Model tipa zalbe</param>
        /// <param name="key">Kljuc kojim se proverava autorizacija(key vrednost: MajaCetic)</param>
        /// <returns>Potvrda o izmenama u tipu zalbe</returns>
        /// <response code="200">Vraca azuziran tip zalbe</response>
        /// <response code="401">Lice koje zeli da izvrsi azuriranje nije autorizovani korisnik</response>
        /// <response code="404">Nije pronadjen tip zalbe za azuriranje</response>
        /// <response code="500">Doslo je do greske na serveru prikilom azuriranja tipa zalbe</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPut]
        public ActionResult<TipZalbeConfirmationDto> UpdateTipZalbe(TipZalbeModelDto tipZalbe, [FromHeader] string key)
        {
            if (!authService.Authorize(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Korisnik nije autorizovan!");
            }
            try
            {
              

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

        /// <summary>
        /// Vraca opcije za rad sa tipovima zalbi
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetTipZalbesOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }

       
    }
}
