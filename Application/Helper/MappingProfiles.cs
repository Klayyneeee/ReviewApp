using AutoMapper;
using ReviewApp.Application.Dto;
using ReviewApp.Domain.Properties.Models;

namespace ReviewApp.Application.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Goods, GoodDto>();
            CreateMap<GoodDto, Goods>();
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();

        }
    }
}
