using AutoMapper;
using ParcelaService.Entities;
using ParcelaService.Models;
using ParcelaService.Models.CreateDto;
using ParcelaService.Models.UpdateDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelaService.Profiles
{
    public class KvalitetZemljistaProfile : Profile
    {
        public KvalitetZemljistaProfile()
        {
            CreateMap<KvalitetZemljista, KvalitetZemljistaDto>();
            CreateMap<KvalitetZemljistaDto, KvalitetZemljista>();
            CreateMap<KvalitetZemljistaCreateDto, KvalitetZemljista>();
            CreateMap<KvalitetZemljistaUpdateDto, KvalitetZemljista>();
            CreateMap<KvalitetZemljista, KvalitetZemljista>();
        }
    }
}
