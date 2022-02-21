using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ParcelaService.Auth;
using ParcelaService.Data;
using ParcelaService.Entities;
using ParcelaService.Entities.Confirmations;
using ParcelaService.Models;
using ParcelaService.Models.CreateDto;
using ParcelaService.Models.UpdateDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelaService.Controllers
{
    [ApiController]
    [Route("api/deloviParcele")]
    [Produces("application/json", "application/xml")]
    public class DeoParceleController : ControllerBase
    {
        private readonly IDeoParceleRepository _deoParceleRepository;
        private readonly LinkGenerator _linkGenerator;
        private readonly IMapper _mapper;
        private readonly IAuthHelper _authHelper;

        public DeoParceleController(IDeoParceleRepository deoParceleRepository, LinkGenerator linkGenerator, IMapper mapper, IAuthHelper authHelper)
        {
            _deoParceleRepository = deoParceleRepository;
            _linkGenerator = linkGenerator;
            _mapper = mapper;
            _authHelper = authHelper;
        }

        /// <summary>
        /// Vraca sve delove parcele ili opciono sve delove parcele sa unetim Id
        /// </summary>
        /// <param name="parcelaId">Id parcele</param>
        /// <returns>Lista delova parcele</returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        [AllowAnonymous]
        public ActionResult<List<DeoParceleDto>> GetAll(Guid ?parcelaId)
        {
            var deloviParcele = _deoParceleRepository.GetAll(parcelaId);
            if (deloviParcele == null || deloviParcele.Count == 0)
            {
                return NoContent();
            }
            return Ok(_mapper.Map<List<DeoParceleDto>>(deloviParcele));
        }

        /// <summary>
        /// Vraca deo parcele sa unetim Id
        /// </summary>
        /// <param name="deoParceleId">Id dela parcele</param>
        /// <returns>Odgovarajuci deo parcele</returns>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{deoParceleId}")]
        [AllowAnonymous]
        public ActionResult<DeoParceleDto> GetById(Guid deoParceleId)
        {
            var deoParcele = _deoParceleRepository.GetById(deoParceleId);
            if (deoParcele == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<DeoParceleDto>(deoParcele));
        }

        /// <summary>
        /// Kreiranje novog dela parcele
        /// </summary>
        /// <param name="deoParcele">Model dela parcele</param>
        /// <returns>Potvrda o kreiranju dela parcele</returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Consumes("application/json")]
        [HttpPost]
        public ActionResult<DeoParceleDto> Create([FromBody] DeoParceleCreateDto deoParcele, [FromHeader] string key)
        {
            if (!_authHelper.Authorize(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Korisnik nije autorizovan!");
            }
            try
            {
                var deoParceleEntity = _mapper.Map<DeoParcele>(deoParcele);
                var confirmation = _deoParceleRepository.Create(deoParceleEntity);
                _deoParceleRepository.SaveChanges();
                string location = _linkGenerator.GetPathByAction("GetById", "DeoParcele", new { deoParceleId = confirmation.DeoParceleId });
                return Created(location, _mapper.Map<DeoParceleConfirmationDto>(confirmation));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska");
            }
        }

        /// <summary>
        /// Azuriranje dela parcele
        /// </summary>
        /// <param name="deoParcele">Model dela parcele</param>
        /// <returns>Potvrda o izmenama u delu parcele</returns>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPut]
        public ActionResult<DeoParceleConfirmationDto> Update(DeoParceleUpdateDto deoParcele, [FromHeader] string key)
        {
            if (!_authHelper.Authorize(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Korisnik nije autorizovan!");
            }
            try
            {
                var oldDeoParcele = _deoParceleRepository.GetById(deoParcele.DeoParceleId);
                if (oldDeoParcele == null)
                {
                    return NotFound();
                }
                DeoParcele deoParceleEntity = _mapper.Map<DeoParcele>(deoParcele);

                _mapper.Map(deoParceleEntity, oldDeoParcele);
                _deoParceleRepository.SaveChanges();
                return Ok(_mapper.Map<DeoParceleDto>(oldDeoParcele));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska");
            }
        }

        /// <summary>
        /// Brisanje dela parcele
        /// </summary>
        /// <param name="deoParceleId">Id dela parcele</param>
        /// <returns>Obrisan deo parcele</returns>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpDelete("{deoParceleId}")]
        public IActionResult Delete(Guid deoParceleId, [FromHeader] string key)
        {
            if (!_authHelper.Authorize(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Korisnik nije autorizovan!");
            }
            try
            {
                var deoParcele = _deoParceleRepository.GetById(deoParceleId);
                if (deoParcele == null)
                {
                    return NotFound();
                }

                _deoParceleRepository.Delete(deoParceleId);
                _deoParceleRepository.SaveChanges();
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska");
            }
        }

        /// <summary>
        /// Vraca opcije za rad delovima parcele
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetDeoParceleOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
