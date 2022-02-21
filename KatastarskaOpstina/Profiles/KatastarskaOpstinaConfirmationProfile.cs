using AutoMapper;
using KatastarskaOpstina.Entities;
using KatastarskaOpstina.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KatastarskaOpstina.Profiles
{
    public class KatastarskaOpstinaConfirmationProfile : Profile
    {
        public KatastarskaOpstinaConfirmationProfile()
        {
            CreateMap<KatastarskaOpstinaConfirmation, KatastarskaOpstinaConfirmationDto>();
            CreateMap<KatastarskaOpstinaE, KatastarskaOpstinaConfirmationDto>();
            CreateMap<KatastarskaOpstinaE, KatastarskaOpstinaConfirmation>();
        }
    }
}
