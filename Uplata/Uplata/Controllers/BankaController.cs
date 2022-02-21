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
    [Route("api/banke")]
    [Consumes("application/json")]
    [Produces("application/json", "application/xml")]
    public class BankaController : ControllerBase
    {
        private readonly IBankaRepository bankaRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly IAuthService authService;

        public BankaController(IBankaRepository bankaRepository, LinkGenerator linkGenerator, IMapper mapper, IAuthService authService)
        {
            this.bankaRepository = bankaRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.authService = authService;
        }

        /// <summary>
        /// Vraća sve banke
        /// </summary>
        /// <returns>Lista banki</returns>
        /// <response code = "200">Vraća listu banki</response>
        /// <response code = "204">Ne postoji nijedna banka</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [AllowAnonymous]
        public ActionResult<List<BankaDto>> GetBanke()
        {
            List<BankaEntity> banke = bankaRepository.GetBanke();

            if (banke == null || banke.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<BankaDto>>(banke));
        }

        /// <summary>
        /// Vraća traženu banku po ID-ju
        /// </summary>
        /// <param name="bankaId">ID banke</param>
        /// <returns>Tražena banka</returns>
        /// <response code = "200">Vraća traženu banku</response>
        /// <response code = "404">Nije pronađena tražena banka</response>
        [HttpGet("{bankaId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public ActionResult<BankaDto> GetBankaById(Guid bankaId)
        {
            BankaEntity banka = bankaRepository.GetBankaById(bankaId);

            if (banka == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<BankaDto>(banka));
        }

        /// <summary>
        /// Kreira novu banku
        /// </summary>
        /// <param name="banka"> model banke</param>
        /// <param name="key"> ključ sa kojim se proverava autorizacija(key vrednost: Bearer LenkaSubotin)</param>
        /// <returns>Potvrda o kreiranoj banci</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje nove banke \
        /// POST /api/banke \
        /// { \
        ///  "NazivBanke" : "OTP banka", \
        ///  "Adresa" : "OTP banka", \
        ///  "Grad" : "Novi Sad", \
        /// } 
        /// </remarks>
        /// <response code = "201">Vraća kreiranu banku</response>
        /// <response code="401">Lice koje želi da izvrši kreiranje banke nije autorizovani korisnik</response>
        /// <response code = "500">Došlo je do greške na serveru prilikom kreiranja banke</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<BankaDto> CreateBanka([FromBody] BankaCreateDto banka, [FromHeader] string key)
        {
            if (!authService.Authorize(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Korisnik nije autorizovan!");
            }

            try
            {
                BankaEntity banka1 = mapper.Map<BankaEntity>(banka);
                BankaEntity banka2 = bankaRepository.CreateBanka(banka1);
                bankaRepository.SaveChanges();
                string location = linkGenerator.GetPathByAction("GetBanke", "Banka", new { bankaId = banka1.BankaId });
                return Created(location, mapper.Map<BankaEntity>(banka2));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska prilikom kreiranja banke!");
            }
        }

        /// <summary>
        /// Briše banku na osnovu ID-ja
        /// </summary>
        /// <param name="bankaId">ID banke</param>
        /// /// <param name="key"> ključ sa kojim se proverava autorizacija(key vrednost: Bearer LenkaSubotin)</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Banka uspešno obrisana</response>
        /// <response code="401">Lice koje želi da izvrši brisanje nije autorizovani korisnik</response>
        /// <response code="404">Nije pronađena banka za brisanje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom brisanja banke</response>
        [HttpDelete("{bankaId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult DeleteBanka(Guid bankaId, [FromHeader] string key)
        {
            if (!authService.Authorize(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Korisnik nije autorizovan!");
            }

            try
            {
                BankaEntity banka = bankaRepository.GetBankaById(bankaId);
                if (banka == null)
                {
                    return NotFound();
                }
                bankaRepository.DeleteBanka(bankaId);
                bankaRepository.SaveChanges();
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska prilikom brisanja banke!");
            }
        }

        /// <summary>
        /// Ažurira jednu banku
        /// </summary>
        /// <param name="banka">Model banke koje se ažurira</param>
        /// <param name="key"> ključ sa kojim se proverava autorizacija(key vrednost: Bearer LenkaSubotin)</param>
        /// <returns>Potvrda o ažuriranoj banci</returns>
        /// <response code="200">Vraća ažuriranu banku</response>
        /// <response code="401">Lice koje želi da izvrši ažuriranje nije autorizovani korisnik</response>
        /// <response code="404">Nije pronađena banka za ažuriranje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom ažuriranja banke</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<BankaDto> UpdateBanka(BankaEntity banka, [FromHeader] string key)
        {
            if (!authService.Authorize(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Korisnik nije autorizovan!");
            }

            try
            {
                var oldBanka = bankaRepository.GetBankaById(banka.BankaId);

                if (oldBanka == null)
                {
                    return NotFound();
                }

                BankaEntity banka1 = mapper.Map<BankaEntity>(banka);
                mapper.Map(banka1, oldBanka);
                bankaRepository.SaveChanges();

                return Ok(mapper.Map<BankaDto>(banka1));

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska prilikom azuriranja banke!");
            }
        }

        /// <summary>
        /// Vraća informacije o opcijama koje je moguće izvršiti za sve banke
        /// </summary>
        /// <response code="200">Vraća informacije o opcijama koje je moguće izvršiti</response>
        [HttpOptions]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [AllowAnonymous]
        public IActionResult GetBankaOptions()
        {
            Response.Headers.Add("Allow", "GET, HEAD, POST, PUT, DELETE");
            return Ok();
        }
    }
}
