using DotnetChallenge.Application.Dtos;
using MediatR;

namespace DotnetChallenge.Application.Commands
{
    public class UpdateProductCommand : IRequest<ProductDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
