using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ParcelaService.Auth;
using ParcelaService.Data;
using ParcelaService.Entities;
using ParcelaService.Logger;
using ParcelaService.Models;
using ParcelaService.Models.ConfirmationDto;
using ParcelaService.Models.CreateDto;
using ParcelaService.Models.UpdateDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelaService.Controllers
{
    [ApiController]
    [Route("api/kvalitetiZemljista")]
    [Produces("application/json", "application/xml")]
    public class KvalitetZemljistaController : ControllerBase
    {
        private readonly IKvalitetZemljistaRepository _kvalitetZemljistaRepository;
        private readonly LinkGenerator _linkGenerator;
        private readonly IMapper _mapper;
        private readonly IAuthHelper _authHelper;
        private readonly LoggerService _logger;

        public KvalitetZemljistaController(IKvalitetZemljistaRepository kvalitetZemljistaRepository, LinkGenerator linkGenerator, IMapper mapper, IAuthHelper authHelper)
        {
            _kvalitetZemljistaRepository = kvalitetZemljistaRepository;
            _linkGenerator = linkGenerator;
            _mapper = mapper;
            _authHelper = authHelper;
            _logger = new LoggerService();
        }

        /// <summary>
        /// Vraća sve kvalitete zemljišta
        /// </summary>
        /// <returns>Lista kvaliteta zemljišta</returns>
        /// <response code = "200">Vraća listu kvaliteta zemljišta</response>
        /// <response code = "204">Ne postoji nijedan kvalitet zemljišta</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        [AllowAnonymous]
        public ActionResult<List<KvalitetZemljistaDto>> GetAll()
        {
            #pragma warning disable CS4014
            _logger.PostLogger("Pristup svim kvalitetima zemljista." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
            #pragma warning restore CS4014
            var kvalitetiZemljista = _kvalitetZemljistaRepository.GetAll();
            if (kvalitetiZemljista == null || kvalitetiZemljista.Count == 0)
            {
                return NoContent();
            }
            return Ok(_mapper.Map<List<KvalitetZemljistaDto>>(kvalitetiZemljista));
        }

        /// <summary>
        /// Vraća kvalitet zemljišta sa unetim ID
        /// </summary>
        /// <param name="kvalitetZemljistaId">ID kvaliteta zemljišta</param>
        /// <returns>Odgovarajući kvalitet zemljišta</returns>
        /// <response code = "200">Vraća traženi kvalitet zemljišta</response>
        /// <response code = "404">Nije pronađen traženi kvalitet zemljišta</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{kvalitetZemljistaId}")]
        [AllowAnonymous]
        public ActionResult<KvalitetZemljistaDto> GetById(Guid kvalitetZemljistaId)
        {
            #pragma warning disable CS4014
            _logger.PostLogger("Pristup kvalitetu zemljista." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
            #pragma warning restore CS4014
            var kvalitetZemljista = _kvalitetZemljistaRepository.GetById(kvalitetZemljistaId);
            if (kvalitetZemljista == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<KvalitetZemljistaDto>(kvalitetZemljista));
        }

        /// <summary>
        /// Kreiranje novog kvaliteta zemljišta
        /// </summary>
        /// <param name="kvalitetZemljista">Model kvaliteta zemljišta</param>
        /// <param name="key"> ključ sa kojim se proverava autorizacija(key vrednost: Bearer Jovana123)</param>
        /// <returns>Potvrdu o kreiranju kvaliteta zemljišta</returns>
        /// <response code = "201">Vraća kreirani kvalitet zemljišta</response>
        /// <response code="401">Lice koje želi da izvrši kreiranje kvaliteta zemljišta nije autorizovani korisnik</response>
        /// <response code = "500">Došlo je do greške na serveru prilikom kreiranja kvaliteta zemljišta</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Consumes("application/json")]
        [HttpPost]
        public ActionResult<KvalitetZemljistaDto> Create([FromBody] KvalitetZemljistaCreateDto kvalitetZemljista, [FromHeader] string key)
        {
            #pragma warning disable CS4014
            _logger.PostLogger("Kreiranje kvaliteta zemljista." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
            #pragma warning restore CS4014
            if (!_authHelper.Authorize(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Korisnik nije autorizovan!");
            }
            try
            {
                var kvalitetZemljistaEntity = _mapper.Map<KvalitetZemljista>(kvalitetZemljista);
                var confirmation = _kvalitetZemljistaRepository.Create(kvalitetZemljistaEntity);
                _kvalitetZemljistaRepository.SaveChanges();
                string location = _linkGenerator.GetPathByAction("GetById", "KvalitetZemljista", new { kvalitetZemljistaId = confirmation.KvalitetZemljistaId });
                return Created(location, _mapper.Map<KvalitetZemljistaConfirmationDto>(confirmation));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska");
            }
        }

        /// <summary>
        /// Ažuriranje kvaliteta zemljišta
        /// </summary>
        /// <param name="kvalitetZemljista">Model kvaliteta zemljišta</param>
        /// <param name="key"> ključ sa kojim se proverava autorizacija(key vrednost: Bearer Jovana123)</param>
        /// <returns>Potvrda o izmenama u kvalitetu zemljšsta</returns>
        /// <response code="200">Vraća ažurirani kvalitet zemljišta</response>
        /// <response code="401">Lice koje želi da izvrši ažuriranje nije autorizovani korisnik</response>
        /// <response code="404">Nije pronađen kvalitet zemljišta za ažuriranje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom ažuriranja kvaliteta zemljišta</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPut]
        public ActionResult<KvalitetZemljistaConfirmationDto> Update(KvalitetZemljistaUpdateDto kvalitetZemljista, [FromHeader] string key)
        {
            #pragma warning disable CS4014
            _logger.PostLogger("Azuriranje kvaliteta zemljista." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
            #pragma warning restore CS4014
            if (!_authHelper.Authorize(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Korisnik nije autorizovan!");
            }
            try
            {
                var oldKvalitetZemljista = _kvalitetZemljistaRepository.GetById(kvalitetZemljista.KvalitetZemljistaId);
                if (oldKvalitetZemljista == null)
                {
                    return NotFound();
                }
                KvalitetZemljista kvalitetZemljistaEntity = _mapper.Map<KvalitetZemljista>(kvalitetZemljista);

                _mapper.Map(kvalitetZemljistaEntity, oldKvalitetZemljista);
                _kvalitetZemljistaRepository.SaveChanges();
                return Ok(_mapper.Map<KvalitetZemljistaDto>(oldKvalitetZemljista));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska");
            }
        }

        /// <summary>
        /// Brisanje kvaliteta zemljišta
        /// </summary>
        /// <param name="kvalitetZemljistaId">ID kvaliteta zemljišta</param>
        ///  <param name="key"> ključ sa kojim se proverava autorizacija(key vrednost: Bearer Jovana123)</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Kvalitet zemljišta uspešno obrisan</response>
        /// <response code="401">Lice koje želi da izvrši brisanje nije autorizovani korisnik</response>
        /// <response code="404">Nije pronađen kvalitet zemljišta  za brisanje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom brisanja kvaliteta zemljišta</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpDelete("{kvalitetZemljistaId}")]
        public IActionResult Delete(Guid kvalitetZemljistaId, [FromHeader] string key)
        {
            #pragma warning disable CS4014
            _logger.PostLogger("Brisanje kvaliteta zemljista." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
            #pragma warning restore CS4014
            if (!_authHelper.Authorize(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Korisnik nije autorizovan!");
            }
            try
            {
                var kvalitetZemljista = _kvalitetZemljistaRepository.GetById(kvalitetZemljistaId);
                if (kvalitetZemljista == null)
                {
                    return NotFound();
                }

                _kvalitetZemljistaRepository.Delete(kvalitetZemljistaId);
                _kvalitetZemljistaRepository.SaveChanges();
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska");
            }
        }

        /// <summary>
        /// Vraća opcije za rad kvalitetima zemljišta
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetKvalitetZemljistaOptions()
        {
            #pragma warning disable CS4014
            _logger.PostLogger("Pristup opcijama." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
            #pragma warning restore CS4014
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
