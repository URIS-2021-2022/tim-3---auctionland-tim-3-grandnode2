using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zalba.Entities;
using Zalba.Models;

namespace Zalba.Profiles
{
    public class ZalbaProfile : Profile
    {
        public ZalbaProfile()
        {
            CreateMap<ZalbaE, ZalbaModelDto>();
            CreateMap<ZalbaModelDto, ZalbaE>();
            CreateMap<ZalbaCreationDto, ZalbaE>();
            CreateMap<ZalbaE, ZalbaE>();
        }
    }
}
