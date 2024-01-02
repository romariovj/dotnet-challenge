using DotnetChallenge.Application.CreateProduct.Commands;
using DotnetChallenge.Application.Dtos;
using DotnetChallenge.Application.GetAllProducts.Queries;
using DotnetChallenge.Application.GetProductById.Queries;
using DotnetChallenge.Application.UpdateProduct.Commands;
using DotnetChallenge.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DotnetChallenge.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [SwaggerTag("Operaciones CRUD para productos")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator) 
        {
            _mediator = mediator;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Obtener todos los productos", Description = "Obtiene la lista completa de productos.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Lista de productos", typeof(List<ProductDto>))]
        public async Task<IActionResult> GetProducts()
        {
            var query = new GetAllProductsQuery();
            var products = await _mediator.Send(query);
            return Ok(products);
        }

        [HttpGet("{Id}")]
        [SwaggerOperation(Summary = "Obtener un producto por ID", Description = "Obtiene un producto por su ID.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Producto encontrado", typeof(ProductDto))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Producto no encontrado")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Solicitud no válida")]
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
        [SwaggerOperation(Summary = "Crear un nuevo producto", Description = "Crea un nuevo producto.")]
        [SwaggerResponse(StatusCodes.Status201Created, "Producto creado", typeof(ProductDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Solicitud no válida")]
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
        [SwaggerOperation(Summary = "Actualizar un producto existente", Description = "Actualiza un producto existente por su ID.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Producto actualizado", typeof(ProductDto))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Producto no encontrado")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Solicitud no válida")]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductCommand command)
        {
            var result = await _mediator.Send(command);

            if (result is ActionResult<ProductDto> actionResult && actionResult.Result is BadRequestObjectResult badRequestResult)
            {
                return badRequestResult;
            }

            if (result.Value is null)
            {
                return NotFound();
            }

            return Ok(result.Value);
        }

    }
}
