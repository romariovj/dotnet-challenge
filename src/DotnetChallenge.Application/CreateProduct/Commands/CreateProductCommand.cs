using DotnetChallenge.Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DotnetChallenge.Application.CreateProduct.Commands
{
    public class CreateProductCommand : IRequest<ActionResult<ProductDto>>
    {
        public string Name { get; set; }
        public int Status { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
