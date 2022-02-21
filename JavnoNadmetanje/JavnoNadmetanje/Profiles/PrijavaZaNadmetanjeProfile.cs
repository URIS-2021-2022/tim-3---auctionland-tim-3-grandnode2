using AutoMapper;
using JavnoNadmetanje.Entities;
using JavnoNadmetanje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Profiles
{
    public class PrijavaZaNadmetanjeProfile : Profile
    {
        public PrijavaZaNadmetanjeProfile()
        {
            CreateMap<PrijavaZaNadmetanjeEntity, PrijavaZaNadmetanjeDto>();
            CreateMap<PrijavaZaNadmetanjeDto, PrijavaZaNadmetanjeEntity>();
            CreateMap<PrijavaZaNadmetanjeCreateDto, PrijavaZaNadmetanjeEntity>();
            CreateMap<PrijavaZaNadmetanjeEntity, PrijavaZaNadmetanjeEntity>();
        }
    }
}
