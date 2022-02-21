using AutoMapper;
using KatastarskaOpstina.Entities;
using KatastarskaOpstina.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KatastarskaOpstina.Profiles
{
    public class KatastarskaOpstinaProfile : Profile
    {
        public KatastarskaOpstinaProfile()
        {
            CreateMap<KatastarskaOpstinaE, KatastarskaOpstinaModelDto>();
            CreateMap<KatastarskaOpstinaModelDto, KatastarskaOpstinaE>();
            CreateMap<KatastarskaOpstinaCreationDto, KatastarskaOpstinaE>();
            CreateMap<KatastarskaOpstinaE, KatastarskaOpstinaE>();
        }
    }
}
