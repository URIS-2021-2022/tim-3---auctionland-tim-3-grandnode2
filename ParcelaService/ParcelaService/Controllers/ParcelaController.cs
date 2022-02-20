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
    [Route("api/parcele")]
    [Produces("application/json", "application/xml")]
    public class ParcelaController : ControllerBase
    {
        private readonly IParcelaRepository _parcelaRepository;
        private readonly LinkGenerator _linkGenerator;
        private readonly IMapper _mapper;
        private readonly IAuthHelper _authHelper;

        public ParcelaController(IParcelaRepository parcelaRepository, LinkGenerator linkGenerator, IMapper mapper, IAuthHelper authHelper)
        {
            _parcelaRepository = parcelaRepository;
            _linkGenerator = linkGenerator;
            _mapper = mapper;
            _authHelper = authHelper;
        }

        /// <summary>
        /// Vraca sve parcele ili opciono sve parcele koje pripadaju nekoj katastarskoj opstini
        /// </summary>
        /// <param name="katastarskaOpstinaId">Id katastarske opstine</param>
        /// <returns>Lista parcela</returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        [AllowAnonymous]
        public ActionResult<List<ParcelaDto>> GetAll(Guid ?katastarskaOpstinaId)
        {
            var parcele = _parcelaRepository.GetAll(katastarskaOpstinaId);
            if (parcele == null || parcele.Count == 0)
            {
                return NoContent();
            }
            return Ok(_mapper.Map<List<ParcelaDto>>(parcele));
        }

        /// <summary>
        /// Vraca sve delove parcele ciji je Id prosledjen
        /// </summary>
        /// <param name="parcelaId">Id parcele</param>
        /// <returns>Lista delova parcele</returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("DeloviParcele/{parcelaId}")]
        [AllowAnonymous]
        public ActionResult<List<DeoParceleDto>> GetDeloveParcele(Guid parcelaId)
        {
            var deloviParcele = _parcelaRepository.GetDeloveParcele(parcelaId);
            if (deloviParcele == null || deloviParcele.Count == 0)
            {
                return NoContent();
            }
            return Ok(_mapper.Map<List<DeoParceleDto>>(deloviParcele));
        }

        /// <summary>
        /// Vraca parcelu sa unetim Id
        /// </summary>
        /// <param name="parcelaId">Id parcele</param>
        /// <returns>Odgovarajuca parcela</returns>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{parcelaId}")]
        [AllowAnonymous]
        public ActionResult<ParcelaDto> GetById(Guid parcelaId)
        {
            var parcela = _parcelaRepository.GetById(parcelaId);
            if (parcela == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ParcelaDto>(parcela));
        }

        /// <summary>
        /// Kreiranje nove parcele
        /// </summary>
        /// <param name="parcela">Model parcele</param>
        /// <returns>Potvrda o kreiranju parcele</returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes("application/json")]
        [HttpPost]
        public ActionResult<ParcelaDto> Create([FromBody] ParcelaCreateDto parcela, [FromHeader] string key)
        {
            if (!_authHelper.Authorize(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Korisnik nije autorizovan!");
            }
            try
            {
                var parcelaEntity = _mapper.Map<Parcela>(parcela);
                var confirmation = _parcelaRepository.Create(parcelaEntity);
                _parcelaRepository.SaveChanges();
                string location = _linkGenerator.GetPathByAction("GetById", "Parcela", new { parcelaId = confirmation.ParcelaId });
                return Created(location, _mapper.Map<ParcelaConfirmationDto>(confirmation));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska");
            }
        }

        /// <summary>
        /// Azuriranje parcele
        /// </summary>
        /// <param name="parcela">Model parcele</param>
        /// <returns>Potvrda o izmenama u parceli</returns>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut]
        public ActionResult<ParcelaConfirmationDto> Update(ParcelaUpdateDto parcela, [FromHeader] string key)
        {
            if (!_authHelper.Authorize(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Korisnik nije autorizovan!");
            }
            try
            {
                var oldParcela = _parcelaRepository.GetById(parcela.ParcelaId);
                if (oldParcela == null)
                {
                    return NotFound();
                }
                Parcela parcelaEntity = _mapper.Map<Parcela>(parcela);

                _mapper.Map(parcelaEntity, oldParcela);
                _parcelaRepository.SaveChanges();
                return Ok(_mapper.Map<ParcelaDto>(oldParcela));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska");
            }
        }

        /// <summary>
        /// Brisanje parcele
        /// </summary>
        /// <param name="parcelaId">Id parcele</param>
        /// <returns>Obrisana parcela</returns>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{parcelaId}")]
        public IActionResult Delete(Guid parcelaId, [FromHeader] string key)
        {
            if (!_authHelper.Authorize(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Korisnik nije autorizovan!");
            }
            try
            {
                var parcela = _parcelaRepository.GetById(parcelaId);
                if (parcela == null)
                {
                    return NotFound();
                }

                _parcelaRepository.Delete(parcelaId);
                _parcelaRepository.SaveChanges();
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska");
            }
        }

        /// <summary>
        /// Vraca opcije za rad sa parcelama
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetParcelaOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
