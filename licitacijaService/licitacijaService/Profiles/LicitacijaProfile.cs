using AutoMapper;
using licitacijaService.DTOs;
using licitacijaService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace licitacijaService.Profiles
{
    public class LicitacijaProfile : Profile
    {
        public LicitacijaProfile()
        {
            CreateMap<LicitacijaCreationDTO, Licitacija>();
            CreateMap<Licitacija, LicitacijaConfirmationDTO>();
            CreateMap<LicitacijaUpdateDTO, Licitacija>();
        }
    }
}
