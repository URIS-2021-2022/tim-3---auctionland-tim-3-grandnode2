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
    public class DozvoljeniRadProfile : Profile
    {
        public DozvoljeniRadProfile()
        {
            CreateMap<DozvoljeniRad, DozvoljeniRadDto>();
            CreateMap<DozvoljeniRadDto, DozvoljeniRad>();
            CreateMap<DozvoljeniRadCreateDto, DozvoljeniRad>();
            CreateMap<DozvoljeniRadUpdateDto, DozvoljeniRad>();
            CreateMap<DozvoljeniRad, DozvoljeniRad>();
        }
    }
}
