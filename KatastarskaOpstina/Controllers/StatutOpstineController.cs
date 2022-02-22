using AutoMapper;
using KatastarskaOpstina.Auth;
using KatastarskaOpstina.Data;
using KatastarskaOpstina.Entities;
using KatastarskaOpstina.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KatastarskaOpstina.Controllers
{
    [ApiController]
    [Route("api/statutiOpstine")]
    [Produces("application/json", "application/xml")]
    public class StatutOpstineController : ControllerBase
    {
        private readonly IStatutOpstineRepository statutOpstineRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly IAuthService authService;
        public StatutOpstineController(IStatutOpstineRepository statutOpstineRepository, LinkGenerator linkGenerator, IMapper mapper, IAuthService authService)
        {
            this.statutOpstineRepository = statutOpstineRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.authService = authService;
        }

        /// <summary>
        /// Vraca sve statute opstine
        /// </summary>
        /// <returns>Lista statuta opstina</returns>
        /// <response code= "200">Vraca listu statuta opstina</response>
        /// <response code= "204">Ne postoji nijedan statut opstine</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        [HttpHead]
        [AllowAnonymous]
        public ActionResult<List<StatutOpstineModelDto>> GetStatutOpstines() //ovde mi isto fale prosledjeni parametri
        {
            var statutOpstines = statutOpstineRepository.GetStatutOpstines();//ovde mi isto fale prosledjeni parametri
            if (statutOpstines == null || statutOpstines.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<StatutOpstineModelDto>>(statutOpstines));
        }

        /// <summary>
        /// Vraca statut opstine po ID-u
        /// </summary>
        /// <param name="statutOpstineID">ID stauta opstine</param>
        /// <returns>Odgovarajuca katastarska opstina</returns>
        /// <response code= "200">Vraca trazen statut opstine</response>
        /// <response code= "204">Nije pronadjen trazeni statut opstine</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{StatutOpstineID}")]
        [AllowAnonymous]
        public ActionResult<StatutOpstineModelDto> GetStatutOpstine(Guid statutOpstineID)
        {
            var statutOpstine = statutOpstineRepository.GetStatutOpstine(statutOpstineID);

            if (statutOpstine == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<StatutOpstineModelDto>(statutOpstine));

        }

        /// <summary>
        /// Kreiranje novog statuta opstine
        /// </summary>
        /// <param name="statutOpstine">Model statuta opstine</param>
        /// <param name="key">Kljuc sa kojim se proverava autorizacija(key vrednost: MajaCetic)</param>
        /// <returns></returns>
        /// <response code= "201">Vraca kreiran statut opstine</response>
        /// <response code= "401">Lice koje zeli da izvrsi kreiranje statuta opstine nije autorizovani korisnik</response>
        /// <response code= "500">Doslo je do greske na serveru prilikom kreiranja stauta opstine</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Consumes("application/json")]
        [HttpPost]
        public ActionResult<StatutOpstineCreationDto> CreateStatutOpstine([FromBody] StatutOpstineCreationDto statutOpstine, [FromHeader] string key)
        {
            if (!authService.Authorize(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Korisnik nije autorizovan!");
            }
            try
            {
                

                var statutOpstineE = mapper.Map<StatutOpstineE>(statutOpstine);
                var confirmation = statutOpstineRepository.CreateStatutOpstine(statutOpstineE);
                statutOpstineRepository.SaveChanges();
                string location = linkGenerator.GetPathByAction("GetStatutOpstine", "StatutOpstine", new { statutOpstineID = confirmation.StatutOpstineID });
                return Created(location, mapper.Map<StatutOpstineConfirmationDto>(confirmation));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create error");
            }

        }

        /// <summary>
        /// Brisanje statuta opstine
        /// </summary>
        /// <param name="statutOpstineID">ID statuta opstine</param>
        /// <param name="key">Kljuc sa kojim se proverava autorizacija(key vrednost: MajaCetic)</param>
        /// <returns></returns>
        /// <response code= "204">Statut opstine uspesno obrisan</response>
        /// <response code= "401">Lice koje zeli da izvrsi brisanje statuta opstine nije autorizovani korisnik</response>
        /// <response code= "404">Nije pronadjena statut opstine za brisanje</response>
        /// <response code= "500">Doslo je do greske na serveru prilikom brisanja statuta opstine</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpDelete("{StatutOpstineID}")]
        public IActionResult DeleteStatutOpstine(Guid statutOpstineID, [FromHeader] string key)
        {
            if (!authService.Authorize(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Korisnik nije autorizovan!");
            }
            try
            {
                var statutOpstine = statutOpstineRepository.GetStatutOpstine(statutOpstineID);

                if (statutOpstine == null)
                {
                    return NotFound();
                }

                statutOpstineRepository.DeleteStatutOpstine(statutOpstineID);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }

        /// <summary>
        /// Azuriranje statuta opstine
        /// </summary>
        /// <param name="statutOpstine">Model statuta opstine</param>
        /// <param name="key">Kljuc sa kojim se proverava autorizacija(key vrednost: MajaCetic)</param>
        /// <returns>Potvrda u azuriranju statuta opstine</returns>
        /// <response code= "200">Statut opstine azurirana</response>
        /// <response code= "401">Lice koje zeli da izvrsi brisanje statuta opstine nije autorizovani korisnik</response>
        /// <response code= "404">Nije pronadjen statut opstine za brisanje</response>
        /// <response code= "500">Doslo je do greske na serveru prilikom brisanja statuta opstine</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPut]
        public ActionResult<StatutOpstineConfirmationDto> UpdateStatutOpstine(StatutOpstineModelDto statutOpstine, [FromHeader] string key)
        {
            if (!authService.Authorize(key))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Korisnik nije autorizovan!");
            }
            try
            {


                if (statutOpstineRepository.GetStatutOpstine(statutOpstine.StatutOpstineID) == null)
                {
                    return NotFound();
                }
                StatutOpstineE statutOpstineE = mapper.Map<StatutOpstineE>(statutOpstine);
                mapper.Map(statutOpstineE, statutOpstineRepository.GetStatutOpstine(statutOpstine.StatutOpstineID));
                statutOpstineRepository.SaveChanges();


                return Ok(mapper.Map<StatutOpstineModelDto>(statutOpstineRepository.GetStatutOpstine(statutOpstine.StatutOpstineID)));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        /// <summary>
        /// Vraca opcije za rad sa statutom opstine
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetStatutOpstinesOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
