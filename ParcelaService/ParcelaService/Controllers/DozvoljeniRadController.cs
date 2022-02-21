using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ParcelaService.Auth;
using ParcelaService.Data;
using ParcelaService.Entities;
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
    [Route("api/dozvoljeniRadovi")]
    [Produces("application/json", "application/xml")]
    public class DozvoljeniRadController : ControllerBase
    {
        private readonly IDozvoljeniRadRepository _dozvoljeniRadRepository;
        private readonly LinkGenerator _linkGenerator;
        private readonly IMapper _mapper;
        private readonly IAuthHelper _authHelper;

        public DozvoljeniRadController(IDozvoljeniRadRepository dozvoljeniRadRepository, LinkGenerator linkGenerator, IMapper mapper, IAuthHelper authHelper)
        {
            _dozvoljeniRadRepository = dozvoljeniRadRepository;
            _linkGenerator = linkGenerator;
            _mapper = mapper;
            _authHelper = authHelper;
        }

        /// <summary>
        /// Vraća sve dozvoljene radove
        /// </summary>
        /// <returns>Lista dozvoljenih radova</returns>
        /// <response code = "200">Vraća listu dozvoljenih radova</response>
        /// <response code = "204">Ne postoji nijedan dozvoljeni rad</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        [AllowAnonymous]
        public ActionResult<List<DozvoljeniRadDto>> GetAll()
        {
            var dozvoljeniRadovi = _dozvoljeniRadRepository.GetAll();
            if (dozvoljeniRadovi == null || dozvoljeniRadovi.Count == 0)
            {
                return NoContent();
            }
            return Ok(_mapper.Map<List<DozvoljeniRadDto>>(dozvoljeniRadovi));
        }

        /// <summary>
        /// Vrača dozvoljene radove po ID-ju
        /// </summary>
        /// <param name="dozvoljeniRadId">ID dozvoljenog rada</param>
        /// <returns>Odgovarajući dozvoljeni rad</returns>
        /// <response code = "200">Vraća traženi dozvoljeni rad</response>
        /// <response code = "404">Nije pronađen traženi dozvoljeni rad</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{dozvoljeniRadId}")]
        [AllowAnonymous]
        public ActionResult<DozvoljeniRadDto> GetById(Guid dozvoljeniRadId)
        {
            var dozvoljeniRad = _dozvoljeniRadRepository.GetById(dozvoljeniRadId);
            if (dozvoljeniRad == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<DozvoljeniRadDto>(dozvoljeniRad));
        }

        /// <summary>
        /// Kreiranje novog dozvoljenog rada
        /// </summary>
        /// <param name="dozvoljeniRad">Model dozvoljenog rada/param>
        /// <param name="key"> ključ sa kojim se proverava autorizacija(key vrednost: Bearer Jovana123)</param>
        /// <returns>Potvrda o kreiranju dozvoljenog rada</returns>
        /// <response code = "201">Vraća kreirani dozvoljeni rad</response>
        /// <response code="401">Lice koje želi da izvrši kreiranje dozvoljenog rada nije autorizovani korisnik</response>
        /// <response code = "500">Došlo je do greške na serveru prilikom kreiranja dozvoljenog rada</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Consumes("application/json")]
        [HttpPost]
        public ActionResult<DozvoljeniRadDto> Create([FromBody] DozvoljeniRadCreateDto dozvoljeniRad, [FromHeader] string key)
        {
            if (!_authHelper.Authorize(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Korisnik nije autorizovan!");
            }
            try
            {
                var dozvoljeniRadEntity = _mapper.Map<DozvoljeniRad>(dozvoljeniRad);
                var confirmation = _dozvoljeniRadRepository.Create(dozvoljeniRadEntity);
                _dozvoljeniRadRepository.SaveChanges();
                string location = _linkGenerator.GetPathByAction("GetById", "DozvoljeniRad", new { dozvoljeniRadId = confirmation.DozvoljeniRadId });
                return Created(location, _mapper.Map<DozvoljeniRadConfirmationDto>(confirmation));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska");
            }
        }

        /// <summary>
        /// Ažuriranje dozvoljenih radova
        /// </summary>
        /// <param name="dozvoljeniRad">Model dozvoljenog rada</param>
        /// <param name="key"> ključ sa kojim se proverava autorizacija(key vrednost: Bearer Jovana123)</param>
        /// <returns>Potvrda o izmenama u dozvoljenom radu</returns>
        /// <response code="200">Vraća ažuriran dozvoljeni rada</response>
        /// <response code="401">Lice koje želi da izvrši ažuriranje nije autorizovani korisnik</response>
        /// <response code="404">Nije pronađen dozvoljeni rad za ažuriranje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom ažuriranja dozvoljenog rada</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPut]
        public ActionResult<DozvoljeniRadConfirmationDto> Update(DozvoljeniRadUpdateDto dozvoljeniRad, [FromHeader] string key)
        {
            if (!_authHelper.Authorize(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Korisnik nije autorizovan!");
            }
            try
            {
                var oldDozvoljeniRad = _dozvoljeniRadRepository.GetById(dozvoljeniRad.DozvoljeniRadId);
                if (oldDozvoljeniRad == null)
                {
                    return NotFound();
                }
                DozvoljeniRad dozvoljeniRadEntity = _mapper.Map<DozvoljeniRad>(dozvoljeniRad);

                _mapper.Map(dozvoljeniRadEntity, oldDozvoljeniRad);
                _dozvoljeniRadRepository.SaveChanges();
                return Ok(_mapper.Map<DozvoljeniRadDto>(oldDozvoljeniRad));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska");
            }
        }

        /// <summary>
        /// Brisanje dozvoljenog rada
        /// </summary>
        /// <param name="dozvoljeniRadId">Id dozvoljenog rada</param>
        /// <param name="key"> ključ sa kojim se proverava autorizacija(key vrednost: Bearer Jovana123)</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Dozvoljeni rad uspešno obrisan</response>
        /// <response code="401">Lice koje želi da izvrši brisanje nije autorizovani korisnik</response>
        /// <response code="404">Nije pronađen dozvoljeni rad za brisanje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom brisanja dozvoljenog rada</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpDelete("{dozvoljeniRadId}")]
        public IActionResult Delete(Guid dozvoljeniRadId, [FromHeader] string key)
        {
            if (!_authHelper.Authorize(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Korisnik nije autorizovan!");
            }
            try
            {
                var dozvoljeniRad = _dozvoljeniRadRepository.GetById(dozvoljeniRadId);
                if (dozvoljeniRad == null)
                {
                    return NotFound();
                }

                _dozvoljeniRadRepository.Delete(dozvoljeniRadId);
                _dozvoljeniRadRepository.SaveChanges();
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska");
            }
        }

        /// <summary>
        /// Vraća opcije za rad dozvoljenim radovima
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetDozvoljeniRadOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
