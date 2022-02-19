using AutoMapper;
using komisijaService.DTOs;
using komisijaService.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace komisijaService.Profiles
{
    public class KomisijaProfile : Profile
    {
        public KomisijaProfile()
        {
            CreateMap<KomisijaCreationDto, Komisija>();
            CreateMap<Komisija, KomisijaConfirmationDto>();
            CreateMap<KomisijaUpdateDto, Komisija>();           
        }
    }
}
