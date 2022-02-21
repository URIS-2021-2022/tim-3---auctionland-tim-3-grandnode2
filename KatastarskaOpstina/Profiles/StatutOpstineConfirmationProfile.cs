using AutoMapper;
using KatastarskaOpstina.Entities;
using KatastarskaOpstina.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KatastarskaOpstina.Profiles
{
    public class StatutOpstineConfirmationProfile : Profile
    {
        public StatutOpstineConfirmationProfile()
        {
            CreateMap<StatutOpstineConfirmation, StatutOpstineConfirmationDto>();
            CreateMap<StatutOpstineE, StatutOpstineConfirmationDto>();
            CreateMap<StatutOpstineE, StatutOpstineConfirmation>();
        }
    }
}
