using DotnetChallenge.Application.Dtos;
using MediatR;

namespace DotnetChallenge.Application.Queries
{
    public class GetProductByIdQuery : IRequest<ProductDto>
    {
        public int Id { get; set; }
    }
}
