using AutoMapper;
using DotnetChallenge.Application.Dtos;
using DotnetChallenge.Domain.Entities;

namespace DotnetChallenge.Application.Mappings
{
    public class FakeProductStatusResolver : IValueResolver<Product, ProductDto, string>
    {
        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            Dictionary<int, string> statusList = new Dictionary<int, string>() {
                { 1, "Active" },
                { 0, "Inactive" }
            };
            return statusList[source.Status.Id];
        }
    }
}
