using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uplata.Entities;
using Uplata.Models;

namespace Uplata.Profiles
{
    public class UplataProfile : Profile
    {
        public UplataProfile()
        {
            CreateMap<UplataEntity, UplataDto>();
            CreateMap<UplataDto, UplataEntity>();
            CreateMap<UplataCreateDto, UplataEntity>();
            CreateMap<UplataEntity, UplataEntity>();
        }
    }
}
