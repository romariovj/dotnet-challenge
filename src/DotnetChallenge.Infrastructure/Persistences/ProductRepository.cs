using AutoMapper;
using DotnetChallenge.Domain.Entities;
using DotnetChallenge.Domain.Repositories;
using DotnetChallenge.Infrastructure.Configurations;
using DotnetChallenge.Infrastructure.Contexts;
using DotnetChallenge.Infrastructure.DataEntities;

namespace DotnetChallenge.Infrastructure.Persistences
{
    public class ProductRepository : CrudRepository<Product, ProductEntity, int>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
