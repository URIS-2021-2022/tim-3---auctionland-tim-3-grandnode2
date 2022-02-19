using AutoMapper;
using licitacijaService.DTOs.Mock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace licitacijaService.Profiles
{
    public class JavnoNadmetanjeProfile : Profile
    {
        public JavnoNadmetanjeProfile()
        {
            CreateMap<JavnoNadmetanjeDTO, JavnoNadmetanjeConfirmationDTO>();
        }
    }
}
