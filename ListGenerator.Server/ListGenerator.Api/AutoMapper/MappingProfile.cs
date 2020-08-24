﻿using AutoMapper;
using ListGenerator.Models.Entities;
using ListGenerator.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace ListGenerator.Api.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ItemViewModel, Item>()
                .ForMember(item => item.ReplenishmentPeriod, opt => opt.MapFrom(a => int.Parse(a.ReplenishmentPeriod)))
                .ReverseMap()
                .ForPath(s => s.ReplenishmentPeriod, opt => opt.MapFrom(src => src.ReplenishmentPeriod.ToString()));
        }
    }
}
