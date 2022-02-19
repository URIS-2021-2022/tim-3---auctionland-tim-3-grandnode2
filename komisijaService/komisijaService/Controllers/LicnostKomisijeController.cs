using AutoMapper;
using komisijaService.Auth;
using komisijaService.Data;
using komisijaService.DTOs;
using komisijaService.Entites;
using komisijaService.Logger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace komisijaService.Controllers
{
    /// <summary>
    /// LicnostKomisije controller sa CRUD endpointima
    /// </summary>
    [ApiController]
    [Route("api/licnostiKomisije")]
    [Produces("application/json", "application/xml")]
    public class LicnostKomisijeController : ControllerBase
    {
        private readonly IKomisijaRepository komisijaRepository;
        private readonly ILicnostKomisijeRepository licnostKomisijeRepository;
        private readonly IMapper mapper;
        private readonly LinkGenerator linkGenerator;
        private readonly ILoggerMockRepository logger;
        private readonly IHttpContextAccessor contextAccessor;
        

        private readonly IAuthHelper auth;

        public LicnostKomisijeController(IKomisijaRepository komisijaRepository, ILicnostKomisijeRepository licnostKomisijeRepository, IMapper mapper, ILoggerMockRepository logger,
                                  LinkGenerator linkGenerator, IHttpContextAccessor contextAccessor, IAuthHelper auth)
        {
            this.komisijaRepository = komisijaRepository;
            this.licnostKomisijeRepository = licnostKomisijeRepository;
            this.mapper = mapper;
            this.linkGenerator = linkGenerator;
            this.logger = logger;
            this.contextAccessor = contextAccessor;
            this.auth = auth;
            
        }

        /// <summary>
        /// Vraca listu svih licnosti komisije
        /// </summary>
        /// <param name="imeLicnostiKomisije">Ime licnosti komisije</param>
        /// <param name="prezimeLicnostiKomisije">Prezime licnosti komisije</param>
        /// <param name="oznakaKomisije">Oznaka komisije</param>
        /// <returns>Lista svih licnosti komisije</returns>
        /// <remarks> 
        /// Primer request-a \
        /// GET 'https://localhost:44306/api/licnostiKomisije' \
        /// </remarks>
        /// <response code="200">Success answer - return all licnosti komisije</response>
        /// <response code="204">No content</response>
        /// <response code="500">Server error</response>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<LicnostKomisije>> GetLicnostiKomisije(string imeLicnostiKomisije, string prezimeLicnostiKomisije, string oznakaKomisije)
        {

            var licnostiKomisije = licnostKomisijeRepository.GetLicnostiKomisije(imeLicnostiKomisije,prezimeLicnostiKomisije, oznakaKomisije);

            if (licnostiKomisije == null || licnostiKomisije.Count == 0)
            {
                return NoContent();
            }

            logger.Log(LogLevel.Information, contextAccessor.HttpContext.TraceIdentifier, "", "Get sve licnosti komisije", null);
            return Ok(mapper.Map<List<LicnostKomisijeConfirmationDto>>(licnostiKomisije));

        }

        /// <summary>
        /// Vraca licnost komisije sa specificiranim licnostKomisijeId
        /// </summary>
        /// <param name="licnostKomisijeId">Jedinstevni identifikator licnosti komisije</param>
        /// <remarks>    
        /// Primer request-a \
        /// GET 'https://localhost:44306/api/licnostKomisije/' \
        ///     --param  'licnostKomisijeId = 4E1F1F8D-A8F7-44B1-9BDA-1C1EE122628D'
        /// </remarks>
        /// <response code="200">Success - vraca licnost komisije sa vrednosti identifikatora licnostKomisijeId</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Server error</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{licnostKomisijeId}")]
        [AllowAnonymous]
        public ActionResult<LicnostKomisije> GetLicnostKomisijeById(Guid licnostKomisijeId)
        {

            var licnostKomisije = licnostKomisijeRepository.GetLicnostKomisijeById(licnostKomisijeId);

            if (licnostKomisije == null)
            {
                return NotFound();
            }

            logger.Log(LogLevel.Information, contextAccessor.HttpContext.TraceIdentifier, "", "Get licnost komisije by licnostKomisijeId", null);
            return Ok(mapper.Map<LicnostKomisijeConfirmationDto>(licnostKomisije));

        }

        

        /// <summary>
        /// Dodaj novu licnost komisije
        /// </summary>
        /// <param name="LicnostKomisijeDto">Model licnosti komisije</param>
        /// <param name="key">Authorization Key Value</param>
        /// <remarks>
        /// Primer request-a \
        /// POST 'https://localhost:44306/api/licnostiKomisije'\
        ///     --header 'key: Bearer DunjaZamaklar' \
        /// Example: \
        /// { \
        ///         imeLicnostiKomisije: "Mina",
        ///         prezimeLicnostiKomisije: "Spasic",
        ///         funkcijaLicnostiKomisije: "Obican clan",
        ///         kontaktLicnostiKomisije: "0672514739",
        ///         datumRodjenjaLicnostiKomisije: "1976-01-19T00:00:00",
        ///         oznakaKomisije: "kom345ef"        
        ///}
        /// </remarks>
        /// <response code="201">Success - vraca kreiranu licnost komisije</response>
        /// <response code="401">Unauthorized user</response>
        /// <response code="500">Server error</response>
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public ActionResult<LicnostKomisijeConfirmationDto> CreateLicnostKomisije([FromBody] LicnostKomisijeCreationDto licnostKomisijeDto, [FromHeader] string key)
        {
            try
            {
                if (key == null)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "Authorization failed!");
                }
                if (!auth.AuthorizeUser(key))
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "Authorization failed!");
                }
                
                LicnostKomisije licnostKomisije = mapper.Map<LicnostKomisije>(licnostKomisijeDto);
                Komisija komisija = komisijaRepository.GetKomisjaByOznaka(licnostKomisije.oznakaKomisije);
                if(komisija==null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Ne postoji komisija sa tom oznakom.");
                }
                licnostKomisije.komisijaId = komisija.komisijaId;
                licnostKomisijeRepository.CreateLicnostKomisije(licnostKomisije);
                licnostKomisijeRepository.SaveChanges();

                logger.Log(LogLevel.Information, contextAccessor.HttpContext.TraceIdentifier, "", "Kreirana nova licnost komisije", null);

                string location = linkGenerator.GetPathByAction("GetLicnostKomisijeById", "LicnostKomisije", new { licnostKomisijeId = licnostKomisije.licnostKomisijeId });

                return Created(location, mapper.Map<LicnostKomisijeConfirmationDto>(licnostKomisije));
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, contextAccessor.HttpContext.TraceIdentifier, "", "Greska prilikom kreiranja licnosti komisije", null);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        /// <summary>
        /// Update licnost komisije
        /// </summary>
        /// <param name="licnostKomisijeUpdateDto">Model licnosti komisije</param>
        /// <param name="licnostKomisijeId">jedinstevni identifikator licnosti komisije</param>
        /// <param name="key">Authorization Key Value</param>
        /// <remarks>
        /// Primer request-a \
        /// PUT 'https://localhost:44306/api/licnostiKomisije/'\
        ///  --header 'key: Bearer DunjaZamaklar' \
        ///  --param  'licnostKomisijeId = 4E1F1F8D-A8F7-44B1-9BDA-1C1EE122628D'\
        /// Example: \
        /// { \
        ///         imeLicnostiKomisije: "Mina",
        ///         prezimeLicnostiKomisije: "Spasic",
        ///         funkcijaLicnostiKomisije: "Obican clan",
        ///         kontaktLicnostiKomisije: "0672514739",
        ///         datumRodjenjaLicnostiKomisije: "1976-01-19T00:00:00",
        ///         oznakaKomisije: "kom345ef"   
        /// } \
        /// </remarks>
        /// <response code="200">Success answer - update-ovana licnost komisije</response>
        /// <response code="401">Unauthorized user</response>
        /// <response code="403">Not allowed</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Server error</response>
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("{licnostKomisijeId}")]
        public ActionResult<LicnostKomisijeConfirmationDto> UpdateLicnostKomisije([FromBody] LicnostKomisijeUpdateDto licnostKomisijeUpdateDto, Guid licnostKomisijeId, [FromHeader] string key)
        {
            try
            {
                if (key == null)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "Authorization failed!");
                }
                if (!auth.AuthorizeUser(key))
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "Authorization failed!");
                }


                var oldLicnost = licnostKomisijeRepository.GetLicnostKomisijeById(licnostKomisijeId);
                if (oldLicnost == null)
                {
                    return NotFound();
                }
                LicnostKomisije newLicnost = mapper.Map<LicnostKomisije>(licnostKomisijeUpdateDto);
                newLicnost.licnostKomisijeId = licnostKomisijeId;
                Komisija komisija = komisijaRepository.GetKomisjaByOznaka(newLicnost.oznakaKomisije);
                if(komisija==null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Ne postoji komisija sa tom oznakom.");
                }
                newLicnost.komisijaId = komisija.komisijaId;

                licnostKomisijeRepository.UpdateLicnostKomisije(oldLicnost, newLicnost);

                licnostKomisijeRepository.SaveChanges();
                logger.Log(LogLevel.Information, contextAccessor.HttpContext.TraceIdentifier, "", "Licnost komisije je update-ovana", null);

                return Ok(mapper.Map<LicnostKomisijeConfirmationDto>(oldLicnost));
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, contextAccessor.HttpContext.TraceIdentifier, "", "Update error", null);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Obrisi licnost komisije
        /// </summary>
        /// <param name="licnostKomisijeId">Jedinstevni identifikator licnosti komisije</param>
        /// <param name="key">Authorization Key Value</param>
        /// <remarks>
        /// Example of request \
        /// DELETE 'https://localhost:44306/api/licnostiKomisije/'\
        ///  --header 'key: Bearer DunjaZamaklar' \
        ///  --param  'licnostKomisijeId = 4E1F1F8D-A8F7-44B1-9BDA-1C1EE122628D'\
        /// </remarks>
        /// <response code="200">Success answer</response>
        /// <response code="401">Unauthorized user</response>
        /// <response code="403">Not allowed</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Server error</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{licnostKomisijeId}")]
        public IActionResult DleteLicnostKomisije(Guid licnostKomisijeId, [FromHeader] string key)
        {
            try
            {
                if (key == null)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "Authorization failed!");
                }
                if (!auth.AuthorizeUser(key))
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "Authorization failed!");
                }

                var licnostKomisije = licnostKomisijeRepository.GetLicnostKomisijeById(licnostKomisijeId);

                if (licnostKomisije == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Nema licnosti komisije!");
                }

                licnostKomisijeRepository.DeleteLicnostKomisije(licnostKomisijeId);
                licnostKomisijeRepository.SaveChanges();

                return NoContent();
            }

            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, contextAccessor.HttpContext.TraceIdentifier, "", "Delete error", null);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Vraca implementirane opcije rada sa servisom
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Example of request
        /// OPTIONS 'https://localhost:44395/api/licnostiKomisije'
        /// </remarks>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetLicnostKomisijeOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
        
    }
}
