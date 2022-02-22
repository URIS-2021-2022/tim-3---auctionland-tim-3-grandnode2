using AutoMapper;
using JavnoNadmetanje.Auth;
using JavnoNadmetanje.Data;
using JavnoNadmetanje.Entities;
using JavnoNadmetanje.Logger;
using JavnoNadmetanje.Models;
using JavnoNadmetanje.Models.KupacService;
using JavnoNadmetanje.Models.ParcelaService;
using JavnoNadmetanje.ServiceCalls;
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
    [Route("api/javnaNadmetanja")]
    [Consumes("application/json")]
    [Produces("application/json", "application/xml")]
    public class JavnoNadmetanjeController : ControllerBase
    {
        private readonly IJavnoNadmetanjeRepository javnoNadmetanjeRepository;
        private readonly IParcelaService parcelaService;
        private readonly IKupacService kupacService;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly IAuthService authService;
        private readonly LoggerService logger;

        public JavnoNadmetanjeController(IJavnoNadmetanjeRepository javnoNadmetanjeRepository, IParcelaService parcelaService, IKupacService kupacService, LinkGenerator linkGenerator, IMapper mapper, IAuthService authService)
        {
            this.javnoNadmetanjeRepository = javnoNadmetanjeRepository;
            this.parcelaService = parcelaService;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.authService = authService;
            this.kupacService = kupacService;
            logger = new LoggerService();
        }

        /// <summary>
        /// Vraća sva javna nadmetanja
        /// </summary>
        /// <returns>Lista javnih nadmetanja</returns>
        /// <response code = "200">Vraća listu javnih nadmetanja</response>
        /// <response code = "204">Ne postoji nijedno javno nadmetanje</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [AllowAnonymous]
        public ActionResult<List<JavnoNadmetanjeDto>> GetJavnaNadmetanja()
        {
            #pragma warning disable CS4014
            logger.PostLogger("Pristup svim javnim nadmetanjima." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
            #pragma warning restore CS4014

            List<JavnoNadmetanjeEntity> javnaNadmetanja = javnoNadmetanjeRepository.GetJavnaNadmetanja();

            if (javnaNadmetanja == null || javnaNadmetanja.Count == 0)
            {
                return NoContent();
            }

            foreach (JavnoNadmetanjeEntity j in javnaNadmetanja)
            {
                List<DeoParceleDto> deloviParcele = parcelaService.GetDeloveParcele(j.ParcelaId).Result;
                KupacDto kupac = kupacService.GetKupacById(j.KupacId).Result;
                j.DeloviParcele = deloviParcele;
                j.Kupac = kupac;
    
            }

            return Ok(mapper.Map<List<JavnoNadmetanjeDto>>(javnaNadmetanja));
        }

        /// <summary>
        /// Vraća traženo javno nadmetanje po ID-ju
        /// </summary>
        /// <param name="javnoNadmetanjeId">ID javnog nadmetanja</param>
        /// <returns>Traženo javno nadmetanje</returns>
        /// <response code = "200">Vraća traženo javno nadmetanje</response>
        /// <response code = "404">Nije pronađeno traženo javno nadmetanje</response>
        [HttpGet("{javnoNadmetanjeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public ActionResult<JavnoNadmetanjeDto> GetJavnoNadmetanjeById(Guid javnoNadmetanjeId)
        {
            #pragma warning disable CS4014
            logger.PostLogger("Pristup javnom nadmetanju putem ID-ja." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
            #pragma warning restore CS4014

            JavnoNadmetanjeEntity javnoNadmetanje = javnoNadmetanjeRepository.GetJavnoNadmetanjeById(javnoNadmetanjeId);
            
            if(javnoNadmetanje == null)
            {
                return NotFound();
            }

            List<DeoParceleDto> deloviParcele = parcelaService.GetDeloveParcele(javnoNadmetanje.ParcelaId).Result;
            KupacDto kupac = kupacService.GetKupacById(javnoNadmetanje.KupacId).Result;
            javnoNadmetanje.DeloviParcele = deloviParcele;
            javnoNadmetanje.Kupac = kupac;

            return Ok(mapper.Map<JavnoNadmetanjeDto>(javnoNadmetanje));
        }

        /// <summary>
        /// Vraća listu javnih nadmetanja po ID-ju licitacije
        /// </summary>
        /// <param name="licitacijaId">ID licitacije</param>
        /// <returns>Lista javnih nadmetanja za prosleđenu licitaciju</returns>
        /// <response code = "200">Vraća listu javnih nadmetanja za prosleđenu licitaciju</response>
        /// <response code = "204">Ne postoji lista javnih nadmetanja za prosleđenu licitaciju</response>
        [HttpGet("/JavnaNadmetanjaLicitacija/{licitacijaId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public ActionResult<List<JavnoNadmetanjeDto>> GetJavnaNadmetanjaByLicitacijaId(Guid licitacijaId)
        {
            #pragma warning disable CS4014
            logger.PostLogger("Pristup javnim nadmetanjima putem ID-ja licitacije." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
            #pragma warning restore CS4014

            List<JavnoNadmetanjeEntity> javnaNadmetanja = javnoNadmetanjeRepository.GetJavnaNadmetanjaByLicitacijaId(licitacijaId);

            if (javnaNadmetanja == null || javnaNadmetanja.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<JavnoNadmetanjeDto>>(javnaNadmetanja));
        }

        /// <summary>
        /// Kreira novo javno nadmetanje
        /// </summary>
        /// <param name="javnoNadmetanje"> model javnog nadmetanja</param>
        /// <param name="key"> ključ sa kojim se proverava autorizacija(key vrednost: Bearer LenkaSubotin)</param>
        /// <returns>Potvrda o kreiranom javnom nadmetanju</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje novog javnog nadmetanja \
        /// POST /api/javnaNadmetanja \
        /// { \
        ///  "Datum" : "2022-02-10", \
        ///  "VremePocetka" : "9:00:00", \
        ///  "VremeKraja" : "13:00:00", \
        ///  "PocetnaCenaPoHektaru" : 2000, \
        ///  "Izuzeto" : false, \
        ///  "Tip" : "JavnaLicitacija", \
        ///  "IzlicitiranaCena" : 1500, \
        ///  "PeriodZakupa" : 3, \
        ///  "BrojUcesnika : 25", \
        ///  "VisinaDopuneDepozita" : 500, \
        ///  "Krug" : 1, \
        ///  "Status" : "PrviKrug", \
        ///   "OglasId" : "382e1636-2705-477e-95c4-8727e819c5e9", \
        ///   "LicitacijaId" : "861e7d2e-268f-495f-8bd3-dbfb4f0594a4", \
        ///   "ParcelaId: : "35d3c2da-7e55-4730-a4ed-9f886e24e6f9", \
        ///   "KupacId" : "bc03a6fb-b322-4797-b6c4-0a899615f653" \
        /// } 
        /// </remarks>
        /// <response code = "201">Vraća kreirano javno nadmetanje</response>
        /// <response code="401">Lice koje želi da izvrši kreiranje javnog nadmetanja nije autorizovani korisnik</response>
        /// <response code = "500">Došlo je do greške na serveru prilikom kreiranja javnog nadmetanja</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<JavnoNadmetanjeDto> CreateJavnoNadmetanje([FromBody] JavnoNadmetanjeCreateDto javnoNadmetanje, [FromHeader] string key)
        {
            #pragma warning disable CS4014
            logger.PostLogger("Kreiranje novog javnog nadmetanja." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
            #pragma warning restore CS4014

            if (!authService.Authorize(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Korisnik nije autorizovan!");
            }

            try
            {
                bool modelValid = ValidationJavnoNadmetanje(javnoNadmetanje);

                if(!modelValid)
                {
                    return BadRequest("Vreme kraja javnog nadmetanja mora biti nakon vremena pocetka javnog nadmetanja!");
                }

                JavnoNadmetanjeEntity jNadmetanje = mapper.Map<JavnoNadmetanjeEntity>(javnoNadmetanje);
                JavnoNadmetanjeEntity jNadmetanje1 = javnoNadmetanjeRepository.CreateJavnoNadmetanje(jNadmetanje);
                javnoNadmetanjeRepository.SaveChanges();
                string location = linkGenerator.GetPathByAction("GetJavnaNadmetanja", "JavnoNadmetanje", new { javnoNadmetanjeId = jNadmetanje.JavnoNadmetanjeId });
                return Created(location, mapper.Map<JavnoNadmetanjeDto>(jNadmetanje1)); 
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska prilikom kreiranja javnog nadmetanja!");
            }
        }

        /// <summary>
        /// Briše javno nadmetanje na osnovu ID-ja
        /// </summary>
        /// <param name="javnoNadmetanjeId">ID javnog nadmetanja</param>
        /// /// <param name="key"> ključ sa kojim se proverava autorizacija(key vrednost: Bearer LenkaSubotin)</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Javno nadmetanja uspešno obrisano</response>
        /// <response code="401">Lice koje želi da izvrši brisanje nije autorizovani korisnik</response>
        /// <response code="404">Nije pronađeno javno nadmetanje za brisanje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom brisanja javnog nadmetanja</response>
        [HttpDelete("{javnoNadmetanjeId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult DeleteJavnoNadmetanje(Guid javnoNadmetanjeId, [FromHeader] string key)
        {
            #pragma warning disable CS4014
            logger.PostLogger("Brisanje postojećeg javnog nadmetanja." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
            #pragma warning restore CS4014

            if (!authService.Authorize(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Korisnik nije autorizovan!");
            }

            try
            {
                JavnoNadmetanjeEntity javnoNadmetanje = javnoNadmetanjeRepository.GetJavnoNadmetanjeById(javnoNadmetanjeId);
                if (javnoNadmetanje == null)
                {
                    return NotFound();
                }

                javnoNadmetanjeRepository.DeleteJavnoNadmetanje(javnoNadmetanjeId);
                javnoNadmetanjeRepository.SaveChanges();
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska prilikom brisanja javnog nadmetanja!");
            }
        }

        /// <summary>
        /// Ažurira jedno javno nadmetanje
        /// </summary>
        /// <param name="javnoNadmetanje">Model javnog nadmetanja koje se ažurira</param>
        /// <param name="key"> ključ sa kojim se proverava autorizacija(key vrednost: Bearer LenkaSubotin)</param>
        /// <returns>Potvrda o ažuriranom javnom nadmetanju</returns>
        /// <response code="200">Vraća ažurirano javno nadmetanje</response>
        /// <response code="401">Lice koje želi da izvrši ažuriranje nije autorizovani korisnik</response>
        /// <response code="404">Nije pronađeno javno nadmetanje za ažuriranje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom ažuriranja javnog nadmetanja</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<JavnoNadmetanjeDto> UpdateJavnoNadmetanje(JavnoNadmetanjeEntity javnoNadmetanje, [FromHeader] string key)
        {
            #pragma warning disable CS4014
            logger.PostLogger("Modifikacija postojećeg javnog nadmetanja." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
            #pragma warning restore CS4014

            if (!authService.Authorize(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Korisnik nije autorizovan!");
            }

            try
            {
                var oldJavnoNadmetanje = javnoNadmetanjeRepository.GetJavnoNadmetanjeById(javnoNadmetanje.JavnoNadmetanjeId);

                if (oldJavnoNadmetanje == null)
                {
                    return NotFound();
                }

                JavnoNadmetanjeEntity jNadmetanje = mapper.Map<JavnoNadmetanjeEntity>(javnoNadmetanje);
                mapper.Map(jNadmetanje, oldJavnoNadmetanje);
                javnoNadmetanjeRepository.SaveChanges();

                return Ok(mapper.Map<JavnoNadmetanjeDto>(jNadmetanje));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska prilikom azuriranja javnog nadmetanja!");
            }
        }

        private bool ValidationJavnoNadmetanje(JavnoNadmetanjeCreateDto javnoNadmetanje)
         {
             if(javnoNadmetanje.VremePocetka > javnoNadmetanje.VremeKraja)
             {
                 return false;
             }

             return true;
         }

        /// <summary>
        /// Vraća informacije o opcijama koje je moguće izvršiti za sva javna nadmetanja
        /// </summary>
        /// <response code="200">Vraća informacije o opcijama koje je moguće izvršiti</response>
        [HttpOptions]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [AllowAnonymous]
        public IActionResult GetJavnoNadmetanjeOptions()
        {
            #pragma warning disable CS4014
            logger.PostLogger("Pristup opcijama za javno nadmetanje." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
            #pragma warning restore CS4014

            Response.Headers.Add("Allow", "GET, HEAD, POST, PUT, DELETE");
            return Ok();
        }
    }
}

