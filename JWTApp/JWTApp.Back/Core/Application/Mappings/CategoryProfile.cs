using AutoMapper;
using JWTApp.Back.Core.Application.Dtos;
using JWTApp.Back.Core.Domain;

namespace JWTApp.Back.Core.Application.Mappings
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryListDto>().ReverseMap();
        }
    }
}
