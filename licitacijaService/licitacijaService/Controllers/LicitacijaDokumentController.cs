using AutoMapper;
using licitacijaService.Auth;
using licitacijaService.Data;
using licitacijaService.DTOs;
using licitacijaService.Entities;
using licitacijaService.Logger;
using licitacijaService.ServiceCalls;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace licitacijaService.Controllers
{
    /// <summary>
    /// Licitacija dokumenti controller sa CRUD endpointima
    /// </summary>
    [ApiController]
    [Route("api/dokumentiLicitacije")]
    [Produces("application/json", "application/xml")]
    public class LicitacijaDokumentController : ControllerBase
    {
        private readonly ILicitacijaRepository licitacijaRepository;
        private readonly ILicitacijaDokumentRepository licitacijaDokumentRepository;
        private readonly IDokumentService dokumentService;
        private readonly IMapper mapper;
        private readonly LinkGenerator linkGenerator;
        private readonly LoggerService logger;

        private readonly IAuthHelper auth;

        public LicitacijaDokumentController(ILicitacijaRepository licitacijaRepository, ILicitacijaDokumentRepository licitacijaDokumentRepository, IDokumentService dokumentService ,IMapper mapper, 
                                  LinkGenerator linkGenerator, IAuthHelper auth)
        {
            this.licitacijaRepository = licitacijaRepository;
            this.licitacijaDokumentRepository = licitacijaDokumentRepository;
            this.dokumentService = dokumentService;
            this.mapper = mapper;
            this.linkGenerator = linkGenerator;
            this.auth = auth;
            this.logger = new LoggerService();
        }

        /// <summary>
        /// Vraca listu dokumenta licitacije sa specificiranim licitacijaId
        /// </summary>
        /// <param name="licitacijaId">Jedinstevni identifikator licitacije</param>
        /// <remarks>    
        /// Primer request-a \
        /// GET 'https://localhost:44306/api/dokumentiLicitacije/' \
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
        public ActionResult<LicitacijaDokumentConfirmationDto> GetDokumentiByLicitacijaId(Guid licitacijaId, string podnosilac)
        {
            #pragma warning disable CS4014
            logger.PostLogger("Pristup dokumentima licitacije." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
            #pragma warning restore CS4014
            var dokumentaLicitacije = licitacijaDokumentRepository.GetDokumnetByLicitacijaId(licitacijaId);
            string accessToken = HttpContext.GetTokenAsync("access_token").Result;
            if (podnosilac!=null)
            {
                
                if (!podnosilac.Equals("f") && !podnosilac.Equals("p"))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Bad Request! Wrong podnosilac value!");
                }
                dokumentaLicitacije = licitacijaDokumentRepository.GetDokumnetByLicitacijaIdAndVrstaPodnosioca(licitacijaId, podnosilac);
                
            }


            if (dokumentaLicitacije == null || dokumentaLicitacije.Count<1)
            {
                return NotFound();
            }

            foreach(var dok in dokumentaLicitacije)
            {
                dok.dokument = dokumentService.GetDokumentByDokumentId(dok.dokumentId,accessToken).Result;
            }
            return Ok(mapper.Map<List<LicitacijaDokumentConfirmationDto>>(dokumentaLicitacije));

        }


        /// <summary>
        /// Pridruzi novi dokument licitaciji
        /// </summary>
        /// <param name="LicitacijaDokumentCreationandUpdateDTO">Model licitacije i dokumenta</param>
        /// <param name="key">Authorization Key Value</param>
        /// <remarks>
        /// Primer request-a \
        /// POST 'https://localhost:44306/api/dokumnetiLicitacije'\
        ///     --header 'key: Bearer DunjaZamaklar' \
        /// Example: \
        /// { \
        ///          licitacijaId = 4E1F1F8D-A8F7-44B1-9BDA-1C1EE122628D,
        ///          dokuemntId = FE1F1F8D-B8F7-34B1-9BDA-1C1EE122628D,
        ///          vrstaPodnosiocaDokumenta = "p"
        ///}
        /// </remarks>
        /// <response code="201">Success - vraca kreiranu vezu licitacije i  dokuemnta</response>
        /// <response code="401">Unauthorized user</response>
        /// <response code="500">Server error</response>
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public ActionResult<LicitacijaDokument> CreateLicitacijaDokument([FromBody] LicitacijaDokumentCreationandUpdateDto licitacijaDokumentiCreationDTO, [FromHeader] string key)
        {
            try
            {
                #pragma warning disable CS4014
                logger.PostLogger("Pridruzivanje dokumenta licitaciji." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
                #pragma warning restore CS4014
                if (key == null)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "Authorization failed!");
                }
                if (!auth.AuthorizeUser(key))
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "Authorization failed!");
                }
                string accessToken = HttpContext.GetTokenAsync("access_token").Result;
                if(dokumentService.GetDokumentByDokumentId(licitacijaDokumentiCreationDTO.dokumentId,accessToken).Result==null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Bad Request! Wrong dokumentId value!");
                }
                LicitacijaDokument dokuemntiLicitacija = mapper.Map<LicitacijaDokument>(licitacijaDokumentiCreationDTO);
                Licitacija licitacija = licitacijaRepository.GetLicitacijaById(dokuemntiLicitacija.licitacijaId);
                if(licitacija==null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Bad Request! Wrong licitacijaId value!");
                }
                licitacijaDokumentRepository.CreateLicitacijaDokument(dokuemntiLicitacija);
                licitacijaDokumentRepository.SaveChanges();

                string location = linkGenerator.GetPathByAction("GetDokumentiByLicitacijaId", "LicitacijaDokument", new { licitacijaId = licitacija.licitacijaId });

                return Created(location, dokuemntiLicitacija);
            }
            catch (Exception ex)
            {
                #pragma warning disable CS4014
                logger.PostLogger("Greska prilikom pridruzivanja dokumenta licitaciji." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
                #pragma warning restore CS4014
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        /// <summary>
        /// Update dokument licitacija
        /// </summary>
        /// <param name="LicitacjaDokumnetCreationandUpdateDTO">Model licitacije i dokumenta</param>
        /// <param name="licitacijaId">jedinstevni identifikator licitacije</param>
        /// <param name="key">Authorization Key Value</param>
        /// <remarks>
        /// Primer request-a \
        /// PUT 'https://localhost:44306/api/dokumentiLicitacije/'\
        ///  --header 'key: Bearer DunjaZamaklar' \
        ///  --param  'licitacijaId = 3F8AA5B3-A67F-45B5-B518-771A7C09A944\
        /// Example: \
        /// { \
        ///         licitacijaId = 4E1F1F8D-A8F7-44B1-9BDA-1C1EE122628D,
        ///          dokuemntId = FE1F1F8D-B8F7-34B1-9BDA-1C1EE122628D,
        ///          vrstaPodnosiocaDokumenta = "p"
        /// } \
        /// </remarks>
        /// <response code="200">Success answer - update-ovan dokument-licitacija</response>
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
        [HttpPut()]
        public ActionResult<LicitacijaDokument> UpdateDokumentLicitacija([FromBody] LicitacijaDokumentCreationandUpdateDto licitacijaDokumentCreationDTO, Guid licitacijaId, Guid dokumentId, [FromHeader] string key)
        {
            try
            {
                #pragma warning disable CS4014
                logger.PostLogger("Azuriranje dokumenta licitacije." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
                #pragma warning restore CS4014
                if (dokumentId == Guid.Empty|| licitacijaId == Guid.Empty)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Bad Request! Provide dokuemntId and licitacijaId values!!");
                }
                if (key == null)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "Authorization failed!");
                }
                if (!auth.AuthorizeUser(key))
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "Authorization failed!");
                }


                var oldLD = licitacijaDokumentRepository.GetDokumnetById(licitacijaId,dokumentId);
                if (oldLD == null)
                {
                    return NotFound();
                }
                LicitacijaDokument newLD = mapper.Map<LicitacijaDokument>(licitacijaDokumentCreationDTO);
                Licitacija lic = licitacijaRepository.GetLicitacijaById( newLD.licitacijaId);
                if(lic==null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Bad Request! Wrong licitacijaId value!");
                }
                string accessToken = HttpContext.GetTokenAsync("access_token").Result;
                if (dokumentService.GetDokumentByDokumentId(newLD.dokumentId, accessToken).Result == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Bad Request! Wrong dokumentId value!");
                }
                licitacijaDokumentRepository.UpdateLicitacijaDokument(oldLD, newLD);

                licitacijaDokumentRepository.SaveChanges();
                return Ok(oldLD);
            }
            catch (Exception ex)
            {
                #pragma warning disable CS4014
                logger.PostLogger("Greska prilikom azuriranja dokumenta licitacije." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
                #pragma warning restore CS4014
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // <summary>
        /// Obrisi dokumenta licitacije
        /// </summary>
        /// <param name="lictacijaId">Jedinstevni identifikator liictacije</param>
        /// <param name="key">Authorization Key Value</param>
        /// <remarks>
        /// Example of request \
        /// DELETE 'https://localhost:44306/api/dokumentaLicitacije/'\
        ///  --header 'key: Bearer DunjaZamaklar' \
        ///  --param  'lictacijaId = 5DB187A7-D99D-4DB1-B843-7771448819A1'\
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
        public IActionResult DeleteDokumentLicitacije(Guid licitacijaId, [FromHeader] string key)
        {
            try
            {
                #pragma warning disable CS4014
                logger.PostLogger("Brisanje dokumenta licitacije." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
                #pragma warning restore CS4014
                if (key == null)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "Authorization failed!");
                }
                if (!auth.AuthorizeUser(key))
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "Authorization failed!");
                }

                var licitacijaDokumenta = licitacijaDokumentRepository.GetDokumnetByLicitacijaId(licitacijaId);

                if (licitacijaDokumenta == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Nema dokuemtata licitacije!");
                }

                licitacijaDokumentRepository.DeleteLicitacijaDokumentByLicitacijaId(licitacijaId);
                licitacijaDokumentRepository.SaveChanges();

                return NoContent();
            }

            catch (Exception ex)
            {
                #pragma warning disable CS4014
                logger.PostLogger("Greska prilikom brisanja dokumenta licitacije." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
                #pragma warning restore CS4014
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Vraca implementirane opcije rada sa lictacijom i dokumentom
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Example of request
        /// OPTIONS 'https://localhost:44395/api/licitacije'
        /// </remarks>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetLicitacijaDokumentOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }

    }
}
