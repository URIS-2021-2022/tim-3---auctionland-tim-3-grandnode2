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
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrator, Prva komisija")]
    [ApiController]
    public class DokumentController : ControllerBase
    {
        private readonly HttpClient httpClient;
        private readonly DokumentService dokumentService;
        private readonly IConfiguration configuration;
        private readonly string KorisnikPath;
        private readonly string KupacPath;
        private readonly string LiciterPath;
        private readonly LoggerService logger;

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


        // GET: api/<DokumentController>
        [HttpGet]
        public IEnumerable<Dokument> Get()
        {
            logger.PostLogger("Pristup svim dokumentima." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
            return dokumentService.GetAll();
        }

        // GET api/<DokumentController>/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            logger.PostLogger("Pristup dokumentu putem id-a." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
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

        // POST api/<DokumentController>
        [HttpPost]
        public IActionResult Post([FromBody] Dokument model)
        {
            logger.PostLogger("Kreiranje novog dokumenta." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
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

        // PUT api/<DokumentController>/5
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] Dokument dokument)
        {
            logger.PostLogger("Modifikacija postojeceg dokumenta." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
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

        // DELETE api/<DokumentController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            logger.PostLogger("Brisanje postojeceg dokumenta." + "*********Korisnicko ime: " + HttpContext.User.Identity.Name);
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
                    if (k.KupacID == model.KupacID)
                    {
                        proveraKupca = true;
                    }
                }
                var responseLiciter = httpClient.GetAsync(requestUri: LiciterPath).Result;
                List<Liciter> liciteri = JsonConvert.DeserializeObject<List<Liciter>>(responseLiciter.Content.ReadAsStringAsync().Result);
                bool proveraLicitera = false;
                foreach (var k in liciteri)
                {
                    if (k.LiciterID == model.LiciterID)
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
