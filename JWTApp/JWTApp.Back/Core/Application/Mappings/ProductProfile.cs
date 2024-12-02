using AutoMapper;
using JWTApp.Back.Core.Application.Dtos;
using JWTApp.Back.Core.Domain;

namespace JWTApp.Back.Core.Application.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductListDto>().ReverseMap();
        }
    }
}
