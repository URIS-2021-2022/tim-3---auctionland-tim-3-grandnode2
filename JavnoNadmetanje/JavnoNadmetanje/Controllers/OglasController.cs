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
    [Route("api/oglasi")]
    [Consumes("application/json")]
    [Produces("application/json", "application/xml")]
    public class OglasController : ControllerBase
    {
        private readonly IOglasRepository oglasRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly IAuthService authService;

        public OglasController(IOglasRepository oglasRepository, LinkGenerator linkGenerator, IMapper mapper, IAuthService authService)
        {
            this.oglasRepository = oglasRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.authService = authService;
        }

        /// <summary>
        /// Vraća sve oglase
        /// </summary>
        /// <returns>Lista oglasa</returns>
        /// <response code = "200">Vraća listu oglasa</response>
        /// <response code = "204">Ne postoji nijedan oglas</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [AllowAnonymous]
        public ActionResult<List<OglasDto>> GetOglasi()
        {
            List<OglasEntity> oglasi = oglasRepository.GetOglasi();

            if (oglasi == null || oglasi.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<OglasDto >>(oglasi));
        }

        /// <summary>
        /// Vraća traženi oglas po ID-ju
        /// </summary>
        /// <param name="oglasId">ID oglasa</param>
        /// <returns>Traženi oglas</returns>
        /// <response code = "200">Vraća traženi oglas</response>
        /// <response code = "404">Nije pronađen traženi oglas</response>
        [HttpGet("{oglasId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public ActionResult<OglasDto> GetOglasById(Guid oglasId)
        {
            OglasEntity oglas = oglasRepository.GetOglasById(oglasId);

            if (oglas == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<OglasDto>(oglas));
        }

        /// <summary>
        /// Kreira novi oglas
        /// </summary>
        /// <param name="oglas"> model oglasa</param>
        /// <param name="key"> ključ sa kojim se proverava autorizacija(key vrednost: Bearer LenkaSubotin)</param>
        /// <returns>Potvrda o kreiranom oglasu</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje novog oglasa \
        /// POST /api/oglasi \
        /// { \
        /// "DatumObjavljivanjaOglasa" : "2022-10-05", \
        /// "GodinaObjavljivanjaOglasa" : 2022, \
        /// "SluzbeniListId" : "1a0d7558-2ebc-45df-83d3-13066c36d42b" \
        /// } 
        /// </remarks>
        /// <response code = "201">Vraća kreiran oglas</response>
        /// <response code="401">Lice koje želi da izvrši kreiranje oglasa nije autorizovani korisnik</response>
        /// <response code = "500">Došlo je do greške na serveru prilikom kreiranja oglasa</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<OglasDto> CreateOglas([FromBody] OglasCreateDto oglas, [FromHeader] string key)
        {
            if (!authService.Authorize(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Korisnik nije autorizovan!");
            }

            try
            {
                OglasEntity oglas1 = mapper.Map<OglasEntity>(oglas);
                OglasEntity oglas2 = oglasRepository.CreateOglas(oglas1);
                oglasRepository.SaveChanges();
                string location = linkGenerator.GetPathByAction("GetOglasi", "Oglas", new { oglasId = oglas1.OglasId });
                return Created(location, mapper.Map<OglasEntity>(oglas2));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska prilikom kreiranja oglasa!");
            }
        }

        /// <summary>
        /// Briše oglas na osnovu ID-ja
        /// </summary>
        /// <param name="oglasId">ID oglasa</param>
        /// <param name="key"> ključ sa kojim se proverava autorizacija(key vrednost: Bearer LenkaSubotin)</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Oglas uspešno obrisan</response>
        /// <response code="401">Lice koje želi da izvrši brisanje nije autorizovani korisnik</response>
        /// <response code="404">Nije pronađen oglas za brisanje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom brisanja oglasa</response>
        [HttpDelete("{oglasId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult DeleteOglas(Guid oglasId, [FromHeader] string key)
        {
            if (!authService.Authorize(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Korisnik nije autorizovan!");
            }

            try
            {
                OglasEntity oglas = oglasRepository.GetOglasById(oglasId); ;
                if (oglas == null)
                {
                    return NotFound();
                }

                oglasRepository.DeleteOglas(oglasId);
                oglasRepository.SaveChanges();
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska prilikom brisanja oglasa!");
            }
        }

        /// <summary>
        /// Ažurira jedan oglas 
        /// </summary>
        /// <param name="oglas">Model oglasa koji se ažurira</param>
        /// <param name="key"> ključ sa kojim se proverava autorizacija(key vrednost: Bearer LenkaSubotin)</param>
        /// <returns>Potvrda o ažuriranom oglasu</returns>
        /// <response code="200">Vraća ažuriran oglas</response>
        /// <response code="401">Lice koje želi da izvrši ažuriranje nije autorizovani korisnik</response>
        /// <response code="404">Nije pronađen oglas za ažuriranje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom ažuriranja oglasa</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<OglasDto> UpdateOglas(OglasEntity oglas, [FromHeader] string key)
        {
            if (!authService.Authorize(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Korisnik nije autorizovan!");
            }

            try
            {
                var oldOglas = oglasRepository.GetOglasById(oglas.OglasId);

                if (oldOglas == null)
                {
                    return NotFound();
                }

                OglasEntity oglas1 = mapper.Map<OglasEntity>(oglas);
                mapper.Map(oglas1, oldOglas);
                oglasRepository.SaveChanges();

                return Ok(mapper.Map<OglasDto>(oglas1));

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska prilikom azuriranja oglasa!");
            }
        }

        /// <summary>
        /// Vraća informacije o opcijama koje je moguće izvršiti za sve oglase
        /// </summary>
        /// <response code="200">Vraća informacije o opcijama koje je moguće izvršiti</response>
        [HttpOptions]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [AllowAnonymous]
        public IActionResult GetOglasOptions()
        {
            Response.Headers.Add("Allow", "GET, HEAD, POST, PUT, DELETE");
            return Ok();
        }
    }
}
