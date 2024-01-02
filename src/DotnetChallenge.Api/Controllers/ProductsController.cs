using DotnetChallenge.Application.CreateProduct.Commands;
using DotnetChallenge.Application.Dtos;
using DotnetChallenge.Application.GetAllProducts.Queries;
using DotnetChallenge.Application.GetProductById.Queries;
using DotnetChallenge.Application.UpdateProduct.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DotnetChallenge.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator) 
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var query = new GetAllProductsQuery();
            var products = await _mediator.Send(query);
            return Ok(products);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetProductById([FromRoute] GetProductByIdQuery query)
        {
            ProductDto? product = await _mediator.Send(query);

            if(product is null)
            {
                return NotFound();
            }

            return Ok(product);
        }


        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
        {
            var result = await _mediator.Send(command);

            if (result is ActionResult<ProductDto> actionResult && actionResult.Result is BadRequestObjectResult badRequestResult)
            {
                return badRequestResult;
            }

            return CreatedAtAction(nameof(CreateProduct), new { id = result.Value.ProductId}, result.Value);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductCommand command)
        {
            var result = await _mediator.Send(command);

            if (result is ActionResult<ProductDto> actionResult && actionResult.Result is BadRequestObjectResult badRequestResult)
            {
                return badRequestResult;
            }

            return Ok(result.Value);
        }

    }
}
