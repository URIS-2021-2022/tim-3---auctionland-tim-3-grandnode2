using KupacService.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KupacService.Logger;
using KupacService.Auth;
using KupacService.Entities;
using KupacService.ServiceCalls;
using KupacService.DTO;

namespace KupacService.Controllers
{
    [ApiController]
    [Route("api/Kupci")]
    [Produces("application/json", "application/xml")]
    public class KupacController : ControllerBase
    {
        private readonly IKupacRepository kupacRepository;
        private readonly IFizickoLiceRepository fizickoLiceRepository;
        private readonly IPravnoLiceRepository pravnoLiceRepository;
        private readonly IKontaktOsobaRepository kontaktOsobaRepository;
        private readonly ILiciterRepository liciterRepository;

        private readonly IUplataService uplataService;

        private readonly IMapper mapper;
        private readonly LinkGenerator linkGenerator;
        private readonly ILoggerMockRepository logger;
        private readonly IHttpContextAccessor contextAccessor;

        private readonly IAuthHelper authHelper;

        public KupacController(IKupacRepository kupacRepository, IFizickoLiceRepository fizickoLiceRepository, IPravnoLiceRepository pravnoLiceRepository, IKontaktOsobaRepository kontaktOsobaRepository, 
                ILiciterRepository liciterRepository, IUplataService uplataService, IMapper mapper, LinkGenerator linkGenerator, ILoggerMockRepository logger, IHttpContextAccessor contextAccessor, IAuthHelper authHelper)
        {
            this.kupacRepository = kupacRepository;
            this.fizickoLiceRepository = fizickoLiceRepository;
            this.pravnoLiceRepository = pravnoLiceRepository;
            this.kontaktOsobaRepository = kontaktOsobaRepository;
            this.liciterRepository = liciterRepository;

            this.uplataService = uplataService;

            this.mapper = mapper;
            this.linkGenerator = linkGenerator;
            this.logger = logger;
            this.contextAccessor = contextAccessor;
            this.authHelper = authHelper;
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<Kupac>> GetKupci(bool imaZabranu)
        {
            var kupci = kupacRepository.GetKupci(imaZabranu);
            if (kupci == null || kupci.Count==0)
            {
                return NoContent();
            }

            foreach(Kupac k in kupci)
            {
                List<UplataDto> uplate = uplataService.GetUplateByKupacId(k.KupacId).Result;
                List<Guid> uplateGuids = uplate.Select(u => u.KupacId).ToList();
                k.UplateId = uplateGuids;
                //mapper.Map<List<UplataDto>>(k.UplateId);
                List<Guid> ovlascenaLicaGuids = fizickoLiceRepository.GetOvlascenaLicaByKupacId(k.KupacId);
                k.OvlascenoLice = ovlascenaLicaGuids;
                List<Guid> javnaNadmetanjaGuids = kupacRepository.GetJavnaNadmetanjaByKupacId(k.KupacId);
                k.JavnaNadmetanjaId = javnaNadmetanjaGuids;
            }

            logger.Log(LogLevel.Information, contextAccessor.HttpContext.TraceIdentifier, "", "Get sve kupce", null);
            return Ok(kupci);
        }

        [HttpGet("{kupacId}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Kupac> GetKupacById(Guid kupacId)
        {
            var kupac = kupacRepository.GetKupacById(kupacId);

            if (kupac==null)
            {
                return NoContent();
            }

            List<UplataDto> uplate = uplataService.GetUplateByKupacId(kupac.KupacId).Result;
            List<Guid> uplateGuids = uplate.Select(u => u.KupacId).ToList();
            kupac.UplateId = uplateGuids;
            //mapper.Map<List<UplataDto>>(kupac.UplateId);
            List<Guid> ovlascenaLicaGuids = fizickoLiceRepository.GetOvlascenaLicaByKupacId(kupac.KupacId);
            kupac.OvlascenoLice = ovlascenaLicaGuids;
            List<Guid> javnaNadmetanjaGuids = kupacRepository.GetJavnaNadmetanjaByKupacId(kupac.KupacId);
            kupac.JavnaNadmetanjaId = javnaNadmetanjaGuids;

            return Ok(kupac);
        }
    }
}
