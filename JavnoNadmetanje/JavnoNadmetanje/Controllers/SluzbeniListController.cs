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
    [Route("api/sluzbeniListovi")]
    public class SluzbeniListController : ControllerBase
    {
        private readonly ISluzbeniListRepository sluzbeniListRepository;
        private readonly LinkGenerator linkGenerator;

        public SluzbeniListController(ISluzbeniListRepository sluzbeniListRepository, LinkGenerator linkGenerator)
        {
            this.sluzbeniListRepository = sluzbeniListRepository;
            this.linkGenerator = linkGenerator;
        }

        [HttpGet]
        public ActionResult<List<SluzbeniListModel>> GetSluzbeniListovi()
        {
            List<SluzbeniListModel> sluzbeniListovi = sluzbeniListRepository.GetSluzbeniListovi();

            if (sluzbeniListovi == null || sluzbeniListovi.Count == 0)
            {
                return NoContent();
            }
            return Ok(sluzbeniListovi);
        }

        [HttpGet("{sluzbeniListId}")]
        public ActionResult<SluzbeniListModel> GetSluzbeniListById(Guid sluzbeniListId)
        {
            SluzbeniListModel sluzbeniList = sluzbeniListRepository.GetSluzbeniListById(sluzbeniListId);

            if (sluzbeniList == null)
            {
                return NotFound();
            }
            return Ok(sluzbeniList);
        }

        [HttpPost]
        public ActionResult<SluzbeniListModel> CreateSluzbeniList([FromBody] SluzbeniListModel sluzbeniList)
        {
            try
            {
                SluzbeniListModel sList = sluzbeniListRepository.CreateSluzbeniList(sluzbeniList);
                string location = linkGenerator.GetPathByAction("GetSluzbeniListovi", "SluzbeniList", new { sluzbeniListId = sList.SluzbeniListId });
                return Created(location, sList);
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
                SluzbeniListModel sluzbeniList = sluzbeniListRepository.GetSluzbeniListById(sluzbeniListId); ;
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
        public ActionResult<SluzbeniListModel> UpdateSluzbeniList(SluzbeniListModel sluzbeniList)
        {
            try
            {
                if (sluzbeniListRepository.GetSluzbeniListById(sluzbeniList.SluzbeniListId) == null)
                {
                    return NotFound();
                }

                return Ok(sluzbeniListRepository.UpdateSluzbeniList(sluzbeniList));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Greska prilikom azuriranja sluzbenog lista!");
            }
        }
    }
}
