using DotnetChallenge.Api.Controllers;
using DotnetChallenge.Application.Commands;
using DotnetChallenge.Application.Dtos;
using DotnetChallenge.Application.Queries;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace DotnetChallenge.Tests.UnitTests.Controller
{
    public class ProductControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;

        public ProductControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
        }

        [Fact]
        public async Task GetProducts_WithUnexistingItems_ReturnOKAndEmptyList()
        {
            // Arrange
            _mediatorMock.Setup(mediator => mediator.Send(It.IsAny<GetAllProductsQuery>(), default))
                        .ReturnsAsync(new List<ProductDto>());


            var controller = new ProductsController(_mediatorMock.Object);

            // Act 
            var result = await controller.GetProducts();

            // Asset
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo(new List<ProductDto>());
        }

        [Fact]
        public async Task GetProducts_WithExistingItems_ReturnOKAndList()
        {
            // Arrange
            var products = new List<ProductDto>() {
                             new ProductDto(){ Id= 1, Name="Product1", Price=12.5m}
                        };

            _mediatorMock.Setup(mediator => mediator.Send(It.IsAny<GetAllProductsQuery>(), default))
                        .ReturnsAsync(products);

            var controller = new ProductsController(_mediatorMock.Object);

            // Act 
            var result = await controller.GetProducts();

            // Asset
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo
                (
                    products,
                    options => options.ComparingByMembers<ProductDto>()
                );
        }

        [Fact]
        public async Task CreateProduct_WithValidItem_ReturnProductDto()
        {
            // Arrange
            var productDto = new ProductDto() {
                Id = 1,
                Name = "Producto",
                Price = 15.2m,
            };

            _mediatorMock.Setup(mediator => mediator.Send(It.IsAny<CreateProductCommand>(), default))
                        .ReturnsAsync(productDto);

            var command = new CreateProductCommand()
            {
                Name = "Producto",
                Description = "Descripcion",
                Price = 15.2m,
                Status = 1,
                Stock = 25
            };

            var controller = new ProductsController(_mediatorMock.Object);

            // Act 
            var result = await controller.CreateProduct(command);

            // Asset
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        }

        [Fact]
        public async Task UpdateProduct_WithValidItem_ReturnProductDto()
        {
            // Arrange
            var productDto = new ProductDto()
            {
                Id = 1,
                Name = "Producto",
                Price = 15.2m,
            };

            _mediatorMock.Setup(mediator => mediator.Send(It.IsAny<UpdateProductCommand>(), default))
                        .ReturnsAsync(productDto);

            var command = new UpdateProductCommand()
            {
                Id= 1,
                Name = "Producto",
                Description = "Descripcion",
                Price = 15.2m,
                Status = 1,
                Stock = 25
            };

            var controller = new ProductsController(_mediatorMock.Object);

            // Act 
            var result = await controller.UpdateProduct(command);

            // Asset
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        }

        [Fact]
        public async Task GetProductById_WithUnexistingItem_ReturnNotFound()
        {
            // Arrange
            _mediatorMock.Setup(mediator => mediator.Send(It.IsAny<GetProductByIdQuery>(), default))
                        .ReturnsAsync((ProductDto)null);

            var query = new GetProductByIdQuery()
            {
                Id = 1
            };

            var controller = new ProductsController(_mediatorMock.Object);

            // Act 
            var result = await controller.GetProductById(query);

            // Asset
            var notFoundResult = result.Should().BeOfType<NotFoundResult>().Subject;
        }

        [Fact]
        public async Task GetProductById_WithExistingItem_ReturnOK()
        {
            // Arrange
            var product = new ProductDto() { Id = 1, Name = "Product1", Price = 12.5m };

            _mediatorMock.Setup(mediator => mediator.Send(It.IsAny<GetProductByIdQuery>(), default))
                        .ReturnsAsync(product);

            var controller = new ProductsController(_mediatorMock.Object);

            // Act 
            var result = await controller.GetProducts();

            // Asset
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        }
    }
}
