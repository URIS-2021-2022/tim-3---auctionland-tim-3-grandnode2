using AutoMapper;
using JavnoNadmetanje.Entities;
using JavnoNadmetanje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Profiles
{
    public class OglasProfile :  Profile
    {
        public OglasProfile()
        {
            CreateMap<OglasEntity, OglasDto>();
            CreateMap<OglasDto, OglasEntity>();
            CreateMap<OglasCreateDto, OglasEntity>();
            CreateMap<OglasEntity, OglasEntity>();
        }
    }
}
