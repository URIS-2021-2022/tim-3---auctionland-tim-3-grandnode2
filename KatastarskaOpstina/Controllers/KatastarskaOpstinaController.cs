using AutoMapper;
using KatastarskaOpstina.Auth;
using KatastarskaOpstina.Data;
using KatastarskaOpstina.Entities;
using KatastarskaOpstina.Logger;
using KatastarskaOpstina.Models;
using KatastarskaOpstina.ServiceCalls;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KatastarskaOpstina.Controllers
{
    [ApiController]
    [Route("api/katastarskeOpstine")]
    [Produces("application/json", "application/xml")]
    public class KatastarskaOpstinaController : ControllerBase
    {
        private readonly IKatastarskaOpstinaRepository katastarskaOpstinaRepository;
        private readonly IParcelaService parcelaService;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly IAuthService authService;
        private readonly LoggerService logger;

        public KatastarskaOpstinaController(IKatastarskaOpstinaRepository katastarskaOpstinaRepository,IParcelaService parcelaService ,LinkGenerator linkGenerator, IMapper mapper, IAuthService authService)
        {
            this.katastarskaOpstinaRepository = katastarskaOpstinaRepository;
            this.linkGenerator = linkGenerator;
            this.parcelaService = parcelaService;
            this.mapper = mapper;
            this.authService = authService;
            logger = new LoggerService();
        }

        /// <summary>
        /// Vraca sve katastarske opstine
        /// </summary>
        /// <returns>Lista katastarskih opstina</returns>
        /// <response code= "200">Vraca listu katastarskih opstina</response>
        /// <response code= "204">Ne postoji nijedna katastarska opstina</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        [HttpHead]
        [AllowAnonymous]
        public ActionResult<List<KatastarskaOpstinaModelDto>> GetKatastarskaOpstinas() 
        {
            #pragma warning disable CS4014
            logger.PostLogger("Pristup svim katastarskim opstinama." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
            #pragma warning restore CS4014
            var katastarskaOpstinas = katastarskaOpstinaRepository.GetKatastarskaOpstinas();
            if (katastarskaOpstinas == null || katastarskaOpstinas.Count == 0)
            {
                return NoContent();
            }
            foreach(var ko in katastarskaOpstinas)
            {
                ko.Parcele = parcelaService.GetParceleByKatastarskaOpstinaID(ko.KatastarskaOpstinaID).Result;
            }
            return Ok(mapper.Map<List<KatastarskaOpstinaModelDto>>(katastarskaOpstinas));
        }

        /// <summary>
        /// Vraca katastarsku opstinu po ID-u
        /// </summary>
        /// <param name="katastarskaOpstinaID">ID katastarske opstine</param>
        /// <returns>Odgovarajuca katastarska opstina</returns>
        /// <response code= "200">Vraca trazenu katastarsku opstina</response>
        /// <response code= "204">Nije pronadjena trazena katastarska opstina</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{katastarskaOpstinaID}")]
        [AllowAnonymous]
        public ActionResult<KatastarskaOpstinaModelDto> GetKatastarskaOpstina(Guid katastarskaOpstinaID)
        {
            #pragma warning disable CS4014
            logger.PostLogger("Pristup katastarskoj opstini putem id-a." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
            #pragma warning restore CS4014
            var katastarskaOpstinas = katastarskaOpstinaRepository.GetKatastarskaOpstinaById(katastarskaOpstinaID);

            if (katastarskaOpstinas == null)
            {
                return NotFound();
            }
            katastarskaOpstinas.Parcele = parcelaService.GetParceleByKatastarskaOpstinaID(katastarskaOpstinaID).Result;
            return Ok(mapper.Map<KatastarskaOpstinaModelDto>(katastarskaOpstinas));

        }

        /// <summary>
        /// Kreiranje nove katastarske opstine
        /// </summary>
        /// <param name="katastarskaOpstina">Model katastarske opstine </param>
        /// <param name="key">Kljuc sa kojim se proverava autorizacija(key vrednost: MajaCetic)</param>
        /// <returns>Potvrda o kreiranju katastarske opstine</returns>
        /// <response code= "201">Vraca kreiranu katastarsku opstina</response>
        /// <response code= "401">Lice koje zeli da izvrsi kreiranje katastarske opstine nije autorizovani korisnik</response>
        /// <response code= "500">Doslo je do greske na serveru prilikom kreiranja katastarske opstine</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Consumes("application/json")]
        [HttpPost]
        public ActionResult<KatastarskaOpstinaCreationDto> CreateKatastarskaOpstina([FromBody] KatastarskaOpstinaCreationDto katastarskaOpstina, [FromHeader] string key)
        {
            #pragma warning disable CS4014
            logger.PostLogger("Kreiranje nove katastarske opstine." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
            #pragma warning restore CS4014
            if (!authService.Authorize(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Korisnik nije autorizovan!");
            }
            try
            {
                

                var katastarskaOpstinaE = mapper.Map<KatastarskaOpstinaE>(katastarskaOpstina);
                var confirmation = katastarskaOpstinaRepository.CreateKatastarskaOpstina(katastarskaOpstinaE);
                katastarskaOpstinaRepository.SaveChanges();
                string location = linkGenerator.GetPathByAction("GetKatastarskaOpstina", "katastarskaOpstina", new { katastarskaOpstinaID = confirmation.KatastarskaOpstinaID });
                return Created(location, mapper.Map<KatastarskaOpstinaConfirmationDto>(confirmation));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create error");
            }

        }

        /// <summary>
        /// Brisanje ugovora
        /// </summary>
        /// <param name="katastarskaOpstinaID">ID katastarske opstine</param>
        /// <param name="key">Kljuc sa kojim se proverava autorizacija(key vrednost: MajaCetic)</param>
        /// <returns>Status 204(NoContent)</returns>
        /// <response code= "204">Katastarska opstina uspesno obrisana</response>
        /// <response code= "401">Lice koje zeli da izvrsi brisanje katastarske opstine nije autorizovani korisnik</response>
        /// <response code= "404">Nije pronadjena katastarska opstina za brisanje</response>
        /// <response code= "500">Doslo je do greske na serveru prilikom brisanja katastarske opstine</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpDelete("{katastarskaOpstinaID}")]
        public IActionResult DeleteKatastarskaOpstina(Guid katastarskaOpstinaID, [FromHeader] string key)
        {
            #pragma warning disable CS4014
            logger.PostLogger("Brisanje postojece katastarske opstine." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
            #pragma warning restore CS4014
            if (!authService.Authorize(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Korisnik nije autorizovan!");
            }
            try
            {
                var katastarskaOpstina = katastarskaOpstinaRepository.GetKatastarskaOpstinaById(katastarskaOpstinaID);

                if (katastarskaOpstina == null)
                {
                    return NotFound();
                }

                katastarskaOpstinaRepository.DeleteKatastarskaOpstina(katastarskaOpstinaID);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }
        /// <summary>
        /// Azuriranje ugovora
        /// </summary>
        /// <param name="katastarskaOpstina">Model katastarske opstine</param>
        /// <param name="key">Kljuc sa kojim se proverava autorizacija(key vrednost: MajaCetic)</param>
        /// <returns>Potvrda u izmenama u katastarskoj opstini</returns>
        /// <response code= "200">Katastarska opstina azurirana</response>
        /// <response code= "401">Lice koje zeli da izvrsi brisanje katastarske opstine nije autorizovani korisnik</response>
        /// <response code= "404">Nije pronadjena katastarska opstina za brisanje</response>
        /// <response code= "500">Doslo je do greske na serveru prilikom brisanja katastarske opstine</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPut]
        public ActionResult<KatastarskaOpstinaConfirmationDto> UpdateTipZalbe(KatastarskaOpstinaModelDto katastarskaOpstina, [FromHeader] string key)
        {
            #pragma warning disable CS4014
            logger.PostLogger("Modifikacija postojece katastarske opstine." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
            #pragma warning restore CS4014
            if (!authService.Authorize(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Korisnik nije autorizovan!");
            }
            try
            {
                

                if (katastarskaOpstinaRepository.GetKatastarskaOpstinaById(katastarskaOpstina.KatastarskaOpstinaID) == null)
                {
                    return NotFound();
                }
                KatastarskaOpstinaE katastarskaOpstinaE = mapper.Map<KatastarskaOpstinaE>(katastarskaOpstina);
                mapper.Map(katastarskaOpstinaE, katastarskaOpstinaRepository.GetKatastarskaOpstinaById(katastarskaOpstina.KatastarskaOpstinaID));
                katastarskaOpstinaRepository.SaveChanges();


                return Ok(mapper.Map<KatastarskaOpstinaModelDto>(katastarskaOpstinaRepository.GetKatastarskaOpstinaById(katastarskaOpstina.KatastarskaOpstinaID)));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        /// <summary>
        /// Vraca opcije za rad sa katastarskim opstinama
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetKatastarskaOpstinasOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }

    }
}
