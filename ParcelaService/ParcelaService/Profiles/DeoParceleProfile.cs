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
    public class DeoParceleProfile : Profile
    {
        public DeoParceleProfile()
        {
            CreateMap<DeoParcele, DeoParceleDto>();
            CreateMap<DeoParceleDto, DeoParcele>();
            CreateMap<DeoParceleCreateDto, DeoParcele>();
            CreateMap<DeoParceleUpdateDto, DeoParcele>();
            CreateMap<DeoParcele, DeoParcele>();
        }
    }
}
