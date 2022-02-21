using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zalba.Entities;
using Zalba.Models;

namespace TipZalbe.Profiles
{
    public class TipZalbeProfile : Profile
    {
        public TipZalbeProfile()
        {
            CreateMap<TipZalbeE, TipZalbeModelDto>();
            CreateMap<TipZalbeModelDto, TipZalbeE>();
            CreateMap<TipZalbeCreationDto, TipZalbeE>();
            CreateMap<TipZalbeE, TipZalbeE>();
        }
    }
}
