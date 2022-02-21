using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Zalba.Entities;
using Zalba.Models;

namespace Zalba.Profiles
{
    public class ZalbaConfirmationProfile : Profile
    {
        public ZalbaConfirmationProfile()
        {
            CreateMap<ZalbaConfirmation, ZalbaConfirmationDto>();
            CreateMap<ZalbaE, ZalbaConfirmationDto>();
            CreateMap<ZalbaE, ZalbaConfirmation>();
        }
    }
}
