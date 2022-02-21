using AutoMapper;
using licitacijaService.Auth;
using licitacijaService.Data;
using licitacijaService.DTOs;
using licitacijaService.DTOs.Mock;
using licitacijaService.Entities;
using licitacijaService.Logger;
using licitacijaService.ServiceCalls;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace licitacijaService.Controllers
{
    /// <summary>
    /// Licitacija controller sa CRUD endpointima
    /// </summary>
    [ApiController]
    [Route("api/licitacije")]
    [Produces("application/json", "application/xml")]
    public class LicitacijaController : ControllerBase
    {
        private readonly ILicitacijaRepository licitacijaRepository;
        private readonly ILicitacijaDokumentRepository licitacijaDokumentRepository;
        private readonly IJavnoNadmetanjeService javnoNadmetanjeService;
        private readonly IKomisijaService komisijaService;
        private readonly IMapper mapper;
        private readonly LinkGenerator linkGenerator;
        private readonly ILoggerMockReposiotry logger;
        private readonly IHttpContextAccessor contextAccessor;

        private readonly IAuthHelper auth;

        public LicitacijaController(ILicitacijaRepository licitacijaRepository, ILicitacijaDokumentRepository licitacijaDokumentRepository,IJavnoNadmetanjeService javnoNadmetanjeService,IKomisijaService komisijaService,IMapper mapper, ILoggerMockReposiotry logger,
                                  LinkGenerator linkGenerator, IHttpContextAccessor contextAccessor, IAuthHelper auth)
        {
            this.licitacijaRepository = licitacijaRepository;
            this.licitacijaDokumentRepository = licitacijaDokumentRepository;
            this.javnoNadmetanjeService = javnoNadmetanjeService;
            this.komisijaService = komisijaService;
            this.mapper = mapper;
            this.linkGenerator = linkGenerator;
            this.logger = logger;
            this.contextAccessor = contextAccessor;
            this.auth = auth;
        }

        /// <summary>
        /// Vraca listu licitacija
        /// </summary>
        /// <param name="brojLicitacije">Broj licitacije</param>
        /// <returns>Lista svih licitacija</returns>
        /// <remarks> 
        /// Primer request-a \
        /// GET 'https://localhost:44306/api/licitacije' \
        /// </remarks>
        /// <response code="200">Success answer - return all licitacije</response>
        /// <response code="204">No content</response>
        /// <response code="500">Server error</response>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<Licitacija>> GetLicitacije(string brojLicitacije)
        {

            var licitacije = licitacijaRepository.GetLicitacije(brojLicitacije);

            if (licitacije == null || licitacije.Count == 0)
            {
                return NoContent();
            }
             

            foreach (Licitacija l in licitacije)
            {
                List<LicitacijaDokument> pravniDokuemnti = licitacijaDokumentRepository.GetDokumnetByLicitacijaIdAndVrstaPodnosioca(l.licitacijaId, "p");
                List<LicitacijaDokument> fizickiDokumneti = licitacijaDokumentRepository.GetDokumnetByLicitacijaIdAndVrstaPodnosioca(l.licitacijaId, "f");
                l.dokumentacijaFizickaLica = fizickiDokumneti;
                l.dokumnetacijaPravnaLica = pravniDokuemnti;
                List<JavnoNadmetanjeConfirmationDTO> javnaNadmetanja = javnoNadmetanjeService.GetJavnaNadmetanjaByLicitacijaId(l.licitacijaId).Result;
                l.javnaNadmetanja = javnaNadmetanja;
                List<KomisijaConfirmationDTO> komisije = komisijaService.GetKomisijaByOznaka(l.oznakaKomisije).Result;
                if (komisije != null && komisije.Count > 0)
                {
                    KomisijaConfirmationDTO komisija = komisijaService.GetKomisijaByOznaka(l.oznakaKomisije).Result.FirstOrDefault();
                    l.komisija = komisija;
                }
                mapper.Map<List<JavnoNadmetanjeConfirmationDTO>>(javnaNadmetanja);
                mapper.Map<List<LicitacijaVrstaDokumentaDTO>>(pravniDokuemnti);
                mapper.Map<List<LicitacijaVrstaDokumentaDTO>>(fizickiDokumneti);
            }
          

            logger.Log(LogLevel.Information, contextAccessor.HttpContext.TraceIdentifier, "", "Get sve licitacije", null);
            return Ok(mapper.Map<List<LicitacijaConfirmationDTO>>(licitacije));

        }

        /// <summary>
        /// Vraca licitaciju sa specificiranim licitacijaId
        /// </summary>
        /// <param name="licitacijaId">Jedinstevni identifikator licitacije</param>
        /// <remarks>    
        /// Primer request-a \
        /// GET 'https://localhost:44306/api/licitacije/' \
        ///     --param  'licitacijaId = 4E1F1F8D-A8F7-44B1-9BDA-1C1EE122628D'
        /// </remarks>
        /// <response code="200">Success - vraca licitaciju sa vrednosti identifikatora licitacijaId</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Server error</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{licitacijaId}")]
        [AllowAnonymous]
        public ActionResult<Licitacija> GetLicitacijaById(Guid licitacijaId)
        {

            var licitacija = licitacijaRepository.GetLicitacijaById(licitacijaId);

            if (licitacija == null)
            {
                return NotFound();
            }
            List<LicitacijaDokument> pravniDokuemnti = licitacijaDokumentRepository.GetDokumnetByLicitacijaIdAndVrstaPodnosioca(licitacijaId, "p");
            List<LicitacijaDokument> fizickiDokumneti = licitacijaDokumentRepository.GetDokumnetByLicitacijaIdAndVrstaPodnosioca(licitacijaId, "f");
            licitacija.dokumentacijaFizickaLica = fizickiDokumneti;
            licitacija.dokumnetacijaPravnaLica = pravniDokuemnti;
            List<JavnoNadmetanjeConfirmationDTO> javnaNadmetanja = javnoNadmetanjeService.GetJavnaNadmetanjaByLicitacijaId(licitacija.licitacijaId).Result;
            licitacija.javnaNadmetanja = javnaNadmetanja;
            List<KomisijaConfirmationDTO> komisije = komisijaService.GetKomisijaByOznaka(licitacija.oznakaKomisije).Result;
            if (komisije != null && komisije.Count > 0)
            {
                KomisijaConfirmationDTO komisija = komisijaService.GetKomisijaByOznaka(licitacija.oznakaKomisije).Result.FirstOrDefault();
                licitacija.komisija = komisija;
            }
            mapper.Map<List<JavnoNadmetanjeConfirmationDTO>>(javnaNadmetanja);
            mapper.Map<List<LicitacijaVrstaDokumentaDTO>>(pravniDokuemnti);
            mapper.Map<List<LicitacijaVrstaDokumentaDTO>>(fizickiDokumneti);

            logger.Log(LogLevel.Information, contextAccessor.HttpContext.TraceIdentifier, "", "Get licitacija by licitacijaId", null);
            return Ok(mapper.Map<LicitacijaConfirmationDTO>(licitacija));

        }

        /// <summary>
        /// Dodaj novu licitaciju
        /// </summary>
        /// <param name="LicitacijaCreationDto">Model licitacije</param>
        /// <param name="key">Authorization Key Value</param>
        /// <remarks>
        /// Primer request-a \
        /// POST 'https://localhost:44306/api/licitacije'\
        ///     --header 'key: Bearer DunjaZamaklar' \
        /// Example: \
        /// { \
        ///          brojLicitacije = 1,
        ///          goidna = 2019,
        ///          ogranicenjeLicitacije = 1,
        ///          oznakaKomisije = "kom123ef",
        ///          korakCene = 1,
        ///          datumLicitacije = "2019-01-02",
        ///          rokZaDostavuPrijava = 2019-02-22"
        ///}
        /// </remarks>
        /// <response code="201">Success - vraca kreiranu licitaciju</response>
        /// <response code="401">Unauthorized user</response>
        /// <response code="500">Server error</response>
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public ActionResult<LicitacijaConfirmationDTO> CreateLicitacija([FromBody] LicitacijaCreationDTO licitacijaCreationDTO, [FromHeader] string key)
        {
            try
            {
                if(key == null)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "Authorization failed!");
                }
                if (!auth.AuthorizeUser(key))
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "Authorization failed!");
                }
                List<KomisijaConfirmationDTO> komisije = komisijaService.GetKomisijaByOznaka(licitacijaCreationDTO.oznakaKomisije).Result;
                if (komisije == null || komisije.Count<=0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Neispravna oznaka komisije!");
                }
                Licitacija licitacija = mapper.Map<Licitacija>(licitacijaCreationDTO);
                licitacijaRepository.CreateLicitacija(licitacija);
                licitacijaRepository.SaveChanges();
                licitacija.komisija = komisije.FirstOrDefault();

                logger.Log(LogLevel.Information, contextAccessor.HttpContext.TraceIdentifier, "", "Kreirana nova licitacija", null);

                string location = linkGenerator.GetPathByAction("GetLicitacijaById", "Licitacija", new { licitacijaId = licitacija.licitacijaId });

                return Created(location, mapper.Map<LicitacijaConfirmationDTO>(licitacija));
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, contextAccessor.HttpContext.TraceIdentifier, "", "Greska prilikom kreiranja licitacije", null);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        /// <summary>
        /// Update licitaciju
        /// </summary>
        /// <param name="lictacijaUpdateDTO">Model licitacije</param>
        /// <param name="licitacijaId">jedinstevni identifikator licitacije</param>
        /// <param name="key">Authorization Key Value</param>
        /// <remarks>
        /// Primer request-a \
        /// PUT 'https://localhost:44306/api/licitacije/'\
        ///  --header 'key: Bearer DunjaZamaklar' \
        ///  --param  'licitacijaId = 4E1F1F8D-A8F7-44B1-9BDA-1C1EE122628D\
        /// Example: \
        /// { \
        ///         brojLicitacije = 1,
        ///          goidna = 2019,
        ///          ogranicenjeLicitacije = 1,
        ///          oznakaKomisije = "kom123ef",
        ///          korakCene = 1,
        ///          datumLicitacije = "2019-01-02",
        ///          rokZaDostavuPrijava = "2019-02-22"
        /// } \
        /// </remarks>
        /// <response code="200">Success answer - update-ovana licitacija</response>
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
        [HttpPut("{licitacijaId}")]
        public ActionResult<LicitacijaConfirmationDTO> UpdateLicitacija([FromBody] LicitacijaUpdateDTO licitacijaUpdateDTO, Guid licitacijaId, [FromHeader] string key)
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


                var oldLicitacija = licitacijaRepository.GetLicitacijaById(licitacijaId);
                if (oldLicitacija == null)
                {
                    return NotFound();
                }
                Licitacija newLicitacija = mapper.Map<Licitacija>(licitacijaUpdateDTO);
                newLicitacija.licitacijaId = licitacijaId;
                List<KomisijaConfirmationDTO> komisije = komisijaService.GetKomisijaByOznaka(licitacijaUpdateDTO.oznakaKomisije).Result;
                if (komisije == null || komisije.Count <= 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Neispravna oznaka komisije!");
                }
                licitacijaRepository.UpdateLicitacija(oldLicitacija, newLicitacija);

                licitacijaRepository.SaveChanges();
                logger.Log(LogLevel.Information, contextAccessor.HttpContext.TraceIdentifier, "", "Licitacija je update-ovana", null);
                oldLicitacija.komisija = komisije.FirstOrDefault();
                return Ok(mapper.Map<LicitacijaConfirmationDTO>(oldLicitacija));
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, contextAccessor.HttpContext.TraceIdentifier, "", "Update error", null);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Obrisi licitaciju
        /// </summary>
        /// <param name="liictacijaId">Jedinstevni identifikator liictacije</param>
        /// <param name="key">Authorization Key Value</param>
        /// <remarks>
        /// Example of request \
        /// DELETE 'https://localhost:44306/api/licitacije/'\
        ///  --header 'key: Bearer DunjaZamaklar' \
        ///  --param  'liictacijaId = 4E1F1F8D-A8F7-44B1-9BDA-1C1EE122628D'\
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
     [HttpDelete("{licitacijaId}")]
     public IActionResult DeleteKomisija(Guid licitacijaId, [FromHeader] string key)
     {
         try
         {
            if(key == null)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "Authorization failed!");
                }
             if (!auth.AuthorizeUser(key))
             {
                 return StatusCode(StatusCodes.Status401Unauthorized, "Authorization failed!");
             }

             var licitacija = licitacijaRepository.GetLicitacijaById(licitacijaId);

             if (licitacija == null)
             {
                 return StatusCode(StatusCodes.Status404NotFound, "Nema licitacije!");
             }

                licitacijaDokumentRepository.DeleteLicitacijaDokumentByLicitacijaId(licitacijaId);
                licitacijaDokumentRepository.SaveChanges();
                licitacijaRepository.DeleteLicitacija(licitacijaId);
                licitacijaRepository.SaveChanges();

             return NoContent();
         }

         catch (Exception ex)
         {
             logger.Log(LogLevel.Error, contextAccessor.HttpContext.TraceIdentifier, "", "Delete error", null);
             return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
         }
     }

     /// <summary>
     /// Vraca implementirane opcije rada sa lictacijom
     /// </summary>
     /// <returns></returns>
     /// <remarks>
     /// Example of request
     /// OPTIONS 'https://localhost:44395/api/licitacije'
     /// </remarks>
     [ProducesResponseType(StatusCodes.Status200OK)]
     [HttpOptions]
     [AllowAnonymous]
     public IActionResult GetLicitacijaOptions()
     {
         Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
         return Ok();
     }

       
        
    }
}
