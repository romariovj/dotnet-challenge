using AutoMapper;
using DotnetChallenge.Application.Dtos;
using DotnetChallenge.Domain.Entities;

namespace DotnetChallenge.Application.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<ProductDto, Product>();
            CreateMap<Product, ProductDto>();
        }
    }
}
