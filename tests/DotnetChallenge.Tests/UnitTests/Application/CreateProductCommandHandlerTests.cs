using AutoMapper;
using DotnetChallenge.Application.Commands;
using DotnetChallenge.Application.Handlers;
using DotnetChallenge.Application.Mappings;
using DotnetChallenge.Domain.Entities;
using DotnetChallenge.Domain.Repositories;
using FluentAssertions;
using Moq;

namespace DotnetChallenge.Tests.UnitTests.Application
{
    public class CreateProductCommnadHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IProductRepository> _productRepositoryMock = new Mock<IProductRepository>();

        public CreateProductCommnadHandlerTests()
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(c =>
            {
                c.AddProfile<TestAutoMapperProfile>();
            });

            _mapper = mapperConfiguration.CreateMapper();
        }

        [Fact]
        public async Task Handle_WithValidItem_ReturnProductDto()
        {
            // Arrange
            var domainProduct = new Product() 
            {
                Id=1,
                Name= "Producto",
                Description="Descripcion",
                Price=15.2m,
                Status=new ProductStatus()
                {
                    Id=1,
                    Name="Active"
                },
                Stock=25
            };

            _productRepositoryMock.Setup(repository => repository.CreateAsync(It.IsAny<Product>()))
                .ReturnsAsync(domainProduct);

            var command = new CreateProductCommand()
            {
                Name = "Producto",
                Description = "Descripcion",
                Price = 15.2m,
                Status = 1,
                Stock = 25
            };

            var handler = new CreateProductCommandHandler(_productRepositoryMock.Object, _mapper);

            // Act
            var expectedProduct = await handler.Handle(command, CancellationToken.None);

            // Assert
            expectedProduct.Should().NotBeNull();
        }
    }
}
