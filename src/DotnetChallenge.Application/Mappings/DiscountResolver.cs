using AutoMapper;
using DotnetChallenge.Application.Dtos;
using DotnetChallenge.Domain.Entities;
using DotnetChallenge.Domain.Interfaces;

namespace DotnetChallenge.Application.Mappings
{
    public class DiscountResolver : IValueResolver<Product, ProductDto, decimal>
    {
        private readonly IDiscountService _discountSeervice;
        public DiscountResolver(IDiscountService discountSeervice) 
        {
            _discountSeervice = discountSeervice;
        }

        public decimal Resolve(Product source, ProductDto destination, decimal destMember, ResolutionContext context)
        {
            int discount = _discountSeervice.GetDiscountPercentageAsync(destination.ProductId).Result;
            return Convert.ToDecimal(discount);
        }
    }
}
