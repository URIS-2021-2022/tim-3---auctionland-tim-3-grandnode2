using AutoMapper;
using KatastarskaOpstina.Entities;
using KatastarskaOpstina.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KatastarskaOpstina.Profiles
{
    public class StatutOpstineProfile : Profile
    {
        public StatutOpstineProfile()
        {
            CreateMap<StatutOpstineE, StatutOpstineModelDto>();
            CreateMap<StatutOpstineModelDto, StatutOpstineE>();
            CreateMap<StatutOpstineCreationDto, StatutOpstineE>();
            CreateMap<StatutOpstineE, StatutOpstineE>();
        }
    }
}
