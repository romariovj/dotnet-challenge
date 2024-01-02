using DotnetChallenge.Application.Dtos;
using MediatR;

namespace DotnetChallenge.Application.GetAllProducts.Queries
{
    public class GetAllProductsQuery : IRequest<List<ProductDto>>
    {
    }
}
