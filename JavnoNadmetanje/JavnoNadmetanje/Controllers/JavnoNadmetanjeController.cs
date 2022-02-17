using JavnoNadmetanje.Data;
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

        public JavnoNadmetanjeController(IJavnoNadmetanjeRepository javnoNadmetanjeRepository, LinkGenerator linkGenerator)
        {
            this.javnoNadmetanjeRepository = javnoNadmetanjeRepository;
            this.linkGenerator = linkGenerator;
        }

        [HttpGet]
        public ActionResult<List<JavnoNadmetanjeModel>> GetJavnaNadmetanja()
        {
            List<JavnoNadmetanjeModel> javnaNadmetanja = javnoNadmetanjeRepository.GetJavnaNadmetanja();

            if (javnaNadmetanja == null || javnaNadmetanja.Count == 0)
            {
                return NoContent();
            }
            return Ok(javnaNadmetanja);
        }

        [HttpGet("{javnoNadmetanjeId}")]
        public ActionResult<JavnoNadmetanjeModel> GetJavnoNadmetanjeById(Guid javnoNadmetanjeId)
        {
            JavnoNadmetanjeModel javnoNadmetanje = javnoNadmetanjeRepository.GetJavnoNadmetanjeById(javnoNadmetanjeId);
            
            if(javnoNadmetanje == null)
            {
                return NotFound();
            }
            return Ok(javnoNadmetanje);
        }

        [HttpPost]
        public ActionResult<JavnoNadmetanjeModel> CreateJavnoNadmetanje([FromBody] JavnoNadmetanjeModel javnoNadmetanje)
        {
            try
            {
                bool modelValid = ValidationJavnoNadmetanje(javnoNadmetanje);

                if(!modelValid)
                {
                    return BadRequest("Vreme kraja javnog nadmetanja mora biti nakon vremena pocetka javnog nadmetanja!");
                }

                JavnoNadmetanjeModel jNadmetanje = javnoNadmetanjeRepository.CreateJavnoNadmetanje(javnoNadmetanje);
                string location = linkGenerator.GetPathByAction("GetJavnaNadmetanja", "JavnoNadmetanje", new { javnoNadmetanjeId = jNadmetanje.JavnoNadmetanjeId });
                return Created(location, jNadmetanje); 
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
                JavnoNadmetanjeModel javnoNadmetanje = javnoNadmetanjeRepository.GetJavnoNadmetanjeById(javnoNadmetanjeId);
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
        public ActionResult<JavnoNadmetanjeModel> UpdateJavnoNadmetanje(JavnoNadmetanjeModel javnoNadmetanje)
        {
            try
            {
                bool modelValid = ValidationJavnoNadmetanje(javnoNadmetanje);

                if (!modelValid)
                {
                    return BadRequest("Vreme kraja javnog nadmetanja mora biti nakon vremena pocetka javnog nadmetanja!");
                }

                if (javnoNadmetanjeRepository.GetJavnoNadmetanjeById(javnoNadmetanje.JavnoNadmetanjeId) == null)
                {
                    return NotFound();
                }

                return Ok(javnoNadmetanjeRepository.UpdateJavnoNadmetanje(javnoNadmetanje));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska prilikom azuriranja javnog nadmetanja!");
            }
        }

        private bool ValidationJavnoNadmetanje(JavnoNadmetanjeModel javnoNadmetanje)
         {
             if(javnoNadmetanje.VremePocetka > javnoNadmetanje.VremeKraja)
             {
                 return false;
             }

             return true;
         }
    }
}

