using DotnetChallenge.Application.Dtos;
using MediatR;

namespace DotnetChallenge.Application.GetProductById.Queries
{
    public class GetProductByIdQuery : IRequest<ProductDto>
    {
        public int Id { get; set; }
    }
}
