using AutoMapper;
using DotnetChallenge.Application.Commands;
using DotnetChallenge.Application.Dtos;
using DotnetChallenge.Domain.Entities;

namespace DotnetChallenge.Application.Mappings
{
    public class AutoMapperProfile : Profile 
    {
        public AutoMapperProfile() 
        {

            CreateMap<ProductDto, Product>();
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.StatusName, opt => opt.MapFrom<ProductStatusResolver>())
                .ForMember(dest=> dest.Discount, opt => opt.MapFrom<DiscountResolver>());

            CreateMap<CreateProductCommand, Product>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => new ProductStatus()
                {
                    Id = src.Status,
                }))
                .ForMember(dest => dest.Stock, opt => opt.MapFrom(src => src.Stock))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price));

            CreateMap<UpdateProductCommand, Product>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => new ProductStatus()
                {
                    Id = src.Status,
                }))
                .ForMember(dest => dest.Stock, opt => opt.MapFrom(src => src.Stock))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price));
        }
    }
}
