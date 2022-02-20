using AutoMapper;
using ParcelaService.Entities;
using ParcelaService.Models;
using ParcelaService.Models.CreateDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelaService.Profiles
{
    public class ZasticenaZonaProfile : Profile
    {
        public ZasticenaZonaProfile()
        {
            CreateMap<ZasticenaZona, ZasticenaZonaDto>();
            CreateMap<ZasticenaZonaDto, ZasticenaZona>();
            CreateMap<ZasticenaZonaCreateDto, ZasticenaZona>();
            CreateMap<ZasticenaZonaUpdateDto, ZasticenaZona>();
            CreateMap<ZasticenaZona, ZasticenaZona>();
        }
    }
}
