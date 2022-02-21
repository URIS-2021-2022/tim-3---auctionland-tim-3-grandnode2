using AutoMapper;
using JavnoNadmetanje.Entities;
using JavnoNadmetanje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Profiles
{
    public class DokumentPrijavaZaNadmetanjeProfile : Profile
    {
        public DokumentPrijavaZaNadmetanjeProfile()
        {
            CreateMap<DokumentPrijavaZaNadmetanjeEntity, DokumentPrijavaZaNadmetanjeDto>();
            CreateMap<DokumentPrijavaZaNadmetanjeDto, DokumentPrijavaZaNadmetanjeEntity>();
            CreateMap<DokumentPrijavaZaNadmetanjeEntity, DokumentPrijavaZaNadmetanjeEntity>();
        }
    }
}
