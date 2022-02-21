using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UgovorService.Entities;
using UgovorService.Models;

namespace UgovorService.Profiles
{
    public class UgovorConfirmationProfile : Profile
    {
        public UgovorConfirmationProfile()
        {
            CreateMap<UgovorConfirmation, UgovorConfirmationDto>();
            CreateMap<Ugovor, UgovorConfirmationDto>();
            CreateMap<Ugovor, UgovorConfirmation>();
        }
    }
}
