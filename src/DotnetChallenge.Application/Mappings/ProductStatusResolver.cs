using AutoMapper;
using DotnetChallenge.Application.Dtos;
using DotnetChallenge.Application.Services;
using DotnetChallenge.Domain.Entities;

namespace DotnetChallenge.Application.Mappings
{
    public class ProductStatusResolver : IValueResolver<Product, ProductDto, string>
    {
        private readonly IProductStatusCacheService _productStatusCacheService;
        public ProductStatusResolver(IProductStatusCacheService productStatusCacheService) 
        {
            _productStatusCacheService = productStatusCacheService;
        }

        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            Dictionary<int, string> statusList = _productStatusCacheService.GetCachedProductStatus();
            bool hasItem = statusList.TryGetValue(source.Status.Id, out string statusName);
            return hasItem ? statusName:"";
        }
    }
}
