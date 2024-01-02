using AutoMapper;
using DotnetChallenge.Application.Mappings;
using DotnetChallenge.Application.UpdateProduct.Commands;
using DotnetChallenge.Application.UpdateProduct.Handlers;
using DotnetChallenge.Domain.Entities;
using DotnetChallenge.Domain.Repositories;
using FluentAssertions;
using Moq;

namespace DotnetChallenge.Tests.UnitTests.Application
{
    public class UpdateProductCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IProductRepository> _productRepositoryMock = new Mock<IProductRepository>();

        public UpdateProductCommandHandlerTests()
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(c =>
            {
                c.AddProfile<TestAutoMapperProfile>();
            });

            _mapper = mapperConfiguration.CreateMapper();
        }

        [Fact]
        public async Task Handle_WithValidItem_ReturnUpdatedProductDto()
        {
            // Arrange
            var domainProduct = new Product()
            {
                Id = 1,
                Name = "Producto Modificado",
                Description = "Descripcion Modificado",
                Price = 15.2m,
                Status = new ProductStatus() { Id = 1, Name = "Active" },
                Stock = 25
            };

            _productRepositoryMock.Setup(repository => repository.UpdateAsync(It.IsAny<Product>()))
                .ReturnsAsync(domainProduct);

            var command = new UpdateProductCommand()
            {
                Id=1,
                Name = "Producto Modificado",
                Description = "Descripcion Modificado",
                Price = 15.2m,
                Status = 1,
                Stock = 25
            };

            var handler = new UpdateProductCommandHandler(_productRepositoryMock.Object, _mapper);

            // Act
            var expectedProduct = await handler.Handle(command, CancellationToken.None);

            // Assert
            expectedProduct.Should().NotBeNull();
        }
    }
}
