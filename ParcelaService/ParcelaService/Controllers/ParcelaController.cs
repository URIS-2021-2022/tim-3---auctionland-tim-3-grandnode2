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
    [Route("api/parcele")]
    [Produces("application/json", "application/xml")]
    public class ParcelaController : ControllerBase
    {
        private readonly IParcelaRepository _parcelaRepository;
        private readonly LinkGenerator _linkGenerator;
        private readonly IMapper _mapper;
        private readonly IAuthHelper _authHelper;
        private readonly LoggerService _logger;

        public ParcelaController(IParcelaRepository parcelaRepository, LinkGenerator linkGenerator, IMapper mapper, IAuthHelper authHelper)
        {
            _parcelaRepository = parcelaRepository;
            _linkGenerator = linkGenerator;
            _mapper = mapper;
            _authHelper = authHelper;
            _logger = new LoggerService();
        }

        /// <summary>
        /// Vraća sve parcele ili opciono sve parcele koje pripadaju nekoj katastarskoj opštini
        /// </summary>
        /// <param name="katastarskaOpstinaId">ID katastarske opštine</param>
        /// <returns>Lista parcela</returns>
        /// <response code = "200">Vraća listu parcela</response>
        /// <response code = "204">Ne postoji nijedna parcela</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        [HttpHead]
        [AllowAnonymous]
        public ActionResult<List<ParcelaDto>> GetAll(Guid ?katastarskaOpstinaId)
        {
            #pragma warning disable CS4014
            _logger.PostLogger("Pristup svim parcelama." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
            #pragma warning restore CS4014
            var parcele = _parcelaRepository.GetAll(katastarskaOpstinaId);
            if (parcele == null || parcele.Count == 0)
            {
                return NoContent();
            }
            return Ok(_mapper.Map<List<ParcelaDto>>(parcele));
        }

        /// <summary>
        /// Vraća sve delove parcele čiji je ID prosleđen
        /// </summary>
        /// <param name="parcelaId">ID parcele</param>
        /// <returns>Lista delova parcele</returns>
        /// <response code = "200">Vraća listu delova parcela</response>
        /// <response code = "204">Ne postoji lista delova parcele</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("DeloviParcele/{parcelaId}")]
        [AllowAnonymous]
        public ActionResult<List<DeoParceleDto>> GetDeloveParcele(Guid parcelaId)
        {
            #pragma warning disable CS4014
            _logger.PostLogger("Pristup delu parcele po Id parcele." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
            #pragma warning restore CS4014
            var deloviParcele = _parcelaRepository.GetDeloveParcele(parcelaId);
            if (deloviParcele == null || deloviParcele.Count == 0)
            {
                return NoContent();
            }
            return Ok(_mapper.Map<List<DeoParceleDto>>(deloviParcele));
        }

        /// <summary>
        /// Vraća parcelu po ID-ju
        /// </summary>
        /// <param name="parcelaId">ID parcele</param>
        /// <returns>Odgovarajuća parcela</returns>
        /// <response code = "200">Vraća traženu parcelu</response>
        /// <response code = "404">Nije pronađena tražena parcela</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{parcelaId}")]
        [AllowAnonymous]
        public ActionResult<ParcelaDto> GetById(Guid parcelaId)
        {
            #pragma warning disable CS4014
            _logger.PostLogger("Pristup parceli." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
            #pragma warning restore CS4014
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
        /// <param name="key"> ključ sa kojim se proverava autorizacija(key vrednost: Bearer Jovana123)</param>
        /// <returns>Potvrda o kreiranju parcele</returns>
        /// <response code = "201">Vraća kreiranu parcele</response>
        /// <response code="401">Lice koje želi da izvrši kreiranje parcele nije autorizovani korisnik</response>
        /// <response code = "500">Došlo je do greške na serveru prilikom kreiranja parcele</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Consumes("application/json")]
        [HttpPost]
        public ActionResult<ParcelaDto> Create([FromBody] ParcelaCreateDto parcela, [FromHeader] string key)
        {
            #pragma warning disable CS4014
            _logger.PostLogger("Kreiranje parcele." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
            #pragma warning restore CS4014
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
        /// Ažuriranje parcele
        /// </summary>
        /// <param name="parcela">Model parcele</param>
        /// <param name="key"> ključ sa kojim se proverava autorizacija(key vrednost: Bearer Jovana123)</param>
        /// <returns>Potvrda o izmenama u parceli</returns>
        /// <response code="200">Vraća ažuriranu parcelu</response>
        /// <response code="401">Lice koje želi da izvrši ažuriranje nije autorizovani korisnik</response>
        /// <response code="404">Nije pronađena parcela za ažuriranje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom ažuriranja parcele</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPut]
        public ActionResult<ParcelaConfirmationDto> Update(ParcelaUpdateDto parcela, [FromHeader] string key)
        {
            #pragma warning disable CS4014
            _logger.PostLogger("Azuriranje parcele." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
            #pragma warning restore CS4014
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
        ///  <param name="key"> ključ sa kojim se proverava autorizacija(key vrednost: Bearer Jovana123)</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Parcela uspešno obrisana</response>
        /// <response code="401">Lice koje želi da izvrši brisanje nije autorizovani korisnik</response>
        /// <response code="404">Nije pronađena parcela za brisanje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom brisanja parcele</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpDelete("{parcelaId}")]
        public IActionResult Delete(Guid parcelaId, [FromHeader] string key)
        {
            #pragma warning disable CS4014
            _logger.PostLogger("Brisanje parcele." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
            #pragma warning restore CS4014
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
        /// Vraća opcije za rad sa parcelama
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetParcelaOptions()
        {
            #pragma warning disable CS4014
            _logger.PostLogger("Pristup opcijama." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
            #pragma warning restore CS4014
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
