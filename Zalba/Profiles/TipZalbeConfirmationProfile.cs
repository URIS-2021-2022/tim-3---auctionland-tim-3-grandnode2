using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zalba.Entities;
using Zalba.Models;

namespace TipZalbe.Profiles
{
    public class TipZalbeConfirmationProfile : Profile
    {
        public TipZalbeConfirmationProfile()
        {
            CreateMap<TipZalbeConfirmation, TipZalbeConfirmationDto>();
            CreateMap<TipZalbeE, TipZalbeConfirmationDto>();
            CreateMap<TipZalbeE, TipZalbeConfirmation>();
        }
    }
}
