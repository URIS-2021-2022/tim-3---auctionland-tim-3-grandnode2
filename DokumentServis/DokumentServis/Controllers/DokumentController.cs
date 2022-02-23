using DokumentServis.Database.Entities;
using DokumentServis.Logger;
using DokumentServis.Services;
using DokumentServis.VO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DokumentServis.Controllers
{
    /// <summary>
    /// Dokument controller pomocu kojeg se vrse sve potrebne funkcionalnosti iz specifikacije vezane za dokument
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrator, Prva komisija")]
    [ApiController]
    [Produces("application/json")]
    public class DokumentController : ControllerBase
    {
        private readonly HttpClient httpClient;
        private readonly DokumentService dokumentService;
        private readonly IConfiguration configuration;
        private readonly string KorisnikPath;
        private readonly string KupacPath;
        private readonly string LiciterPath;
        private readonly LoggerService logger;

        /// <summary>
        /// Dokument controller korisnik
        /// </summary>
        /// <param name="iconfiguration"></param>
        public DokumentController(IConfiguration iconfiguration)
        {
            dokumentService = new DokumentService();
            httpClient = new HttpClient();
            configuration = iconfiguration;
            KorisnikPath = configuration.GetValue<String>("Paths:Korisnik");
            KupacPath = configuration.GetValue<String>("Paths:Kupac");
            LiciterPath = configuration.GetValue<String>("Paths:Liciter");
            logger = new LoggerService();
        }


        /// <summary>
        /// Pristup svim dokumentima, koji omogucen od strane prethodno ulogovanog korisnika koji ima ulogu Administratora ili Prve komisije,
        /// uz logovanje navedene aktivnosti, kao i korisnickog imena korisnika koji je izvrsio tu aktivnost u okviru loggera
        /// </summary>
        /// <returns>Vraca listu svih dokumenata</returns>
        /// <response code = "200">Pristup svim dokumentima</response>
        // GET: api/<DokumentController>
        [HttpGet]
        public IEnumerable<Dokument> Get()
        {
            #pragma warning disable CS4014
            logger.PostLogger("Pristup svim dokumentima." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
            #pragma warning restore CS4014
            return dokumentService.GetAll();
        }

        /// <summary>
        /// Pristup dokumentu na osnovu zadatog id-a, od strane prethodno ulogovanog korisnika koji ima ulogu Administratora ili Prve komisije, 
        /// uz logovanje navedene aktivnosti, kao i korisnickog imena korisnika koji je izvrsio tu aktivnost u okviru loggera
        /// </summary>
        /// <param name="id">Id dokumenta</param>
        /// <returns>Vraca dokument ciji id je zadat u putanji</returns>
        /// <response code = "200">Dobijanje dokumenta na osnovu zadatog id-a</response>
        /// <response code = "404">Ne postoji dokument sa zadatim id-em</response>
        // GET api/<DokumentController>/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            #pragma warning disable CS4014
            logger.PostLogger("Pristup dokumentu putem id-a." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
            #pragma warning restore CS4014
            ResponseTemplateVO vo = new ResponseTemplateVO();
            Dokument dokument = dokumentService.GetById(id);
            if (dokument != null)
            {
                try
                {
                    var accessToken = HttpContext.GetTokenAsync("access_token");
                    httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken.Result}");
                    var responseKorisnik = httpClient.GetAsync(requestUri: KorisnikPath + dokument.KorisnikID).Result;
                    Korisnik korisnik = JsonConvert.DeserializeObject<Korisnik>(responseKorisnik.Content.ReadAsStringAsync().Result);
                    var responseKupac = httpClient.GetAsync(requestUri: KupacPath + dokument.KupacID).Result;
                    Kupac kupac = JsonConvert.DeserializeObject<Kupac>(responseKupac.Content.ReadAsStringAsync().Result);
                    var responseLiciter = httpClient.GetAsync(requestUri: LiciterPath + dokument.LiciterID).Result;
                    Liciter liciter = JsonConvert.DeserializeObject<Liciter>(responseLiciter.Content.ReadAsStringAsync().Result);
                    vo.dokument = dokument;
                    vo.korisnik = korisnik;
                    vo.kupac = kupac;
                    vo.liciter = liciter;
                    return StatusCode(StatusCodes.Status200OK, vo);
                }
                catch (Exception exp)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { message = "Korisnik or kupac service is down", error = exp.Message});
                }
                
            }
            return StatusCode(StatusCodes.Status404NotFound, new { message = "Dokument with this id: " + id + "doesnt exist" });
        }

        /// <summary>
        /// Kreiranje novog dokumenta, od strane prethodno ulogovanog korisnika koji ima ulogu Administratora ili Prve komisije
        /// uz logovanje navedene aktivnosti, kao i korisnickog imena korisnika koji je izvrsio tu aktivnost u okviru loggera
        /// </summary>
        /// <param name="model">Model dokumenta</param>
        /// <returns>Vraca novi dokument</returns>
        /// <remarks>
        /// Primer request-a za kreiranje novog dokumenta \
        /// POST api/Dokument/ \
        ///{
        ///     "zavodniBroj": "1313", \
        ///     "datum": "2019-06-24T00:00:00", \
        ///     "datumDonosenja": "2021-12-09T00:00:00", \
        ///     "sablon": "FontFamily: TimesNewRoman, FontSize: 12pt", \
        ///     "korisnikID": "bbc752be-d0b8-41f0-94e8-d54df388a9f0", \
        ///     "kupacID": "734e2747-d30f-4ddc-9d15-33fd6a036898", \
        ///     "liciterID": "fe650e15-966e-470e-a6f7-2932d0a2f2a2", \
        ///     "verzijaDokumentaID": "e51ecff3-0f88-4803-97fe-c853cae5fd99" \
        ///}
        /// </remarks>
        /// <response code = "201">Kreiran je novi dokument</response>
        /// <response code = "500">Greska prilikom pokusaja kreiranja dokumenta</response>
    // POST api/<DokumentController>
    [HttpPost]
        public IActionResult Post([FromBody] Dokument model)
        {
            #pragma warning disable CS4014
            logger.PostLogger("Kreiranje novog dokumenta." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
            #pragma warning restore CS4014
            try
            {
                if (!Provera(model))
                {
                 return StatusCode(StatusCodes.Status400BadRequest, new { message = "Korisnik or kupac or liciter doesnt exist" });
                }
                model.DokumentID = Guid.NewGuid();
                dokumentService.Save(model);
                return StatusCode(StatusCodes.Status201Created, model);
            }
            catch (Exception exp)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, exp);
            }
        }

        /// <summary>
        /// Modifikacija postojeceg dokumenta, od strane prethodno ulogovanog korisnika koji ima ulogu Administratora ili Prve komisije
        /// uz logovanje navedene aktivnosti, kao i korisnickog imena korisnika koji je izvrsio tu aktivnost u okviru loggera
        /// </summary>
        /// <param name="id">Parametar na osnovu kojeg se identifikuje dokument za azuriranje</param>
        /// <param name="dokument">Model novog dokumenta</param>
        /// <returns>Vraca modifikovan dokument</returns>
        /// <remarks>
        /// Primer request-a za modifikaciju dokumenta \
        /// PUT api/Dokument/dc37631c-78ae-4663-ba97-09ec6b1e5111 \
        ///{
        ///     "dokumentID": "dc37631c-78ae-4663-ba97-09ec6b1e5111", \
        ///     "zavodniBroj": "1313", \
        ///     "datum": "2019-06-24T00:00:00", \
        ///     "datumDonosenja": "2021-12-09T00:00:00", \
        ///     "sablon": "FontFamily: TimesNewRoman, FontSize: 12pt", \
        ///     "korisnikID": "bbc752be-d0b8-41f0-94e8-d54df388a9f0", \
        ///     "kupacID": "734e2747-d30f-4ddc-9d15-33fd6a036898", \
        ///     "liciterID": "fe650e15-966e-470e-a6f7-2932d0a2f2a2", \
        ///     "verzijaDokumentaID": "e51ecff3-0f88-4803-97fe-c853cae5fd99" \
        ///}
        /// </remarks>
        /// <response code = "200">Dobijanje modifikovanog dokumenta</response>
        /// <response code = "404">Ne postoji dokument sa zadatim id-em</response>
        // PUT api/<DokumentController>/5
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] Dokument dokument)
        {
            #pragma warning disable CS4014
            logger.PostLogger("Modifikacija postojeceg dokumenta." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
            #pragma warning restore CS4014
            if (id != dokument.DokumentID)
            {
                return BadRequest();
            }
            try
            {
                if (!Provera(dokument))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { message = "Korisnik or kupac or liciter doesnt exist" });
                }
                dokumentService.Update(dokument);
            }
            catch (Exception exp)
            {
                if (!dokumentService.DokumentExists(id))
                {
                    return StatusCode(StatusCodes.Status404NotFound, exp);
                }
                else
                {
                    throw;
                }
            }
            return StatusCode(StatusCodes.Status200OK, dokument);
        }

        /// <summary>
        /// Brisanje postojeceg dokumenta, od strane prethodno ulogovanog korisnika koji ima ulogu Administratora ili Prve komisije, 
        /// uz logovanje navedene aktivnosti, kao i korisnickog imena korisnika koji je izvrsio tu aktivnost u okviru loggera
        /// </summary>
        /// <param name="id">Parametar id-a dokumenta za koji se vrsi brisanje</param>
        /// <returns>Brise zadati dokument</returns>
        /// <response code = "200">Obrisan je dokument</response>
        /// <response code = "404">Ne postoji dokument za kojeg se izvrsava brisanje</response>
        // DELETE api/<DokumentController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            #pragma warning disable CS4014
            logger.PostLogger("Brisanje postojeceg dokumenta." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
            #pragma warning restore CS4014
            Dokument dokument = dokumentService.GetById(id);
            if (dokument == null)
            {
                return NotFound();
            }

            dokumentService.Delete(dokument);
            return Ok(dokument);
        }

        private bool Provera(Dokument model)
        {
            try
            {
                var accessToken = HttpContext.GetTokenAsync("access_token");
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken.Result}");
                var responseKorisnik = httpClient.GetAsync(requestUri: KorisnikPath).Result;
                List<Korisnik> korisnici = JsonConvert.DeserializeObject<List<Korisnik>>(responseKorisnik.Content.ReadAsStringAsync().Result);
                bool proveraKorisnika = false;
                foreach (var k in korisnici)
                {
                    if (k.KorisnikID == model.KorisnikID)
                    {
                        proveraKorisnika = true;
                    }
                }
                var responseKupac = httpClient.GetAsync(requestUri: KupacPath).Result;
                List<Kupac> kupci = JsonConvert.DeserializeObject<List<Kupac>>(responseKupac.Content.ReadAsStringAsync().Result);
                bool proveraKupca = false;
                foreach (var k in kupci)
                {
                    if (k.KupacId == model.KupacID)
                    {
                        proveraKupca = true;
                    }
                }
                var responseLiciter = httpClient.GetAsync(requestUri: LiciterPath).Result;
                List<Liciter> liciteri = JsonConvert.DeserializeObject<List<Liciter>>(responseLiciter.Content.ReadAsStringAsync().Result);
                bool proveraLicitera = false;
                foreach (var k in liciteri)
                {
                    if (k.KupacId == model.LiciterID)
                    {
                        proveraLicitera = true;
                    }
                }
                if (proveraKorisnika && proveraKupca && proveraLicitera)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
