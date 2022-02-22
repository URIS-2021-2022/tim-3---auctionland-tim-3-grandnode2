using AutoMapper;
using JavnoNadmetanje.Auth;
using JavnoNadmetanje.Data;
using JavnoNadmetanje.Entities;
using JavnoNadmetanje.Logger;
using JavnoNadmetanje.Models;
using JavnoNadmetanje.Models.UplataService;
using JavnoNadmetanje.ServiceCalls;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Controllers
{
    [ApiController]
    [Route("api/prijaveZaNadmetanje")]
    [Consumes("application/json")]
    [Produces("application/json", "application/xml")]
    public class PrijavaZaNadmetanjeController : ControllerBase
    {
        private readonly IPrijavaZaNadmetanjeRepository prijavaZaNadmetanjeRepository;
        private readonly IUplataService uplataService;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly IAuthService authService;
        private readonly LoggerService logger;

        public PrijavaZaNadmetanjeController(IPrijavaZaNadmetanjeRepository prijavaZaNadmetanjeRepository, IUplataService uplataService, LinkGenerator linkGenerator, IMapper mapper, IAuthService authService)
        {
            this.prijavaZaNadmetanjeRepository = prijavaZaNadmetanjeRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.authService = authService;
            this.uplataService = uplataService;
            logger = new LoggerService();
        }

        /// <summary>
        /// Vraća sve prijave za nadmetanje
        /// </summary>
        /// <returns>Lista prijava za nadmetanje</returns>
        /// <response code = "200">Vraća listu prijava za nadmetanje</response>
        /// <response code = "204">Ne postoji nijedna prijava za nadmetanje</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [AllowAnonymous]
        public ActionResult<List<PrijavaZaNadmetanjeDto>> GetPrijaveZaNadmetanje()
        {
            #pragma warning disable CS4014
            logger.PostLogger("Pristup svim prijavama za nadmetanje." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
            #pragma warning restore CS4014

            List<PrijavaZaNadmetanjeEntity> prijaveZaNadmetanje = prijavaZaNadmetanjeRepository.GetPrijaveZaNadmetanje();

            if (prijaveZaNadmetanje == null || prijaveZaNadmetanje.Count == 0)
            {
                return NoContent();
            }

            foreach(PrijavaZaNadmetanjeEntity p in prijaveZaNadmetanje)
            {
                List<UplataDto> uplate = uplataService.GetUplateByPrijavaZaNadmetanjeId(p.PrijavaZaNadmetanjeId).Result;
                p.Uplate = uplate;
             }

            return Ok(mapper.Map<List<PrijavaZaNadmetanjeDto>>(prijaveZaNadmetanje));
        }

        /// <summary>
        /// Vraća prijavu za nadmetanje po ID-ju
        /// </summary>
        /// <param name="prijavaZaNadmetanjeId">ID prijave za nadmetanje</param>
        /// <returns>Tražena prijava za nadmetanje</returns>
        /// <response code = "200">Vraća traženu prijavu za nadmetanje</response>
        /// <response code = "404">Nije pronađena tražena prijava za nadmetanje</response>
        [HttpGet("{prijavaZaNadmetanjeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public ActionResult<PrijavaZaNadmetanjeDto> GetPrijavaZaNadmetanjeById(Guid prijavaZaNadmetanjeId)
        {
            #pragma warning disable CS4014
            logger.PostLogger("Pristup prijavi za nadmetanje putem ID-ja." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
            #pragma warning restore CS4014

            PrijavaZaNadmetanjeEntity prijavaZaNadmetanje = prijavaZaNadmetanjeRepository.GetPrijavaZaNadmetanjeById(prijavaZaNadmetanjeId);

            if (prijavaZaNadmetanje == null)
            {
                return NotFound();
            }

            List<UplataDto> uplate = uplataService.GetUplateByPrijavaZaNadmetanjeId(prijavaZaNadmetanjeId).Result;
            prijavaZaNadmetanje.Uplate = uplate;

            return Ok(mapper.Map<PrijavaZaNadmetanjeDto>(prijavaZaNadmetanje));
        }

        /// <summary>
        /// Kreira novu prijavu za nadmetanje
        /// </summary>
        /// <param name="prijavaZaNadmetanje"> model prijave za nadmetanje</param>
        /// <param name="key"> ključ sa kojim se proverava autorizacija(key vrednost: Bearer LenkaSubotin)</param>
        /// <returns>Potvrda o kreiranoj prijavi za nadmetanje</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje nove prijave za nadmetanje \
        /// POST /api/prijaveZaNadmetanje \
        /// { \
        /// "DatumPrijave": "2022-02-08", \
        /// "MestoPrijave" : "Subotica", \
        /// "JavnoNadmetanjeId" : "1c7ea607-8ddb-493a-87fa-4bf5893e965b", \
        /// "Uplate" : [] \
        /// } 
        /// </remarks>
        /// <response code = "201">Vraća kreiranu prijavu za nadmetanje</response>
        /// <response code="401">Lice koje želi da izvrši kreiranje prijave za nadmetanje nije autorizovani korisnik</response>
        /// <response code = "500">Došlo je do greške na serveru prilikom kreiranja prijave za nadmetanje</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<PrijavaZaNadmetanjeDto> CreatePrijavaZaNadmetanje([FromBody] PrijavaZaNadmetanjeCreateDto prijavaZaNadmetanje, [FromHeader] string key)
        {
            #pragma warning disable CS4014
            logger.PostLogger("Kreiranje nove prijave za nadmetanje." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
            #pragma warning restore CS4014

            if (!authService.Authorize(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Korisnik nije autorizovan!");
            }

            try
            {
                PrijavaZaNadmetanjeEntity prijavaNadmetanje = mapper.Map<PrijavaZaNadmetanjeEntity>(prijavaZaNadmetanje);
                PrijavaZaNadmetanjeEntity prijavaNadmetanje1 = prijavaZaNadmetanjeRepository.CreatePrijavaZaNadmetanje(prijavaNadmetanje);
                prijavaZaNadmetanjeRepository.SaveChanges();
                string location = linkGenerator.GetPathByAction("GetPrijaveZaNadmetanje", "PrijavaZaNadmetanje", new { prijavaZaNadmetanjeId = prijavaNadmetanje.PrijavaZaNadmetanjeId });
                return Created(location, mapper.Map<PrijavaZaNadmetanjeEntity>(prijavaNadmetanje1));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska prilikom kreiranja prijave za nadmetanje!");
            }
        }

        /// <summary>
        /// Briše prijavu za nadmetanje na osnovu ID-ja
        /// </summary>
        /// <param name="prijavaZaNadmetanjeId">ID prijave za nadmetanje</param>
        /// <param name="key"> ključ sa kojim se proverava autorizacija(key vrednost: Bearer LenkaSubotin)</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Prijava za nadmetanje uspešno obrisana</response>
        /// <response code="401">Lice koje želi da izvrši brisanje nije autorizovani korisnik</response>
        /// <response code="404">Nije pronađena prijava za nadmetanje za brisanje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom brisanja prijave za nadmetanje</response>
        [HttpDelete("{prijavaZaNadmetanjeId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult DeletePrijavaZaNadmetanje(Guid prijavaZaNadmetanjeId, [FromHeader] string key)
        {
            #pragma warning disable CS4014
            logger.PostLogger("Brisanje postojeće prijave za nadmetanje." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
            #pragma warning restore CS4014

            if (!authService.Authorize(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Korisnik nije autorizovan!");
            }

            try
            {
                PrijavaZaNadmetanjeEntity prijavaNadmetanje = prijavaZaNadmetanjeRepository.GetPrijavaZaNadmetanjeById(prijavaZaNadmetanjeId);
                if (prijavaNadmetanje == null)
                {
                    return NotFound();
                }

                prijavaZaNadmetanjeRepository.DeletePrijavaZaNadmetanje(prijavaZaNadmetanjeId);
                prijavaZaNadmetanjeRepository.SaveChanges();
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska prilikom brisanja prijave za nadmetanje!");
            }
        }

        /// <summary>
        /// Ažurira jedna prijava za nadmetanje
        /// </summary>
        /// <param name="prijavaZaNadmetanje">Model prijave za nadmetanje koji se ažurira</param>
        /// <param name="key"> ključ sa kojim se proverava autorizacija(key vrednost: Bearer LenkaSubotin)</param>
        /// <returns>Potvrda o ažuriranoj prijavi za nadmetanje</returns>
        /// <response code="200">Vraća ažuriranu prijavu za nadmetanje</response>
        /// <response code="401">Lice koje želi da izvrši ažuriranje nije autorizovani korisnik</response>
        /// <response code="404">Nije pronađena prijava za nadmetanje za ažuriranje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom ažuriranja prijave za nadmetanje</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<PrijavaZaNadmetanjeDto> UpdatePrijavaZaNadmetanje(PrijavaZaNadmetanjeEntity prijavaZaNadmetanje, [FromHeader] string key)
        {

            #pragma warning disable CS4014
            logger.PostLogger("Modifikacija postojeće prijave za nadmetanje." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
            #pragma warning restore CS4014

            if (!authService.Authorize(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Korisnik nije autorizovan!");
            }

            try
            {
                var oldPrijavaZaNadmetanje = prijavaZaNadmetanjeRepository.GetPrijavaZaNadmetanjeById(prijavaZaNadmetanje.PrijavaZaNadmetanjeId);

                if (oldPrijavaZaNadmetanje == null)
                {
                    return NotFound();
                }

                PrijavaZaNadmetanjeEntity prijavaZaNadmetanje1 = mapper.Map<PrijavaZaNadmetanjeEntity>(prijavaZaNadmetanje);
                mapper.Map(prijavaZaNadmetanje1, oldPrijavaZaNadmetanje);
                prijavaZaNadmetanjeRepository.SaveChanges();

                return Ok(mapper.Map<PrijavaZaNadmetanjeDto>(prijavaZaNadmetanje1));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska prilikom azuriranja prijave za nadmetanje!");
            }
        }

        /// <summary>
        /// Vraća informacije o opcijama koje je moguće izvršiti za sve prijave za nadmetanje
        /// </summary>
        /// <response code="200">Vraća informacije o opcijama koje je moguće izvršiti</response>
        [HttpOptions]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [AllowAnonymous]
        public IActionResult GetPrijavaZaNadmetanjeOptions()
        {
            #pragma warning disable CS4014
            logger.PostLogger("Pristup opcijama za prijavu za nadmetanje." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
            #pragma warning restore CS4014

            Response.Headers.Add("Allow", "GET, HEAD, POST, PUT, DELETE");
            return Ok();
        }
    }
}
