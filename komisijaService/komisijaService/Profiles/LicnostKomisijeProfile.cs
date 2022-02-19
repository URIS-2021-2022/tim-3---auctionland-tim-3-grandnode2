using AutoMapper;
using komisijaService.DTOs;
using komisijaService.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace komisijaService.Profiles
{
    public class LicnostKomisijeProfile : Profile 
    {
        public LicnostKomisijeProfile()
        {
            CreateMap<LicnostKomisijeCreationDto, LicnostKomisije>().ForAllMembers(opt => opt.Condition(src => src != null));
            CreateMap<LicnostKomisije, LicnostKomisijeConfirmationDto>().ForMember(
                    dest => dest.licnostKomisije,
                    opt => opt.MapFrom(src => $"{ src.imeLicnostiKomisije } { src.prezimeLicnostiKomisije }"));
            CreateMap<LicnostKomisijeUpdateDto, LicnostKomisije>();
        }
    }
}
