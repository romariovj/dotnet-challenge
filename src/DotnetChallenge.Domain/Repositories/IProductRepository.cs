using DotnetChallenge.Domain.Entities;

namespace DotnetChallenge.Domain.Repositories
{
    public interface IProductRepository: ICrudRepository<Product, int>
    {
    }
}
