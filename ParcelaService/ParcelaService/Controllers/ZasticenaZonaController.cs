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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelaService.Controllers
{
    [ApiController]
    [Route("api/zasticeneZone")]
    [Produces("application/json", "application/xml")]
    public class ZasticenaZonaController : ControllerBase
    {
        private readonly IZasticenaZonaRepository _zasticenaZonaRepository;
        private readonly LinkGenerator _linkGenerator;
        private readonly IMapper _mapper;
        private readonly IAuthHelper _authHelper;

        public ZasticenaZonaController(IZasticenaZonaRepository zasticenaZonaRepository, LinkGenerator linkGenerator, IMapper mapper, IAuthHelper authHelper)
        {
            _zasticenaZonaRepository = zasticenaZonaRepository;
            _linkGenerator = linkGenerator;
            _mapper = mapper;
            _authHelper = authHelper;
        }

        /// <summary>
        /// Vraca sve zaštićene zone
        /// </summary>
        /// <returns>Lista zaštićenih zona</returns>
        /// <response code = "200">Vraća listu zaštićenih zona</response>
        /// <response code = "204">Ne postoji nijedna zaštićena zona</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        [AllowAnonymous]
        public ActionResult<List<ZasticenaZonaDto>> GetAll()
        {
            var zasticeneZone = _zasticenaZonaRepository.GetAll();
            if (zasticeneZone == null || zasticeneZone.Count == 0)
            {
                return NoContent();
            }
            return Ok(_mapper.Map<List<ZasticenaZonaDto>>(zasticeneZone));
        }

        /// <summary>
        /// Vraca zaštićenu zonu po ID-ju
        /// </summary>
        /// <param name="zasticenaZonaId">ID zaštićene zone</param>
        /// <returns>Odgovarajuća zaštićena zona</returns>
        /// <response code = "200">Vraća traženu zaštićenu zonu</response>
        /// <response code = "404">Nije pronađena tražena zaštićena zona</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{zasticenaZonaId}")]
        [AllowAnonymous]
        public ActionResult<ZasticenaZonaDto> GetById(Guid zasticenaZonaId)
        {
            var zasticenaZona = _zasticenaZonaRepository.GetById(zasticenaZonaId);
            if (zasticenaZona == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ZasticenaZonaDto>(zasticenaZona));
        }

        /// <summary>
        /// Kreiranje nove zaštićene zone
        /// </summary>
        /// <param name="zasticenaZona">Model zaštićene zone</param>
        /// <param name="key"> ključ sa kojim se proverava autorizacija(key vrednost: Bearer Jovana123)</param>
        /// <returns>Potvrdu o kreiranju zaštićene zone/returns>
        /// <response code = "201">Vraća kreiranu zaštićenu zonu</response>
        /// <response code="401">Lice koje želi da izvrši kreiranje zaštićene zone nije autorizovani korisnik</response>
        /// <response code = "500">Došlo je do greške na serveru prilikom kreiranja zaštićene zone</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Consumes("application/json")]
        [HttpPost]
        public ActionResult<ZasticenaZonaDto> Create([FromBody] ZasticenaZonaCreateDto zasticenaZona, [FromHeader] string key)
        {
            if (!_authHelper.Authorize(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Korisnik nije autorizovan!");
            }
            try
            {
                var zasticenaZonaEntity = _mapper.Map<ZasticenaZona>(zasticenaZona);
                var confirmation = _zasticenaZonaRepository.Create(zasticenaZonaEntity);
                _zasticenaZonaRepository.SaveChanges();
                string location = _linkGenerator.GetPathByAction("GetById", "ZasticenaZona", new { zasticenaZonaId = confirmation.ZasticenaZonaId });
                return Created(location, _mapper.Map<ZasticenaZonaConfirmationDto>(confirmation));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska");
            }
        }

        /// <summary>
        /// Ažuriranje zaštićene zone
        /// </summary>
        /// <param name="zasticenaZona">Model zaštićene zone</param>
        /// <param name="key"> ključ sa kojim se proverava autorizacija(key vrednost: Bearer Jovana123)</param>
        /// <returns>Potvrdu o izmenama o zaštićenoj zoni</returns>
        /// <response code="200">Vraća ažuriranu zaštićenu zonu</response>
        /// <response code="401">Lice koje želi da izvrši ažuriranje nije autorizovani korisnik</response>
        /// <response code="404">Nije pronađena zaštićena zona za ažuriranje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom ažuriranja zaštićene zone</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPut]
        public ActionResult<ZasticenaZonaConfirmationDto> Update(ZasticenaZonaUpdateDto zasticenaZona, [FromHeader] string key)
        {
            if (!_authHelper.Authorize(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Korisnik nije autorizovan!");
            }
            try
            {
                var oldZasticenaZona = _zasticenaZonaRepository.GetById(zasticenaZona.ZasticenaZonaId);
                if (oldZasticenaZona == null)
                {
                    return NotFound();
                }
                ZasticenaZona zasticenaZonaEntity = _mapper.Map<ZasticenaZona>(zasticenaZona);

                _mapper.Map(zasticenaZonaEntity, oldZasticenaZona);
                _zasticenaZonaRepository.SaveChanges();
                return Ok(_mapper.Map<ZasticenaZonaDto>(oldZasticenaZona));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska");
            }
        }

        /// <summary>
        /// Brisanje zaštićene zone
        /// </summary>
        /// <param name="zasticenaZonaId">ID zaštićene zone</param>
        /// <param name="key"> ključ sa kojim se proverava autorizacija(key vrednost: Bearer Jovana123)</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Zaštićena zona uspešno obrisana</response>
        /// <response code="401">Lice koje želi da izvrši brisanje nije autorizovani korisnik</response>
        /// <response code="404">Nije pronađena zaštićena zona za brisanje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom brisanja zaštićene zone</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpDelete("{zasticenaZonaId}")]
        public IActionResult Delete(Guid zasticenaZonaId, [FromHeader] string key)
        {
            if (!_authHelper.Authorize(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Korisnik nije autorizovan!");
            }
            try
            {
                var zasticenaZona = _zasticenaZonaRepository.GetById(zasticenaZonaId);
                if (zasticenaZona == null)
                {
                    return NotFound();
                }

                _zasticenaZonaRepository.Delete(zasticenaZonaId);
                _zasticenaZonaRepository.SaveChanges();
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska");
            }
        }

        /// <summary>
        /// Vraća opcije za rad sa zaštićenim zonama
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetZasticenaZonaOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
