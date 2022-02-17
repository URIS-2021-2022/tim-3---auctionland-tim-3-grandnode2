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
    [Route("api/sluzbeniListovi")]
    public class SluzbeniListController : ControllerBase
    {
        private readonly ISluzbeniListRepository sluzbeniListRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        
        public SluzbeniListController(ISluzbeniListRepository sluzbeniListRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.sluzbeniListRepository = sluzbeniListRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [HttpGet]
        [HttpHead]
        public ActionResult<List<SluzbeniListDto>> GetSluzbeniListovi()
        {
            List<SluzbeniListEntity> sluzbeniListovi = sluzbeniListRepository.GetSluzbeniListovi();

            if (sluzbeniListovi == null || sluzbeniListovi.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<SluzbeniListDto>>(sluzbeniListovi));
        }

        [HttpGet("{sluzbeniListId}")]
        public ActionResult<SluzbeniListDto> GetSluzbeniListById(Guid sluzbeniListId)
        {
            SluzbeniListEntity sluzbeniList = sluzbeniListRepository.GetSluzbeniListById(sluzbeniListId);

            if (sluzbeniList == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<SluzbeniListDto>(sluzbeniList));
        }

        [HttpPost]
        public ActionResult<SluzbeniListDto> CreateSluzbeniList([FromBody] SluzbeniListDto sluzbeniList)
        {
            try
            {
                SluzbeniListEntity sList1 = mapper.Map<SluzbeniListEntity>(sluzbeniList);
                SluzbeniListEntity sList2 = sluzbeniListRepository.CreateSluzbeniList(sList1);
                string location = linkGenerator.GetPathByAction("GetSluzbeniListovi", "SluzbeniList", new { sluzbeniListId = sList1.SluzbeniListId });
                return Created(location, mapper.Map<SluzbeniListEntity>(sList2));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska prilikom kreiranja sluzbenog lista!");
            }
        }

        [HttpDelete("{sluzbeniListId}")]
        public IActionResult DeleteSluzbeniList(Guid sluzbeniListId)
        {
            try
            {
                SluzbeniListEntity sluzbeniList = sluzbeniListRepository.GetSluzbeniListById(sluzbeniListId); ;
                if (sluzbeniList == null)
                {
                    return NotFound();
                }

                sluzbeniListRepository.DeleteSluzbeniList(sluzbeniListId);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska prilikom brisanja sluzbenog lista!");
            }
        }

        [HttpPut]
        public ActionResult<SluzbeniListDto> UpdateSluzbeniList(SluzbeniListEntity sluzbeniList)
        {
            try
            {
                if (sluzbeniListRepository.GetSluzbeniListById(sluzbeniList.SluzbeniListId) == null)
                {
                    return NotFound();
                }

                SluzbeniListEntity sluzbeniList1 = sluzbeniListRepository.UpdateSluzbeniList(sluzbeniList);

                return Ok(mapper.Map<SluzbeniListDto>(sluzbeniList1));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska prilikom azuriranja sluzbenog lista!");
            }
        }

        [HttpOptions]
        public IActionResult GetSluzbeniListOptions()
        {
            Response.Headers.Add("Allow", "GET, HEAD, POST, PUT, DELETE");
            return Ok();
        }
    }
}
