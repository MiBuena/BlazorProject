using AutoMapper;
using ListGenerator.Web.Shared.Dtos;
using ListGenerator.Models.Entities;
using ListGenerator.Web.Shared.ViewModels;

namespace ListGenerator.Common.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ItemOverviewDto, ItemOverviewViewModel>()
                .ForMember(item => item.ReplenishmentPeriod, opt => opt.MapFrom(a => a.ReplenishmentPeriod.ToString()))
                .ReverseMap()
                .ForPath(s => s.ReplenishmentPeriod, opt => opt.MapFrom(src => double.Parse(src.ReplenishmentPeriod)));

            CreateMap<ItemDto, ItemViewModel>()
                .ForMember(item => item.ReplenishmentPeriod, opt => opt.MapFrom(a => a.ReplenishmentPeriod.ToString()))
                .ReverseMap()
                .ForPath(s => s.ReplenishmentPeriod, opt => opt.MapFrom(src => double.Parse(src.ReplenishmentPeriod)));

            CreateMap<ItemDto, Item>()
                .ReverseMap();

            CreateMap<ItemDto, PurchaseItemViewModel>()
                .ForMember(item => item.ItemId, opt => opt.MapFrom(a => a.Id));

            CreateMap<PurchaseItemViewModel, PurchaseItemDto>()
                .ForMember(item => item.Quantity, opt => opt.MapFrom(a => int.Parse(a.Quantity)));
        }
    }
}
