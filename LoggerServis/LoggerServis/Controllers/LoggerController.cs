using LoggerServis.Database.Entities;
using LoggerServis.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LoggerServis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoggerController : ControllerBase
    {

        private readonly LoggerService loggerService;

        public LoggerController()
        {
            loggerService = new LoggerService();
        }

        // GET: api/<LoggerController>
        [HttpGet]
        public IEnumerable<Logger> Get()
        {
            return loggerService.GetAll();
        }

        // GET api/<LoggerController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Logger logger = loggerService.GetById(id);
            if (logger != null)
            {
                return StatusCode(StatusCodes.Status200OK, logger);
            }
            return StatusCode(StatusCodes.Status404NotFound, new { message = "Logger with this id: " + id + " doesnt exist" });
        }

        // POST api/<LoggerController>
        [HttpPost]
        public IActionResult Post([FromBody] Logger model)
        {
            try
            {
                loggerService.Save(model);
                return StatusCode(StatusCodes.Status201Created, model);
            }
            catch (Exception exp)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, exp);
            }
        }

        // PUT api/<LoggerController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Logger logger)
        {
            if (id != logger.LoggerID)
            {
                return BadRequest();
            }

            try
            {
                loggerService.Update(logger);
            }
            catch (Exception exp)
            {
                if (!loggerService.LoggerExists(id))
                {
                    return StatusCode(StatusCodes.Status404NotFound, exp);
                }
                else
                {
                    throw;
                }
            }
            return StatusCode(StatusCodes.Status200OK, logger);
        }

        // DELETE api/<LoggerController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Logger logger = loggerService.GetById(id);
            if (logger == null)
            {
                return NotFound();
            }
            loggerService.Delete(logger);

            return Ok(logger);
        }
    }
}
