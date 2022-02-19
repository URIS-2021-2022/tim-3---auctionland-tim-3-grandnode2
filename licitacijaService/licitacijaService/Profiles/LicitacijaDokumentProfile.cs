using AutoMapper;
using licitacijaService.DTOs;
using licitacijaService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace licitacijaService.Profiles
{
    public class LicitacijaDokumentProfile : Profile
    {
        public LicitacijaDokumentProfile()
        {
            CreateMap<LicitacijaDokumentCreationandUpdateDTO, LicitacijaDokument>();
            CreateMap<LicitacijaDokument, LicitacijaDokumentConfirmationDTO>();
            CreateMap<LicitacijaDokument, LicitacijaVrstaDokumentaDTO>();
        }
    }
}
