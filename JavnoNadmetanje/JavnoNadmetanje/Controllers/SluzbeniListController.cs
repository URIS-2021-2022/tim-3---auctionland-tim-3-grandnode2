using AutoMapper;
using JavnoNadmetanje.Auth;
using JavnoNadmetanje.Data;
using JavnoNadmetanje.Entities;
using JavnoNadmetanje.Models;
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
    [Route("api/sluzbeniListovi")]
    [Consumes("application/json")]
    [Produces("application/json", "application/xml")]
    public class SluzbeniListController : ControllerBase
    {
        private readonly ISluzbeniListRepository sluzbeniListRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly IAuthService authService;

        public SluzbeniListController(ISluzbeniListRepository sluzbeniListRepository, LinkGenerator linkGenerator, IMapper mapper, IAuthService authService)
        {
            this.sluzbeniListRepository = sluzbeniListRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.authService = authService;
        }

        /// <summary>
        /// Vraća sve službene listove
        /// </summary>
        /// <returns>Lista službenih listova</returns>
        /// <response code = "200">Vraća listu službenih listova</response>
        /// <response code = "204">Ne postoji nijedan službeni list</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [AllowAnonymous]
        public ActionResult<List<SluzbeniListDto>> GetSluzbeniListovi()
        {
            List<SluzbeniListEntity> sluzbeniListovi = sluzbeniListRepository.GetSluzbeniListovi();

            if (sluzbeniListovi == null || sluzbeniListovi.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<SluzbeniListDto>>(sluzbeniListovi));
        }

        /// <summary>
        /// Vraća traženi službeni list po ID-ju
        /// </summary>
        /// <param name="sluzbeniListId">ID službenog lista</param>
        /// <returns>Traženi službeni list</returns>
        /// <response code = "200">Vraća traženi službeni list</response>
        /// <response code = "404">Nije pronađen traženi službeni list</response>
        [HttpGet("{sluzbeniListId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public ActionResult<SluzbeniListDto> GetSluzbeniListById(Guid sluzbeniListId)
        {
            SluzbeniListEntity sluzbeniList = sluzbeniListRepository.GetSluzbeniListById(sluzbeniListId);

            if (sluzbeniList == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<SluzbeniListDto>(sluzbeniList));
        }

        /// <summary>
        /// Kreira novi službeni list
        /// </summary>
        /// <param name="sluzbeniList"> model službenog lista</param>
        /// <param name="key"> ključ sa kojim se proverava autorizacija(key vrednost: Bearer LenkaSubotin)</param>
        /// <returns>Potvrda o kreiranom službenom listu</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje novog službenog lista \
        /// POST /api/sluzbeniListovi \
        /// { \
        /// "Opstina" : "Novi Sad", \
        /// "BrojSluzbenogLista" : 12, \
        /// "DatumIzdavanja": "2021-10-11" \
        /// } 
        /// </remarks>
        /// <response code = "201">Vraća kreiran službeni list</response>
        /// <response code="401">Lice koje želi da izvrši kreiranje službenog lista nije autorizovani korisnik</response>
        /// <response code = "500">Došlo je do greške na serveru prilikom kreiranja službenog lista</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<SluzbeniListDto> CreateSluzbeniList([FromBody] SluzbeniListCreateDto sluzbeniList, [FromHeader] string key)
        {
            if (!authService.Authorize(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Korisnik nije autorizovan!");
            }

            try
            {
                SluzbeniListEntity sList1 = mapper.Map<SluzbeniListEntity>(sluzbeniList);
                SluzbeniListEntity sList2 = sluzbeniListRepository.CreateSluzbeniList(sList1);
                sluzbeniListRepository.SaveChanges();
                string location = linkGenerator.GetPathByAction("GetSluzbeniListovi", "SluzbeniList", new { sluzbeniListId = sList1.SluzbeniListId });
                return Created(location, mapper.Map<SluzbeniListEntity>(sList2));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska prilikom kreiranja sluzbenog lista!");
            }
        }

        /// <summary>
        /// Briše službeni list na osnovu ID-ja
        /// </summary>
        /// <param name="sluzbeniListId">ID službenog lista</param>
        /// <param name="key"> ključ sa kojim se proverava autorizacija(key vrednost: Bearer LenkaSubotin)</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Službeni list uspešno obrisan</response>
        /// <response code="401">Lice koje želi da izvrši brisanje nije autorizovani korisnik</response>
        /// <response code="404">Nije pronađen službeni list za brisanje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom brisanja službenog lista</response>
        [HttpDelete("{sluzbeniListId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult DeleteSluzbeniList(Guid sluzbeniListId, [FromHeader] string key)
        {
            if (!authService.Authorize(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Korisnik nije autorizovan!");
            }

            try
            {
                SluzbeniListEntity sluzbeniList = sluzbeniListRepository.GetSluzbeniListById(sluzbeniListId); 
                if (sluzbeniList == null)
                {
                    return NotFound();
                }

                sluzbeniListRepository.DeleteSluzbeniList(sluzbeniListId);
                sluzbeniListRepository.SaveChanges();
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska prilikom brisanja sluzbenog lista!");
            }
        }

        /// <summary>
        /// Ažurira jedan službeni list 
        /// </summary>
        /// <param name="sluzbeniList">Model službenog lista koji se ažurira</param>
        /// <param name="key"> ključ sa kojim se proverava autorizacija(key vrednost: Bearer LenkaSubotin)</param>
        /// <returns>Potvrda o ažuriranom službenom listu</returns>
        /// <response code="200">Vraća ažuriran službeni list</response>
        /// <response code="401">Lice koje želi da izvrši ažuriranje nije autorizovani korisnik</response>
        /// <response code="404">Nije pronađen službeni list za ažuriranje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom ažuriranja službenog lista</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<SluzbeniListDto> UpdateSluzbeniList(SluzbeniListEntity sluzbeniList, [FromHeader] string key)
        {
            if (!authService.Authorize(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Korisnik nije autorizovan!");
            }

            try
            {
                var oldSluzbeniList = sluzbeniListRepository.GetSluzbeniListById(sluzbeniList.SluzbeniListId);

                if ( oldSluzbeniList == null)
                {
                    return NotFound();
                }

                SluzbeniListEntity sluzbeniList1 = mapper.Map<SluzbeniListEntity>(sluzbeniList);
                mapper.Map(sluzbeniList1, oldSluzbeniList);
                sluzbeniListRepository.SaveChanges();

                return Ok(mapper.Map<SluzbeniListDto>(sluzbeniList1));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska prilikom azuriranja sluzbenog lista!");
            }
        }

        /// <summary>
        /// Vraća informacije o opcijama koje je moguće izvršiti za sve službene listove
        /// </summary>
        /// <response code="200">Vraća informacije o opcijama koje je moguće izvršiti</response>
        [HttpOptions]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [AllowAnonymous]
        public IActionResult GetSluzbeniListOptions()
        {
            Response.Headers.Add("Allow", "GET, HEAD, POST, PUT, DELETE");
            return Ok();
        }
    }
}
