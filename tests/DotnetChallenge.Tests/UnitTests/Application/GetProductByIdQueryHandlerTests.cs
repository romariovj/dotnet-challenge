using AutoMapper;
using DotnetChallenge.Application.Handlers;
using DotnetChallenge.Application.Mappings;
using DotnetChallenge.Application.Queries;
using DotnetChallenge.Domain.Entities;
using DotnetChallenge.Domain.Repositories;
using FluentAssertions;
using Moq;

namespace DotnetChallenge.Tests.UnitTests.Application
{
    public class GetProductByIdQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IProductRepository> _productRepositoryMock = new Mock<IProductRepository>();

        public GetProductByIdQueryHandlerTests()
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(c =>
            {
                c.AddProfile<AutoMapperProfile>();
            });

            _mapper = mapperConfiguration.CreateMapper();
        }

        [Fact]
        public async Task Handle_WithUnexistingItems_ReturnNull()
        {
            // Arrange
            _productRepositoryMock.Setup(repository => repository.FindByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Product)null);

            var handler = new GetProductByIdQueryHandler(_productRepositoryMock.Object, _mapper);
            var query = new GetProductByIdQuery() { Id = 1 };

            // Act
            var expectedProduct = await handler.Handle(query, CancellationToken.None);

            // Assert
            expectedProduct.Should().BeNull();
        }

        [Fact]
        public async Task Handle_WithExistingItems_ReturnProduct()
        {
            // Arrange
            var domainProduct = new Product()
            {
                Id=1,
                Name="Product",
                Description="Descripcion",
                Price=15m,
                Status=0,
                Stock=14
            };

            _productRepositoryMock.Setup(repository => repository.FindByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(domainProduct);

            var handler = new GetProductByIdQueryHandler(_productRepositoryMock.Object, _mapper);
            var query = new GetProductByIdQuery() { Id = 1 };

            // Act
            var expectedProduct = await handler.Handle(query, CancellationToken.None);

            // Assert
            expectedProduct.Should().NotBeNull();
        }

    }
}
