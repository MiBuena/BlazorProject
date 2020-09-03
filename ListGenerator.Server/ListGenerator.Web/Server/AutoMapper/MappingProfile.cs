using AutoMapper;
using ListGenerator.Data.Entities;
using ListGenerator.Web.Shared.Dtos;
using ListGenerator.Web.Client.ViewModels;

namespace ListGenerator.Web.Server.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ItemDto, Item>()
                .ReverseMap();

            CreateMap<PurchaseItemDto, Purchase>();
        }
    }
}