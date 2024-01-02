using AutoMapper;
using DotnetChallenge.Application.Dtos;
using DotnetChallenge.Application.Handlers;
using DotnetChallenge.Application.Mappings;
using DotnetChallenge.Application.Queries;
using DotnetChallenge.Domain.Entities;
using DotnetChallenge.Domain.Repositories;
using FluentAssertions;
using Moq;

namespace DotnetChallenge.Tests.UnitTests.Application
{
    public class GetAllProductsQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IProductRepository> _productRepositoryMock = new Mock<IProductRepository>();

        public GetAllProductsQueryHandlerTests() 
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(c =>
            {
                c.AddProfile<TestAutoMapperProfile>();
            });

            _mapper = mapperConfiguration.CreateMapper();
        }

        [Fact]
        public async Task Handle_WithUnexistingItems_ReturnEmptyListProductDtos()
        {
            // Arrange
            _productRepositoryMock.Setup(repository => repository.FindAllAsync())
                .ReturnsAsync(GetEmptyProducts);

            var handler = new GetAllProductsQueryHandler(_productRepositoryMock.Object, _mapper);

            // Act
            var products = await handler.Handle(new GetAllProductsQuery(), CancellationToken.None);

            // Assert
            products.Should().BeNullOrEmpty();
            products.Should().HaveCount(0);
        }

        [Fact]
        public async Task Handle_WithExistingItems_ReturnListProductDtos()
        {
            // Arrange
            _productRepositoryMock.Setup(repository => repository.FindAllAsync())
                .ReturnsAsync(GetProducts);

            var handler = new GetAllProductsQueryHandler(_productRepositoryMock.Object, _mapper);

            // Act
            var products = await handler.Handle(new GetAllProductsQuery(), CancellationToken.None);

            // Assert
            products.Should().NotBeNullOrEmpty();
            products.Should().HaveCount(3);
        }

        private List<Product> GetProducts()
        {
            return new List<Product>
            {
                new Product { Id = 1, Name= "Coca-Cola", Description="Soda", Price=5.5m, Status= new ProductStatus() { Id=1, Name="Active"}, Stock=25},
                new Product { Id = 2, Name= "Inka-Cola", Description="Soda", Price=4.5m, Status= new ProductStatus() { Id=1, Name="Active"}, Stock=20},
                new Product { Id = 3, Name= "Fanta", Description="Soda", Price=3.5m, Status= new ProductStatus() { Id=1, Name="Active"}, Stock=30},
            };
        }

        private List<Product> GetEmptyProducts()
        {
            return new List<Product>();
        }

        private List<ProductDto> GetProductDtos()
        {
            return new List<ProductDto>
            {
                new ProductDto { ProductId = 1, Name= "Coca-Cola", Price=5.5m},
                new ProductDto { ProductId = 2, Name= "Inka-Cola", Price=4.5m},
                new ProductDto { ProductId = 3, Name= "Fanta", Price=3.5m},
            };
        }
    }
}
