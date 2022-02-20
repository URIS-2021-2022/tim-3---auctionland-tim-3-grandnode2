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
        /// Vraca sve zasticene zone
        /// </summary>
        /// <returns>Lista zasticenih zona</returns>
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
        /// Vraca zasticenu zonu sa unetim Id
        /// </summary>
        /// <param name="zasticenaZonaId">Id zasticene zone</param>
        /// <returns>Odgovarajuca zasticena zona</returns>
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
        /// Kreiranje nove zasticene zone
        /// </summary>
        /// <param name="zasticenaZona">Model zasticene zone</param>
        /// <returns>Potvrdu o kreiranju zasticene zone/returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
        /// Azuriranje zasticene zone
        /// </summary>
        /// <param name="zasticenaZona">Model zasticene zone</param>
        /// <returns>Potvrdu o izmenama o zasticenoj zoni</returns>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
        /// Brisanje zasticene zone
        /// </summary>
        /// <param name="zasticenaZonaId">Id zasticene zone</param>
        /// <returns>Obrisana zasticena zona</returns>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
        /// Vraca opcije za rad sa zasticenim zonama
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        public IActionResult GetZasticenaZonaOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
