using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uplata.Entities;
using Uplata.Models;

namespace Uplata.Profiles
{
    public class BankaProfile : Profile
    {
        public BankaProfile()
        {
            CreateMap<BankaEntity, BankaDto>();
            CreateMap<BankaDto, BankaEntity>();
            CreateMap<BankaCreateDto, BankaEntity>();
            CreateMap<BankaEntity, BankaEntity>();
        }
    }
}
