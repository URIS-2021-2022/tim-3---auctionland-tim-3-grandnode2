using DokumentServis.Database.Entities;
using DokumentServis.Services;
using DokumentServis.VO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public static HttpClient httpClient;
        private readonly DokumentService dokumentService;

        public DokumentController()
        {
            dokumentService = new DokumentService();
            httpClient = new HttpClient();
        }


        // GET: api/<DokumentController>
        [HttpGet]
        public IEnumerable<Dokument> Get()
        {
            return dokumentService.GetAll();
        }

        // GET api/<DokumentController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            ResponseTemplateVO vo = new ResponseTemplateVO();
            Dokument dokument = dokumentService.GetById(id);
            if (dokument != null)
            {
                try
                {
                    var accessToken = HttpContext.GetTokenAsync("access_token");
                    httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken.Result}");
                    var responseKorisnik = httpClient.GetAsync(requestUri: "http://localhost:9901/api/Korisnik/" + dokument.KorisnikID).Result;
                    Korisnik korisnik = JsonConvert.DeserializeObject<Korisnik>(responseKorisnik.Content.ReadAsStringAsync().Result);
                    var responseKupac = httpClient.GetAsync(requestUri: "http://localhost:9904/api/Kupci/" + dokument.KupacID).Result;
                    Kupac kupac = JsonConvert.DeserializeObject<Kupac>(responseKupac.Content.ReadAsStringAsync().Result);
                    var responseLiciter = httpClient.GetAsync(requestUri: "http://localhost:9904/api/Liciteri/" + dokument.LiciterID).Result;
                    Liciter liciter = JsonConvert.DeserializeObject<Liciter>(responseLiciter.Content.ReadAsStringAsync().Result);
                    vo.dokument = dokument;
                    vo.korisnik = korisnik;
                    vo.kupac = kupac;
                    vo.liciter = liciter;
                    return StatusCode(StatusCodes.Status200OK, vo);
                }
                catch (Exception exp)
                {
                    return StatusCode(StatusCodes.Status200OK, new { message = "Korisnik or kupac service is down"});
                }
                
            }
            return StatusCode(StatusCodes.Status404NotFound, new { message = "Dokument with this id: " + id + "doesnt exist" });
        }

        // POST api/<DokumentController>
        [HttpPost]
        public IActionResult Post([FromBody] Dokument model)
        {
            try
            {
                if (Provera(model) == false)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { message = "Korisnik or kupac or liciter doesnt exist" });
                }
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
        public IActionResult Put(int id, [FromBody] Dokument dokument)
        {
            if (id != dokument.DokumentID)
            {
                return BadRequest();
            }
            try
            {
                if (Provera(dokument) == false)
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
        public IActionResult Delete(int id)
        {
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
            var accessToken = HttpContext.GetTokenAsync("access_token");
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken.Result}");
            var responseKorisnik = httpClient.GetAsync(requestUri: "http://localhost:9901/api/Korisnik/").Result;
            List<Korisnik> korisnici = JsonConvert.DeserializeObject<List<Korisnik>>(responseKorisnik.Content.ReadAsStringAsync().Result);
            bool proveraKorisnika = false;
            foreach (var k in korisnici)
            {
                if (k.KorisnikID == model.KorisnikID)
                {
                    proveraKorisnika = true;
                }
            }
            var responseKupac = httpClient.GetAsync(requestUri: "http://localhost:9904/api/Kupci/").Result;
            List<Kupac> kupci = JsonConvert.DeserializeObject<List<Kupac>>(responseKupac.Content.ReadAsStringAsync().Result);
            bool proveraKupca = false;
            foreach (var k in kupci)
            {
                if (k.KupacID == model.KupacID)
                {
                    proveraKupca = true;
                }
            }
            var responseLiciter = httpClient.GetAsync(requestUri: "http://localhost:9904/api/Liciteri/").Result;
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
    }
}
