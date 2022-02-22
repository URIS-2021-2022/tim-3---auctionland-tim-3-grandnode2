using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uplata.Auth;
using Uplata.Data;
using Uplata.Entities;
using Uplata.Models;

namespace Uplata.Controllers
{
    [ApiController]
    [Route("api/uplate")]
    [Consumes("application/json")]
    [Produces("application/json", "application/xml")]
    public class UplataController : ControllerBase
    {
        private readonly IUplataRepository uplataRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly IAuthService authService;

        public UplataController(IUplataRepository uplataRepository, LinkGenerator linkGenerator, IMapper mapper, IAuthService authService)
        {
            this.uplataRepository = uplataRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.authService = authService;
        }

        /// <summary>
        /// Vraća sve uplate
        /// </summary>
        /// <returns>Lista uplata</returns>
        /// <response code = "200">Vraća listu uplata</response>
        /// <response code = "204">Ne postoji nijedna uplata</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [AllowAnonymous]
        public ActionResult<List<UplataDto>> GetUplate()
        {
            List<UplataEntity> uplate = uplataRepository.GetUplate();

            if (uplate == null || uplate.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<UplataDto>>(uplate));
        }

        /// <summary>
        /// Vraća traženu uplatu po ID-ju
        /// </summary>
        /// <param name="uplataId">ID uplate</param>
        /// <returns>Tražena uplata</returns>
        /// <response code = "200">Vraća traženu uplatu</response>
        /// <response code = "404">Nije pronađena tražena uplata</response>
        [HttpGet("{uplataId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public ActionResult<UplataDto> GetUplataById(Guid uplataId)
        {
            UplataEntity uplata = uplataRepository.GetUplataById(uplataId);

            if (uplata == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<UplataDto>(uplata));
        }

        /// <summary>
        /// Vraća listu uplata po ID-ju prijave za nadmetanje
        /// </summary>
        /// <param name="prijavaZaNadmetanjeId">ID prijave za nadmetanje</param>
        /// <returns>Lista uplata za prosleđenu prijavu za nadmetanje</returns>
        /// <response code = "200">Vraća listu uplata za prosleđenu prijavu za nadmetanje</response>
        /// <response code = "204">Ne postoji lista uplata za prosleđenu prijavu za nadmetanje</response>
        [HttpGet("/UplateZaPrijavu/{prijavaZaNadmetanjeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public ActionResult<List<UplataDto>> GetUplateByPrijavaZaNadmetanjeId(Guid prijavaZaNadmetanjeId)
        {
            List<UplataEntity> uplate = uplataRepository.GetUplateByPrijavaZaNadmetanjeId(prijavaZaNadmetanjeId);

            if (uplate == null || uplate.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<UplataDto>>(uplate));
        }


        /// <summary>
        /// Vraća listu uplata po ID-ju kupca
        /// </summary>
        /// <param name="kupacId">ID kupca</param>
        /// <returns>Lista uplata za prosleđenog kupca</returns>
        /// <response code = "200">Vraća listu uplata za prosleđenog kupca</response>
        /// <response code = "204">Ne postoji lista uplata za prosleđenog kupca</response>
        [HttpGet("/UplateZaKupca/{kupacId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public ActionResult<List<UplataDto>> GetUplateByKupacId(Guid kupacId)
        {
            List<UplataEntity> uplate = uplataRepository.GetUplateByKupacId(kupacId);

            if (uplate == null || uplate.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<UplataDto>>(uplate));
        }

        /// <summary>
        /// Kreira novu uplatu
        /// </summary>
        /// <param name="uplata"> model uplate</param>
        /// <param name="key"> ključ sa kojim se proverava autorizacija(key vrednost: Bearer LenkaSubotin)</param>
        /// <returns>Potvrda o kreiranoj uplati</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje nove uplate \
        /// POST /api/uplate \
        /// { \
        ///   "BrojRacuna" : 43604112, \
        ///   "PozivNaBroj" : 43100222, \
        ///   "Iznos" : 1500, \
        ///   "SvrhaUplate" : "Uplata za javno nadmetanje u 2022. godini", \
        ///   "Datum" : "2022-02-10" \
        ///   "BankaId" : "9aef1da1-d5af-4073-9d40-8794f9d33564", \
        ///   "PrijavaZaNadmetanjeId" = "1cd5c783-4bf5-4bbc-b7f0-bd66e2ba0bd7" \
        /// } 
        /// </remarks>
        /// <response code = "201">Vraća kreiranu uplatu</response>
        /// <response code="401">Lice koje želi da izvrši kreiranje uplate nije autorizovani korisnik</response>
        /// <response code = "500">Došlo je do greške na serveru prilikom kreiranja uplate</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<UplataDto> CreateUplata([FromBody] UplataCreateDto uplata, [FromHeader] string key)
        {
            if (!authService.Authorize(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Korisnik nije autorizovan!");
            }

            try
            {
                UplataEntity uplata1 = mapper.Map<UplataEntity>(uplata);
                UplataEntity uplata2 = uplataRepository.CreateUplata(uplata1);
                uplataRepository.SaveChanges();
                string location = linkGenerator.GetPathByAction("GetUplate", "Uplata", new { uplataId = uplata1.UplataId });
                return Created(location, mapper.Map<UplataEntity>(uplata2));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska prilikom kreiranja uplate!");
            }
        }

        /// <summary>
        /// Briše uplatu na osnovu ID-ja
        /// </summary>
        /// <param name="uplataId">ID uplate</param>
        /// /// <param name="key"> ključ sa kojim se proverava autorizacija(key vrednost: Bearer LenkaSubotin)</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Uplata uspešno obrisana</response>
        /// <response code="401">Lice koje želi da izvrši brisanje nije autorizovani korisnik</response>
        /// <response code="404">Nije pronađena uplata za brisanje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom brisanja uplate</response>
        [HttpDelete("{uplataId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult DeleteUplata(Guid uplataId, [FromHeader] string key)
        {
            if (!authService.Authorize(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Korisnik nije autorizovan!");
            }

            try
            {
                UplataEntity uplata = uplataRepository.GetUplataById(uplataId); 
                if (uplata == null)
                {
                    return NotFound();
                }
                uplataRepository.DeleteUplata(uplataId);
                uplataRepository.SaveChanges();
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska prilikom brisanja uplate!");
            }
        }

        /// <summary>
        /// Ažurira jednu uplatu
        /// </summary>
        /// <param name="uplata">Model uplate koja se ažurira</param>
        /// <param name="key"> ključ sa kojim se proverava autorizacija(key vrednost: Bearer LenkaSubotin)</param>
        /// <returns>Potvrda o ažuriranoj uplati</returns>
        /// <response code="200">Vraća ažuriranu uplatu</response>
        /// <response code="401">Lice koje želi da izvrši ažuriranje nije autorizovani korisnik</response>
        /// <response code="404">Nije pronađena uplata za ažuriranje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom ažuriranja uplate</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<UplataDto> UpdateOglas(UplataEntity uplata, [FromHeader] string key)
        {
            if (!authService.Authorize(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Korisnik nije autorizovan!");
            }

            try
            {
                var oldUplata = uplataRepository.GetUplataById(uplata.UplataId);

                if (oldUplata == null)
                {
                    return NotFound();
                }

                UplataEntity uplata1 = mapper.Map<UplataEntity>(uplata);
                mapper.Map(uplata1, oldUplata);
                uplataRepository.SaveChanges();

                return Ok(mapper.Map<UplataDto>(uplata1));

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska prilikom azuriranja uplate!");
            }
        }

        /// <summary>
        /// Vraća informacije o opcijama koje je moguće izvršiti za sve uplate
        /// </summary>
        /// <response code="200">Vraća informacije o opcijama koje je moguće izvršiti</response>
        [HttpOptions]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [AllowAnonymous]
        public IActionResult GetUplataOptions()
        {
            Response.Headers.Add("Allow", "GET, HEAD, POST, PUT, DELETE");
            return Ok();
        }
    }
}

