using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UgovorService.Data;
using UgovorService.Models;
using UgovorService.Entities;
using Microsoft.AspNetCore.Authorization;
using UgovorService.Auth;
using UgovorService.ServiceCalls;

namespace UgovorService.Controllers
{
    [ApiController]
    [Route("api/ugovori")]
    [Produces("application/json", "application/xml")]
    public class UgovorController : ControllerBase
    {
        private readonly IUgovorRepository _ugovorRepository;
        private readonly LinkGenerator _linkGenerator;
        private readonly IMapper _mapper;
        private readonly IAuthHelper _authHelper;
        private readonly ILicitacijaService _licitacijaService;
        private readonly ILiceService _liceService;

        public UgovorController(IUgovorRepository ugovorRepository, LinkGenerator linkGenerator, IMapper mapper, IAuthHelper authHelper, ILicitacijaService licitacijaService, ILiceService liceService)
        {
            _ugovorRepository = ugovorRepository;
            _linkGenerator = linkGenerator;
            _mapper = mapper;
            _authHelper = authHelper;
            _licitacijaService = licitacijaService;
            _liceService = liceService;
        }

        /// <summary>
        /// Vraća sve ugovore
        /// </summary>
        /// <returns>Lista ugovora</returns>
        /// <response code = "200">Vraća listu ugovora</response>
        /// <response code = "204">Ne postoji nijedan ugovor</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        [HttpHead]
        [AllowAnonymous]
        public ActionResult<List<UgovorDto>> GetAll()
        {
            try
            {
                var ugovori = _ugovorRepository.GetAll();
                if (ugovori == null || ugovori.Count == 0)
                {
                    return NoContent();
                }

                foreach (Ugovor u in ugovori)
                {
                    LicitacijaDto licitacija = _licitacijaService.GetLicitacijaById(u.LicitacijaId).Result;
                    LiceDto lice = _liceService.GetLiceById(u.LiceId).Result;
                    if (licitacija != null)
                    {
                        u.Licitacija = licitacija;
                    }
                    if (lice != null)
                    {
                        u.Lice = lice;
                    }
                }
                return Ok(_mapper.Map<List<UgovorDto>>(ugovori));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska");
            }
        }

        /// <summary>
        /// Vraća ugovor po ID-ju
        /// </summary>
        /// <param name="ugovorId">ID ugovora</param>
        /// <returns>Odgovarajući ugovor</returns>
        /// <response code = "200">Vraća traženi ugovor</response>
        /// <response code = "404">Nije pronađen traženi ugovor</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{ugovorId}")]
        [AllowAnonymous]
        public ActionResult<UgovorDto> GetById(Guid ugovorId)
        {
            try
            {
                var ugovor = _ugovorRepository.GetById(ugovorId);
                if (ugovor == null)
                {
                    return NotFound();
                }
                LicitacijaDto licitacija = _licitacijaService.GetLicitacijaById(ugovor.LicitacijaId).Result;
                LiceDto lice = _liceService.GetLiceById(ugovor.LiceId).Result;
                ugovor.Licitacija = licitacija;
                ugovor.Lice = lice;
                return Ok(_mapper.Map<UgovorDto>(ugovor));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska");
            }
        }

        /// <summary>
        /// Kreiranje novog ugovora
        /// </summary>
        /// <param name="ugovor">Model ugovora</param>
        /// <param name="key"> ključ sa kojim se proverava autorizacija(key vrednost: Bearer Jovana123)</param>
        /// <returns>Potvrdu o kreiranju ugovora</returns>
        /// <response code = "201">Vraća kreirani ugovor</response>
        /// <response code="401">Lice koje želi da izvrši kreiranje ugovora nije autorizovani korisnik</response>
        /// <response code = "500">Došlo je do greške na serveru prilikom kreiranja ugovora</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Consumes("application/json")]
        [HttpPost]
        public ActionResult<UgovorDto> Create([FromBody] UgovorCreateDto ugovor, [FromHeader] string key)
        {
            if (!_authHelper.Authorize(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Korisnik nije autorizovan!");
            }
            try
            {
                var ugovorEntity = _mapper.Map<Ugovor>(ugovor);
                var confirmation = _ugovorRepository.Create(ugovorEntity);
                _ugovorRepository.SaveChanges();
                string location = _linkGenerator.GetPathByAction("GetById", "Ugovor", new { ugovorId = confirmation.UgovorId });
                return Created(location, _mapper.Map<UgovorConfirmationDto>(confirmation));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska");
            }
        }

        /// <summary>
        /// Ažuriranje ugovora
        /// </summary>
        /// <param name="ugovor">Model ugovora</param>
        /// <param name="key"> ključ sa kojim se proverava autorizacija(key vrednost: Bearer Jovana123)</param>
        /// <returns>Potvrdu o izmenama u ugovoru</returns>
        /// <response code="200">Vraća ažurirani ugovor</response>
        /// <response code="401">Lice koje želi da izvrši ažuriranje nije autorizovani korisnik</response>
        /// <response code="404">Nije pronađen ugovor za ažuriranje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom ažuriranja ugovora</response>

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPut]
        public ActionResult<UgovorConfirmationDto> Update(UgovorUpdateDto ugovor, [FromHeader] string key)
        {
            if (!_authHelper.Authorize(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Korisnik nije autorizovan!");
            }
            try
            {
                var oldUgovor = _ugovorRepository.GetById(ugovor.UgovorId);
                if (oldUgovor == null)
                {
                    return NotFound();
                }
                Ugovor ugovorEntity = _mapper.Map<Ugovor>(ugovor);

                _mapper.Map(ugovorEntity, oldUgovor);
                _ugovorRepository.SaveChanges();
                return Ok(_mapper.Map<UgovorConfirmationDto>(oldUgovor));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska");
            }
        }

        /// <summary>
        /// Brisanje ugovora
        /// </summary>
        /// <param name="ugovorId">Id ugovora</param>
        ///  <param name="key"> ključ sa kojim se proverava autorizacija(key vrednost: Bearer Jovana123)</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Ugovor uspešno obrisan</response>
        /// <response code="401">Lice koje želi da izvrši brisanje nije autorizovani korisnik</response>
        /// <response code="404">Nije pronađen ugovor za brisanje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom brisanja ugovora</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpDelete("{ugovorId}")]
        public IActionResult Delete(Guid ugovorId, [FromHeader] string key)
        {
            if (!_authHelper.Authorize(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Korisnik nije autorizovan!");
            }
            try
            {
                var ugovor = _ugovorRepository.GetById(ugovorId);
                if (ugovor == null)
                {
                    return NotFound();
                }

                _ugovorRepository.Delete(ugovorId);
                _ugovorRepository.SaveChanges();
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska");
            }
        }

        /// <summary>
        /// Vraća opcije za rad ugovorima
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetUgovorOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
