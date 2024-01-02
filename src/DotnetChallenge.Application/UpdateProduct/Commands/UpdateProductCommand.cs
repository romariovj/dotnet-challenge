using DotnetChallenge.Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DotnetChallenge.Application.UpdateProduct.Commands
{
    public class UpdateProductCommand : IRequest<ActionResult<ProductDto>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
