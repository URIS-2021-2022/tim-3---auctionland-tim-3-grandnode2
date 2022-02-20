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
    public class ParcelaProfile : Profile
    {
        public ParcelaProfile()
        {
            CreateMap<Parcela, ParcelaDto>();
            CreateMap<ParcelaDto, Parcela>();
            CreateMap<ParcelaUpdateDto, Parcela>();
            CreateMap<ParcelaCreateDto, Parcela>();
            CreateMap<Parcela, Parcela>();
        }
    }
}
