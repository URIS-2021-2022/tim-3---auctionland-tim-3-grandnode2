using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UgovorService.Entities;
using UgovorService.Models;

namespace UgovorService.Profiles
{
    public class UgovorProfile : Profile
    {
        public UgovorProfile()
        {
            CreateMap<Ugovor, UgovorDto>();
            CreateMap<UgovorDto, Ugovor>();
            CreateMap<UgovorCreateDto, Ugovor>();
            CreateMap<UgovorUpdateDto, Ugovor>();
            CreateMap<Ugovor, Ugovor>();
        }
    }
}
