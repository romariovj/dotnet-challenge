using DotnetChallenge.Application.Dtos;
using MediatR;

namespace DotnetChallenge.Application.Queries
{
    public class GetAllProductsQuery : IRequest<List<ProductDto>>
    {
    }
}
