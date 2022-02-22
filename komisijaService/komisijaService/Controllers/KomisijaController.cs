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
using System.Linq;
using System.Threading.Tasks;

namespace komisijaService.Controllers
{
    /// <summary>
    /// Komisija controller sa CRUD endpointima
    /// </summary>
    [ApiController]
    [Route("api/komisije")]
    [Produces("application/json", "application/xml")]
    public class KomisijaController : ControllerBase
    {
        private readonly IKomisijaRepository komisijaRepository;
        private readonly ILicnostKomisijeRepository licnostKomisijeRepository;
        private readonly IMapper mapper;
        private readonly LinkGenerator linkGenerator;
        private readonly ILoggerMockRepository logger;
        private readonly IHttpContextAccessor contextAccessor;

        private readonly IAuthHelper auth;

            public KomisijaController(IKomisijaRepository komisijaRepository, ILicnostKomisijeRepository licnostKomisijeRepository, IMapper mapper, ILoggerMockRepository logger,
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
        /// Vraca listu svih komisija
        /// </summary>
        /// <param name="naziv">Naziv komisije</param>
        /// <param name="oznakaKomisije">Oznaka komisije</param>
        /// <returns>Lista svih komisija</returns>
        /// <remarks> 
        /// Primer request-a \
        /// GET 'https://localhost:44306/api/komisije' \
        /// </remarks>
        /// <response code="200">Success answer - return all komisije</response>
        /// <response code="204">No content</response>
        /// <response code="500">Server error</response>
        [HttpGet]
            [AllowAnonymous]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status204NoContent)]
            [ProducesResponseType(StatusCodes.Status500InternalServerError)]
            public ActionResult<List<Komisija>> GetKomisije(string naziv, string oznakaKomisije)
            {

                var komisije = komisijaRepository.GetKomsijas(naziv,oznakaKomisije);

                if (komisije == null || komisije.Count == 0)
                {
                    return NoContent();
                }
                foreach (Komisija k in komisije)
                {
                    k.clanoviKomisije = licnostKomisijeRepository.GetLicnosiKomisijeByOznakaKomisije(k.oznakaKomisije);
                    mapper.Map<List<LicnostKomisijeConfirmationDto>>(k.clanoviKomisije);
                    k.predsednikKomisije = licnostKomisijeRepository.GetPredsednikaKomisije(k.komisijaId);
                if (k.predsednikKomisije != null)
                {
                    k.clanoviKomisije.Remove(k.predsednikKomisije);
                    mapper.Map<LicnostKomisijeConfirmationDto>(k.predsednikKomisije);

                }
                }
                
                logger.Log(LogLevel.Information, contextAccessor.HttpContext.TraceIdentifier, "", "Get sve komisije", null);
                return Ok(mapper.Map<List<KomisijaConfirmationDto>>(komisije));

            }

        /// <summary>
        /// Vraca komisiju sa specificiranim komisijaId
        /// </summary>
        /// <param name="komisijaId">Jedinstevni identifikator komisije</param>
        /// <remarks>    
        /// Primer request-a \
        /// GET 'https://localhost:44306/api/komisije/' \
        ///     --param  'komisijaId = 4E1F1F8D-A8F7-44B1-9ABD-1C1EE122628D'
        /// </remarks>
        /// <response code="200">Success - vraca komisiju sa vrednosti identifikatora komisijaId</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Server error</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            [ProducesResponseType(StatusCodes.Status500InternalServerError)]
            [HttpGet("{komisijaId}")]
            [AllowAnonymous]
            public ActionResult<Komisija> GetKomisijaById(Guid komisijaId)
            {

                var komisija = komisijaRepository.GetKomisijaById(komisijaId);

            if (komisija == null)
                {
                    return NotFound();
                }
            komisija.clanoviKomisije = licnostKomisijeRepository.GetLicnosiKomisijeByOznakaKomisije(komisija.oznakaKomisije);
            mapper.Map<List<LicnostKomisijeConfirmationDto>>(komisija.clanoviKomisije);
            komisija.predsednikKomisije = licnostKomisijeRepository.GetPredsednikaKomisije(komisija.komisijaId);
            if (komisija.predsednikKomisije != null)
            {
                komisija.clanoviKomisije.Remove(komisija.predsednikKomisije);
                mapper.Map<LicnostKomisijeConfirmationDto>(komisija.predsednikKomisije);
            }

            logger.Log(LogLevel.Information, contextAccessor.HttpContext.TraceIdentifier, "", "Get komisija by komisijaId", null);
                return Ok(mapper.Map<KomisijaConfirmationDto>(komisija));

            }
        

        /// <summary>
        /// Dodaj novu komisiju
        /// </summary>
        /// <param name="KomisijaDto">Model komisije</param>
        /// <param name="key">Authorization Key Value</param>
        /// <remarks>
        /// Primer request-a \
        /// POST 'https://localhost:44306/api/komisije'\
        ///     --header 'key: Bearer DunjaZamaklar' \
        /// Example: \
        /// { \
        ///         oznakaKomisije = "kom556ef" \
        ///         nazivKomisije = "Treca komisija", \
        ///}
        /// </remarks>
        /// <response code="201">Success - vraca kreirane komisije</response>
        /// <response code="401">Unauthorized user</response>
        /// <response code="500">Server error</response>
        [Consumes("application/json")]
            [ProducesResponseType(StatusCodes.Status201Created)]
            [ProducesResponseType(StatusCodes.Status401Unauthorized)]
            [ProducesResponseType(StatusCodes.Status500InternalServerError)]
            [HttpPost]
            public ActionResult<KomisijaConfirmationDto> CreateKomisija([FromBody] KomisijaCreationDto komisijaDto, [FromHeader] string key)
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
                string oznakaKomisije = komisijaDto.oznakaKomisije;
                Komisija kom = komisijaRepository.GetKomisjaByOznaka(oznakaKomisije);
                if(kom!=null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Vrednost obelezja oznakaKomisije mora biti jednistvena!");
                }
                Komisija komisija = mapper.Map<Komisija>(komisijaDto);
                    komisijaRepository.CreateKomsija(komisija);
                    komisijaRepository.SaveChanges();

                    logger.Log(LogLevel.Information, contextAccessor.HttpContext.TraceIdentifier, "", "Kreirana nova komisija", null);

                    string location = linkGenerator.GetPathByAction("GetKomisijaById", "Komisija", new { komisijaId = komisija.komisijaId });

                    return Created(location, mapper.Map<KomisijaConfirmationDto>(komisija));
                }
                catch (Exception ex)
                {
                    logger.Log(LogLevel.Error, contextAccessor.HttpContext.TraceIdentifier, "", "Greska prilikom kreiranja komisije", null);
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }


        /// <summary>
        /// Update komisiju
        /// </summary>
        /// <param name="komisijaUpdateDto">Model komisije</param>
        /// <param name="komisijaId">jedinstevni identifikator komisije</param>
        /// <param name="key">Authorization Key Value</param>
        /// <remarks>
        /// Primer request-a \
        /// PUT 'https://localhost:44306/api/komisije/'\
        ///  --header 'key: Bearer DunjaZamaklar' \
        ///  --param  'komisijaId = 4E1F1F8D-A8F7-44B1-9ABD-1C1EE122628D'\
        /// Example: \
        /// { \
        ///        "nazivKomisije": "Update Test service", \
        ///        "oznakaKomisije": "Update Test description", \
        /// } \
        /// </remarks>
        /// <response code="200">Success answer - update-ovana komisija</response>
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
            [HttpPut("{komisijaId}")]
            public ActionResult<KomisijaConfirmationDto> UpdateKomisija([FromBody] KomisijaUpdateDto komisijaUpdateDto, Guid komisijaId, [FromHeader] string key)
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


                    var oldKomisija = komisijaRepository.GetKomisijaById(komisijaId);
                    if (oldKomisija == null)
                    {
                        return NotFound();
                    }
                    Komisija newKomisija = mapper.Map<Komisija>(komisijaUpdateDto);
                    newKomisija.komisijaId = komisijaId;
                    List<LicnostKomisije> licnostiKomisije = licnostKomisijeRepository.GetLicnosiKomisijeByOznakaKomisije(oldKomisija.oznakaKomisije);
                    if (newKomisija.oznakaKomisije != oldKomisija.oznakaKomisije)
                        {
                      
                            if (licnostiKomisije.Count > 0)
                            {
                                foreach (LicnostKomisije lk in licnostiKomisije)
                                {
                                    licnostKomisijeRepository.UpdateOznakuKomisije(lk, newKomisija.oznakaKomisije);
                                }
                            }
                            licnostKomisijeRepository.SaveChanges();
                    }

                    komisijaRepository.UpdateKomisija(oldKomisija, newKomisija);
                    
                    komisijaRepository.SaveChanges();
                    logger.Log(LogLevel.Information, contextAccessor.HttpContext.TraceIdentifier, "", "Komisija update-ovana", null);

                    return Ok(mapper.Map<KomisijaConfirmationDto>(oldKomisija));
                }
                catch (Exception ex)
                {
                    logger.Log(LogLevel.Error, contextAccessor.HttpContext.TraceIdentifier, "", "Update error", null);
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }

        /// <summary>
        /// Obrisi komisiju
        /// </summary>
        /// <param name="komisijaId">Jedinstevni identifikator komisije</param>
        /// <param name="key">Authorization Key Value</param>
        /// <remarks>
        /// Example of request \
        /// DELETE 'https://localhost:44306/api/komisije/'\
        ///  --header 'key: Bearer DunjaZamaklar' \
        ///  --param  'komisijaId = 4E1F1F8D-A8F7-44B1-9ABD-1C1EE122628D'\
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
            [HttpDelete("{komisijaId}")]
            public IActionResult DeleteKomisija(Guid komisijaId,  [FromHeader] string key)
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

                    var komisja = komisijaRepository.GetKomisijaById(komisijaId);

                    if (komisja == null)
                    {
                        return StatusCode(StatusCodes.Status404NotFound, "Nema komisije!");
                    }

                    List<LicnostKomisije> licnostKomisije = licnostKomisijeRepository.GetLicnosiKomisijeByOznakaKomisije(komisja.oznakaKomisije);
                    if(licnostKomisije!=null && licnostKomisije.Count>0)
                    {
                        foreach(LicnostKomisije lk in licnostKomisije)
                        {
                         licnostKomisijeRepository.DeleteLicnostKomisije(lk.licnostKomisijeId);
                        }
                    licnostKomisijeRepository.SaveChanges();
                    }
                    komisijaRepository.DeleteKomsija(komisijaId);
                    komisijaRepository.SaveChanges();

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
            /// OPTIONS 'https://localhost:44395/api/komisije'
            /// </remarks>
            [ProducesResponseType(StatusCodes.Status200OK)]
            [HttpOptions]
            [AllowAnonymous]
            public IActionResult GetKomisijaOptions()
            {
                Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
                return Ok();
            } 

    
        
    }
}
