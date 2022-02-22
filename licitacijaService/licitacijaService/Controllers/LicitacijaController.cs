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
        private readonly IDokumentService dokumentService;
        private readonly IMapper mapper;
        private readonly LinkGenerator linkGenerator;
        private readonly LoggerService logger;

        private readonly IAuthHelper auth;

        public LicitacijaController(ILicitacijaRepository licitacijaRepository, ILicitacijaDokumentRepository licitacijaDokumentRepository,IJavnoNadmetanjeService javnoNadmetanjeService,IKomisijaService komisijaService,IMapper mapper, 
                                  IDokumentService dokumentService,LinkGenerator linkGenerator, IAuthHelper auth)
        {
            this.licitacijaRepository = licitacijaRepository;
            this.licitacijaDokumentRepository = licitacijaDokumentRepository;
            this.javnoNadmetanjeService = javnoNadmetanjeService;
            this.komisijaService = komisijaService;
            this.dokumentService = dokumentService;
            this.mapper = mapper;
            this.linkGenerator = linkGenerator;
            this.auth = auth;
            this.logger = new LoggerService();
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
            #pragma warning disable CS4014
            logger.PostLogger("Pristup svim licitacijama." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
            #pragma warning restore CS4014
            var licitacije = licitacijaRepository.GetLicitacije(brojLicitacije);

            if (licitacije.Count == 0)
            {
                return NoContent();
            }
            string accessToken = HttpContext.GetTokenAsync("access_token").Result;

            foreach (Licitacija l in licitacije)
            {
                List<LicitacijaDokument> pravniDokuemnti = licitacijaDokumentRepository.GetDokumnetByLicitacijaIdAndVrstaPodnosioca(l.licitacijaId, "p");
                List<LicitacijaDokument> fizickiDokumneti = licitacijaDokumentRepository.GetDokumnetByLicitacijaIdAndVrstaPodnosioca(l.licitacijaId, "f");
                l.dokumentacijaFizickaLica = fizickiDokumneti;
                l.dokumnetacijaPravnaLica = pravniDokuemnti;
                if (fizickiDokumneti.Count > 0)
                {
                    foreach (var dokp in fizickiDokumneti)
                    {
                        dokp.dokument = dokumentService.GetDokumentByDokumentId(dokp.dokumentId, accessToken).Result;
                    }
                }

                if (pravniDokuemnti.Count > 0)
                {
                    foreach (var dokp in pravniDokuemnti)
                    {
                        dokp.dokument = dokumentService.GetDokumentByDokumentId(dokp.dokumentId, accessToken).Result;
                    }
                }
                List<JavnoNadmetanjeConfirmationDto> javnaNadmetanja = javnoNadmetanjeService.GetJavnaNadmetanjaByLicitacijaId(l.licitacijaId).Result;
                l.javnaNadmetanja = javnaNadmetanja;
                List<KomisijaConfirmationDto> komisije = komisijaService.GetKomisijaByOznaka(l.oznakaKomisije).Result;
                if (komisije != null && komisije.Count > 0)
                {
                    KomisijaConfirmationDto komisija = komisijaService.GetKomisijaByOznaka(l.oznakaKomisije).Result.FirstOrDefault();
                    l.komisija = komisija;
                }
                mapper.Map<List<JavnoNadmetanjeConfirmationDto>>(javnaNadmetanja);
                mapper.Map<List<LicitacijaVrstaDokumentaDto>>(pravniDokuemnti);
                mapper.Map<List<LicitacijaVrstaDokumentaDto>>(fizickiDokumneti);
            }
            return Ok(mapper.Map<List<LicitacijaConfirmationDto>>(licitacije));

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
            #pragma warning disable CS4014
            logger.PostLogger("Pristup licitaciji po id." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
            #pragma warning restore CS4014
            var licitacija = licitacijaRepository.GetLicitacijaById(licitacijaId);

            if (licitacija == null)
            {
                return NotFound();
            }
            var dokumentaLicitacije = licitacijaDokumentRepository.GetDokumnetByLicitacijaId(licitacijaId);
            string accessToken = HttpContext.GetTokenAsync("access_token").Result;
            List<LicitacijaDokument> pravniDokuemnti = licitacijaDokumentRepository.GetDokumnetByLicitacijaIdAndVrstaPodnosioca(licitacijaId, "p");
            List<LicitacijaDokument> fizickiDokumneti = licitacijaDokumentRepository.GetDokumnetByLicitacijaIdAndVrstaPodnosioca(licitacijaId, "f");

            if (fizickiDokumneti.Count > 0)
            {
                foreach (var dokp in fizickiDokumneti)
                {
                    dokp.dokument = dokumentService.GetDokumentByDokumentId(dokp.dokumentId, accessToken).Result;
                }
            }

            if (pravniDokuemnti.Count > 0)
            {
                foreach (var dokp in pravniDokuemnti)
                {
                    dokp.dokument = dokumentService.GetDokumentByDokumentId(dokp.dokumentId, accessToken).Result;
                }
            }
            licitacija.dokumentacijaFizickaLica = fizickiDokumneti;
            licitacija.dokumnetacijaPravnaLica = pravniDokuemnti;
            
            List<JavnoNadmetanjeConfirmationDto> javnaNadmetanja = javnoNadmetanjeService.GetJavnaNadmetanjaByLicitacijaId(licitacija.licitacijaId).Result;
            licitacija.javnaNadmetanja = javnaNadmetanja;
            List<KomisijaConfirmationDto> komisije = komisijaService.GetKomisijaByOznaka(licitacija.oznakaKomisije).Result;
            if (komisije != null && komisije.Count > 0)
            {
                KomisijaConfirmationDto komisija = komisijaService.GetKomisijaByOznaka(licitacija.oznakaKomisije).Result.FirstOrDefault();
                licitacija.komisija = komisija;
            }
            mapper.Map<List<JavnoNadmetanjeConfirmationDto>>(javnaNadmetanja);
            mapper.Map<List<LicitacijaVrstaDokumentaDto>>(pravniDokuemnti);
            mapper.Map<List<LicitacijaVrstaDokumentaDto>>(fizickiDokumneti);
            return Ok(mapper.Map<LicitacijaConfirmationDto>(licitacija));

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
        public ActionResult<LicitacijaConfirmationDto> CreateLicitacija([FromBody] LicitacijaCreationDto licitacijaCreationDTO, [FromHeader] string key)
        {
            try
            {
                #pragma warning disable CS4014
                logger.PostLogger("Dodavanje nove licitacije." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
                #pragma warning restore CS4014
                if (key == null)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "Authorization failed!");
                }
                if (!auth.AuthorizeUser(key))
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "Authorization failed!");
                }
                List<KomisijaConfirmationDto> komisije = komisijaService.GetKomisijaByOznaka(licitacijaCreationDTO.oznakaKomisije).Result;
                if (komisije == null || komisije.Count<=0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Neispravna oznaka komisije!");
                }
                Licitacija licitacija = mapper.Map<Licitacija>(licitacijaCreationDTO);
                licitacijaRepository.CreateLicitacija(licitacija);
                licitacijaRepository.SaveChanges();
                licitacija.komisija = komisije.FirstOrDefault();
                string location = linkGenerator.GetPathByAction("GetLicitacijaById", "Licitacija", new { licitacijaId = licitacija.licitacijaId });

                return Created(location, mapper.Map<LicitacijaConfirmationDto>(licitacija));
            }
            catch (Exception ex)
            {
                #pragma warning disable CS4014
                logger.PostLogger("Greska prilikom dodvanja nove licitacije." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
                #pragma warning restore CS4014
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
        public ActionResult<LicitacijaConfirmationDto> UpdateLicitacija([FromBody] LicitacijaUpdateDto licitacijaUpdateDTO, Guid licitacijaId, [FromHeader] string key)
        {
            try
            {
                #pragma warning disable CS4014
                logger.PostLogger("Azuriranje licitacije." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
                #pragma warning restore CS4014
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
                List<KomisijaConfirmationDto> komisije = komisijaService.GetKomisijaByOznaka(licitacijaUpdateDTO.oznakaKomisije).Result;
                if (komisije == null || komisije.Count <= 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Neispravna oznaka komisije!");
                }
                licitacijaRepository.UpdateLicitacija(oldLicitacija, newLicitacija);

                licitacijaRepository.SaveChanges();
                oldLicitacija.komisija = komisije.FirstOrDefault();
                return Ok(mapper.Map<LicitacijaConfirmationDto>(oldLicitacija));
            }
            catch (Exception ex)
            {
                #pragma warning disable CS4014
                logger.PostLogger("Greska prilikom azuriranja licitacije." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
                #pragma warning restore CS4014
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
                #pragma warning disable CS4014
                logger.PostLogger("Brisanje licitacije." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
                #pragma warning restore CS4014
                if (key == null)
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
                #pragma warning disable CS4014
                logger.PostLogger("Greska prilikom brisanja licitacije." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
                #pragma warning restore CS4014
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
