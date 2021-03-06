﻿using System.Linq;
using AutoMapper;
using Capri.Database.Entities;
using Capri.Web.ViewModels.Promoter;

namespace Capri.Web.Configuration.Mapper
{
    public class PromoterMappingProfile : Profile
    {
        public PromoterMappingProfile()
        {
            CreateMap<PromoterRegistration, Promoter>();

            CreateMap<PromoterUpdateViewModel, Promoter>();

            CreateMap<Promoter, PromoterViewModel>()
            .ForMember(
                view=>view.Proposals, 
                o=>o.MapFrom(promoter=>promoter.Proposals.Select(p=>p.Id)));
        }
    }
}
