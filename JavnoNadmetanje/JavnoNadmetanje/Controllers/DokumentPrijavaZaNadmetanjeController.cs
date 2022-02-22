using AutoMapper;
using JavnoNadmetanje.Auth;
using JavnoNadmetanje.Data;
using JavnoNadmetanje.Entities;
using JavnoNadmetanje.Models;
using JavnoNadmetanje.Models.DokumentService;
using JavnoNadmetanje.ServiceCalls;
using Microsoft.AspNetCore.Authentication;
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
    [Route("api/dokumentiPrijave")]
    [Consumes("application/json")]
    [Produces("application/json", "application/xml")]
    public class DokumentPrijavaZaNadmetanjeController : ControllerBase
    {
        private readonly IDokumentPrijavaZaNadmetanjeRepository dokumentPrijavaZaNadmetanjeRepository;
        private readonly IDokumentService dokumentService;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly IAuthService authService;

        public DokumentPrijavaZaNadmetanjeController(IDokumentPrijavaZaNadmetanjeRepository dokumentPrijavaZaNadmetanjeRepository, IDokumentService dokumentService, LinkGenerator linkGenerator, IMapper mapper, IAuthService authService)
        {
            this.dokumentPrijavaZaNadmetanjeRepository= dokumentPrijavaZaNadmetanjeRepository;
            this.dokumentService = dokumentService;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.authService = authService;
        }

        /// <summary>
        /// Vraća sve veze između dokumenta i prijava za nadmetanje na osnovu prijave za nadmetanje ID
        /// </summary>
        /// <returns>Lista veza između dokumenata i prijave za nadmetanje</returns>
        /// <response code = "200">Vraća listu veza između dokumenata i prijava za nadmetanje</response>
        /// <response code = "204">Ne postoji nijedna veza između dokumenata i prijava za nadmetanje</response>
        [HttpGet("{prijavaZaNadmetanjeId}")]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [AllowAnonymous]
        public ActionResult<List<DokumentPrijavaZaNadmetanjeDto>> GetDokumentiPrijavaByPrijavaZaNadmetanjeId(Guid prijavaZaNadmetanjeId)
        {
            List<DokumentPrijavaZaNadmetanjeEntity> dokumentiPrijave = dokumentPrijavaZaNadmetanjeRepository.GetDokumentiPrijavaByPrijavaZaNadmetanjeId(prijavaZaNadmetanjeId);
            string accessToken = HttpContext.GetTokenAsync("access_token").Result;

            if (dokumentiPrijave == null || dokumentiPrijave.Count == 0)
            {
                return NoContent();
            }

            foreach(DokumentPrijavaZaNadmetanjeEntity dok in dokumentiPrijave)
            {
                DokumentDto dokument = dokumentService.GetDokumentById(dok.DokumentId, accessToken).Result;
                dok.Dokument = dokument;
            }

            return Ok(mapper.Map<List<DokumentPrijavaZaNadmetanjeDto>>(dokumentiPrijave));
        }

        /// <summary>
        /// Vraća vezu između dokumenta i prijave za nadmetanje po ID-ju dokumenta i prijave za nadmetanje
        /// </summary>
        /// <param name="prijavaZaNadmetanjeId">ID prijave za nadmetanje</param>
        ///  <param name="dokumentId">ID dokumenta</param>
        /// <returns>Tražena veza između dokumenta i prijave za nadmetanje</returns>
        /// <response code = "200">Vraća traženu vezu između dokumenta i prijave za nadmetanje</response>
        /// <response code = "404">Nije pronađena tražena veza između dokumenta i prijave za nadmetanje</response>
        [HttpGet("/ByPrijavaZaNadmetanjeId_DokumentId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public ActionResult<DokumentPrijavaZaNadmetanjeDto> GetDokumentPrijavaById(Guid prijavaZaNadmetanjeId, Guid dokumentId)
        {
            DokumentPrijavaZaNadmetanjeEntity dokumentPrijava = dokumentPrijavaZaNadmetanjeRepository.GetDokumentPrijavaById(prijavaZaNadmetanjeId, dokumentId);
            string accessToken = HttpContext.GetTokenAsync("access_token").Result;

            if (dokumentPrijava == null)
            {
                return NotFound();
            }

             DokumentDto dokument = dokumentService.GetDokumentById(dokumentPrijava.DokumentId, accessToken).Result;
             dokumentPrijava.Dokument = dokument;


            return Ok(mapper.Map<DokumentPrijavaZaNadmetanjeDto>(dokumentPrijava));
        }

        /// <summary>
        /// Kreira novu vezu između dokumenta i prijave za nadmetanje
        /// </summary>
        /// <param name="dokumentPrijava"> model veze između dokumenta i prijave za nadmetanje</param>
        /// <param name="key"> ključ sa kojim se proverava autorizacija(key vrednost: Bearer LenkaSubotin)</param>
        /// <returns>Potvrda o kreiranoj vezi između dokumenta i prijave za nadmetanje</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje nove veze između dokumenta i prijave za nadmetanje \
        /// POST /api/prijaveZaNadmetanje \
        /// { \
        /// "DatumDonosenjaDokumenta" : "09-02-2022" \
        /// } 
        /// </remarks>
        /// <response code = "201">Vraća kreiranu vezu između dokumenta i prijave za nadmetanje</response>
        /// <response code="401">Lice koje želi da izvrši kreiranje veze između dokumenta i prijave za nadmetanje nije autorizovani korisnik</response>
        /// <response code = "500">Došlo je do greške na serveru prilikom kreiranja veze između dokumenta i prijave za nadmetanje</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<DokumentPrijavaZaNadmetanjeDto> CreateDokumentPrijava([FromBody] DokumentPrijavaZaNadmetanjeDto dokumentPrijava, [FromHeader] string key)
        {
            if (!authService.Authorize(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Korisnik nije autorizovan!");
            }

            try
            {
                DokumentPrijavaZaNadmetanjeEntity dokumentPrijava1 = mapper.Map<DokumentPrijavaZaNadmetanjeEntity>(dokumentPrijava);
                DokumentPrijavaZaNadmetanjeEntity dokumentPrijava2 = dokumentPrijavaZaNadmetanjeRepository.CreateDokumentPrijava(dokumentPrijava1);
                dokumentPrijavaZaNadmetanjeRepository.SaveChanges();
                string location = linkGenerator.GetPathByAction("GetDokumentiPrijavaByPrijavaZaNadmetanjeId", "DokumentPrijavaZaNadmetanje", new { prijavaZaNadmetanjeId = dokumentPrijava1.PrijavaZaNadmetanjeId });
                return Created(location, mapper.Map<DokumentPrijavaZaNadmetanjeEntity>(dokumentPrijava2));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska prilikom kreiranja veze izmedju dokumenta i prijave za nadmetanje!");
            }
        }

        /// <summary>
        /// Briše vezu između dokumenta i prijavu za nadmetanje na osnovu ID-ja prijave za nadmetanje
        /// </summary>
        /// <param name="prijavaZaNadmetanjeId">ID prijave za nadmetanje</param>
        /// <param name="key"> ključ sa kojim se proverava autorizacija(key vrednost: Bearer LenkaSubotin)</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Veza između dokumenta i prijave za nadmetanje uspešno obrisana</response>
        /// <response code="401">Lice koje želi da izvrši brisanje nije autorizovani korisnik</response>
        /// <response code="404">Nije pronađena veza između dokumenta i prijave za nadmetanje za brisanje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom brisanja veze između dokumenta i prijave za nadmetanje</response>
        [HttpDelete("{prijavaZaNadmetanjeId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult DeleteDokumentPrijavaByPrijavaZaNadmetanjeId(Guid prijavaZaNadmetanjeId, [FromHeader] string key)
        {
            if (!authService.Authorize(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Korisnik nije autorizovan!");
            }

            try
            {
                var dokumentPrijava = dokumentPrijavaZaNadmetanjeRepository.GetDokumentiPrijavaByPrijavaZaNadmetanjeId(prijavaZaNadmetanjeId);
                if (dokumentPrijava == null)
                {
                    return NotFound();
                }

                dokumentPrijavaZaNadmetanjeRepository.DeleteDokumentPrijavaByPrijavaZaNadmetanjeId(prijavaZaNadmetanjeId);
                dokumentPrijavaZaNadmetanjeRepository.SaveChanges();
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska prilikom brisanja veze izmedju dokumenta i prijave za nadmetanje!");
            }
        }

        /// <summary>
        /// Ažurira jednu vezu između dokumenta i prijave za nadmetanje
        /// </summary>
        /// <param name="dokumentPrijava">Model veze između dokumenta i prijave za nadmetanje koji se ažurira</param>
        /// <param name="key"> ključ sa kojim se proverava autorizacija(key vrednost: Bearer LenkaSubotin)</param>
        /// <returns>Potvrda o ažuriranoj vezi između dokumenta i prijave za nadmetanje</returns>
        /// <response code="200">Vraća ažuriranu vezu između dokumenta i prijave za nadmetanje</response>
        /// <response code="401">Lice koje želi da izvrši ažuriranje nije autorizovani korisnik</response>
        /// <response code="404">Nije pronađena  veza između dokumenta i prijave za nadmetanje za ažuriranje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom ažuriranja veze između dokumenta i prijave za nadmetanje</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<DokumentPrijavaZaNadmetanjeDto> UpdateDokumentPrijava(DokumentPrijavaZaNadmetanjeEntity dokumentPrijava, [FromHeader] string key)
        {
            if (!authService.Authorize(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Korisnik nije autorizovan!");
            }

            try
            {
                var oldDokumentPrijava = dokumentPrijavaZaNadmetanjeRepository.GetDokumentPrijavaById(dokumentPrijava.PrijavaZaNadmetanjeId, dokumentPrijava.DokumentId);

                if (oldDokumentPrijava == null)
                {
                    return NotFound();
                }

                DokumentPrijavaZaNadmetanjeEntity dokumentPrijava1 = mapper.Map<DokumentPrijavaZaNadmetanjeEntity>(dokumentPrijava);
                mapper.Map(dokumentPrijava1, oldDokumentPrijava);
                dokumentPrijavaZaNadmetanjeRepository.SaveChanges();

                return Ok(mapper.Map<DokumentPrijavaZaNadmetanjeDto>(dokumentPrijava1));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska prilikom azuriranja veze izmedju dokumenta i prijave za nadmetanje!");
            }
        }

        /// <summary>
        /// Vraća informacije o opcijama koje je moguće izvršiti za sve dokumente prijave za nadmetanje
        /// </summary>
        /// <response code="200">Vraća informacije o opcijama koje je moguće izvršiti</response>
        [HttpOptions]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [AllowAnonymous]
        public IActionResult GetDokumentPrijavaOptions()
        {
            Response.Headers.Add("Allow", "GET, HEAD, POST, PUT, DELETE");
            return Ok();
        }
    }
}
