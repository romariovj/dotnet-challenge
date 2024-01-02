using AutoMapper;
using DotnetChallenge.Domain.Entities;
using DotnetChallenge.Domain.Repositories;
using DotnetChallenge.Infrastructure.Configurations;
using DotnetChallenge.Infrastructure.Persistences.Contexts;
using DotnetChallenge.Infrastructure.Persistences.DataEntities;

namespace DotnetChallenge.Infrastructure.Persistences
{
    public class ProductRepository : CrudRepository<Product, ProductEntity, int>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
