using AutoMapper;
using JavnoNadmetanje.Entities;
using JavnoNadmetanje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Profiles
{
    public class JavnoNadmetanjeProfile : Profile
    {
        public JavnoNadmetanjeProfile()
        {
            CreateMap<JavnoNadmetanjeEntity, JavnoNadmetanjeDto>();
            CreateMap<JavnoNadmetanjeDto, JavnoNadmetanjeEntity>();
        }
    }
}
