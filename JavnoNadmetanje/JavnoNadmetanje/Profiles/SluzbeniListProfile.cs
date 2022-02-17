using AutoMapper;
using JavnoNadmetanje.Entities;
using JavnoNadmetanje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Profiles
{
    public class SluzbeniListProfile : Profile
    {
        public SluzbeniListProfile()
        {
            CreateMap<SluzbeniListEntity, SluzbeniListDto>();
            CreateMap<SluzbeniListDto, SluzbeniListEntity>();
        }
    }
}
