using DotnetChallenge.Api.Controllers;
using DotnetChallenge.Application.Dtos;
using DotnetChallenge.Application.Queries;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
