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
    [Route("api/kvalitetiZemljista")]
    [Produces("application/json", "application/xml")]
    public class KvalitetZemljistaController : ControllerBase
    {
        private readonly IKvalitetZemljistaRepository _kvalitetZemljistaRepository;
        private readonly LinkGenerator _linkGenerator;
        private readonly IMapper _mapper;
        private readonly IAuthHelper _authHelper;

        public KvalitetZemljistaController(IKvalitetZemljistaRepository kvalitetZemljistaRepository, LinkGenerator linkGenerator, IMapper mapper, IAuthHelper authHelper)
        {
            _kvalitetZemljistaRepository = kvalitetZemljistaRepository;
            _linkGenerator = linkGenerator;
            _mapper = mapper;
            _authHelper = authHelper;
        }

        /// <summary>
        /// Vraca sve kvalitete zemljista
        /// </summary>
        /// <returns>Lista kvaliteta zemljista</returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        [AllowAnonymous]
        public ActionResult<List<KvalitetZemljistaDto>> GetAll()
        {
            var kvalitetiZemljista = _kvalitetZemljistaRepository.GetAll();
            if (kvalitetiZemljista == null || kvalitetiZemljista.Count == 0)
            {
                return NoContent();
            }
            return Ok(_mapper.Map<List<KvalitetZemljistaDto>>(kvalitetiZemljista));
        }

        /// <summary>
        /// Vraca kvalitet zemljista sa unetim Id
        /// </summary>
        /// <param name="kvalitetZemljistaId">Id kvaliteta zemljista</param>
        /// <returns>Odgovarajuci kvalitet zemljista</returns>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{kvalitetZemljistaId}")]
        [AllowAnonymous]
        public ActionResult<KvalitetZemljistaDto> GetById(Guid kvalitetZemljistaId)
        {
            var kvalitetZemljista = _kvalitetZemljistaRepository.GetById(kvalitetZemljistaId);
            if (kvalitetZemljista == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<KvalitetZemljistaDto>(kvalitetZemljista));
        }

        /// <summary>
        /// Kreiranje novog kvaliteta zemljista
        /// </summary>
        /// <param name="kvalitetZemljista">Model kvaliteta zemljista</param>
        /// <returns>Potvrdu o kreiranju kvaliteta zemljista</returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Consumes("application/json")]
        [HttpPost]
        public ActionResult<KvalitetZemljistaDto> Create([FromBody] KvalitetZemljistaCreateDto kvalitetZemljista, [FromHeader] string key)
        {
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
        /// Azuriranje kvaliteta zemljista
        /// </summary>
        /// <param name="kvalitetZemljista">Model kvaliteta zemljista</param>
        /// <returns>Potvrda o izmenama u kvalitetu zemljista</returns>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPut]
        public ActionResult<KvalitetZemljistaConfirmationDto> Update(KvalitetZemljistaUpdateDto kvalitetZemljista, [FromHeader] string key)
        {
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
        /// Brisanje kvaliteta zemljista
        /// </summary>
        /// <param name="kvalitetZemljistaId">Id kvaliteta zemljista</param>
        /// <returns>Obrisan kvalitet zemljista</returns>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpDelete("{kvalitetZemljistaId}")]
        public IActionResult Delete(Guid kvalitetZemljistaId, [FromHeader] string key)
        {
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
        /// Vraca opcije za rad kvalitetima zemljista
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetKvalitetZemljistaOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
