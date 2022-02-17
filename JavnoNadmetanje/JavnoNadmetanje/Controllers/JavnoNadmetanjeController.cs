using AutoMapper;
using JavnoNadmetanje.Data;
using JavnoNadmetanje.Entities;
using JavnoNadmetanje.Models;
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
    public class JavnoNadmetanjeController : ControllerBase
    {
        private readonly IJavnoNadmetanjeRepository javnoNadmetanjeRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public JavnoNadmetanjeController(IJavnoNadmetanjeRepository javnoNadmetanjeRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.javnoNadmetanjeRepository = javnoNadmetanjeRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [HttpGet]
        [HttpHead]
        public ActionResult<List<JavnoNadmetanjeDto>> GetJavnaNadmetanja()
        {
            List<JavnoNadmetanjeEntity> javnaNadmetanja = javnoNadmetanjeRepository.GetJavnaNadmetanja();

            if (javnaNadmetanja == null || javnaNadmetanja.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<JavnoNadmetanjeDto>>(javnaNadmetanja));
        }

        [HttpGet("{javnoNadmetanjeId}")]
        public ActionResult<JavnoNadmetanjeDto> GetJavnoNadmetanjeById(Guid javnoNadmetanjeId)
        {
            JavnoNadmetanjeEntity javnoNadmetanje = javnoNadmetanjeRepository.GetJavnoNadmetanjeById(javnoNadmetanjeId);
            
            if(javnoNadmetanje == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<JavnoNadmetanjeDto>(javnoNadmetanje));
        }

        [HttpPost]
        public ActionResult<JavnoNadmetanjeDto> CreateJavnoNadmetanje([FromBody] JavnoNadmetanjeDto javnoNadmetanje)
        {
            try
            {
                bool modelValid = ValidationJavnoNadmetanje(javnoNadmetanje);

                if(!modelValid)
                {
                    return BadRequest("Vreme kraja javnog nadmetanja mora biti nakon vremena pocetka javnog nadmetanja!");
                }

                JavnoNadmetanjeEntity jNadmetanje = mapper.Map<JavnoNadmetanjeEntity>(javnoNadmetanje);
                JavnoNadmetanjeEntity jNadmetanje1 = javnoNadmetanjeRepository.CreateJavnoNadmetanje(jNadmetanje);

                string location = linkGenerator.GetPathByAction("GetJavnaNadmetanja", "JavnoNadmetanje", new { javnoNadmetanjeId = jNadmetanje1.JavnoNadmetanjeId });
                return Created(location, mapper.Map<JavnoNadmetanjeDto>(jNadmetanje1)); 
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska prilikom kreiranja javnog nadmetanja!");
            }
        }

        [HttpDelete("{javnoNadmetanjeId}")]
        public IActionResult DeleteJavnoNadmetanje(Guid javnoNadmetanjeId)
        {
            try
            {
                JavnoNadmetanjeEntity javnoNadmetanje = javnoNadmetanjeRepository.GetJavnoNadmetanjeById(javnoNadmetanjeId);
                if (javnoNadmetanje == null)
                {
                    return NotFound();
                }

                javnoNadmetanjeRepository.DeleteJavnoNadmetanje(javnoNadmetanjeId);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska prilikom brisanja javnog nadmetanja!");
            }
        }

        [HttpPut]
        public ActionResult<JavnoNadmetanjeDto> UpdateJavnoNadmetanje(JavnoNadmetanjeEntity javnoNadmetanje)
        {
            try
            {
                if (javnoNadmetanjeRepository.GetJavnoNadmetanjeById(javnoNadmetanje.JavnoNadmetanjeId) == null)
                {
                    return NotFound();
                }

                JavnoNadmetanjeEntity jNadmetanje = javnoNadmetanjeRepository.UpdateJavnoNadmetanje(javnoNadmetanje);

                return Ok(mapper.Map<JavnoNadmetanjeDto>(jNadmetanje));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska prilikom azuriranja javnog nadmetanja!");
            }
        }

        private bool ValidationJavnoNadmetanje(JavnoNadmetanjeDto javnoNadmetanje)
         {
             if(javnoNadmetanje.VremePocetka > javnoNadmetanje.VremeKraja)
             {
                 return false;
             }

             return true;
         }

        [HttpOptions]
        public IActionResult GetJavnoNadmetanjeOptions()
        {
            Response.Headers.Add("Allow", "GET, HEAD, POST, PUT, DELETE");
            return Ok();
        }
    }
}

