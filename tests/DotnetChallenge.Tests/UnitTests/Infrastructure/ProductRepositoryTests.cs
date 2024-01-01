using AutoMapper;
using DotnetChallenge.Domain.Entities;
using DotnetChallenge.Infrastructure.Contexts;
using DotnetChallenge.Infrastructure.DataEntities;
using DotnetChallenge.Infrastructure.Mappings;
using DotnetChallenge.Infrastructure.Persistences;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetChallenge.Tests.UnitTests.Infrastructure
{
    public class ProductRepositoryTests
    {
        private DbContextOptions<ApplicationDbContext> _options;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProductRepositoryTests() 
        {
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
                .Options;
            
            _context = new ApplicationDbContext(_options);

            MapperConfiguration mapperConfiguration = new MapperConfiguration(c =>
            {
                c.AddProfile<AutoMapperProfile>();
            });
            _mapper = mapperConfiguration.CreateMapper();

        }

        [Fact]
        public async Task FindAllAsync_WithExistingItems_ReturnListOfProducts()
        {
            // Arrange
            var products = GetProducts();

            _context.Products.AddRange(products);
            _context.SaveChanges();

            var repository = new ProductRepository(_context, _mapper);

            // Act
            var actualItems = await repository.FindAllAsync();

            // Assert
            actualItems.Should().NotBeNull();
            actualItems.Should().BeEquivalentTo(
                products,
                options => options.ComparingByMembers<Product>()
            );
        }

       

        [Fact]
        public async Task FindAllAsync_WithUnexistingItems_ReturnEmptyList()
        {
            // Arrange
            var repository = new ProductRepository(_context, _mapper);

            // Act
            var actualItems = await repository.FindAllAsync();

            // Assert
            actualItems.Should().BeNullOrEmpty();
        }

        [Fact]
        public async Task FindByIdAsync_WithExistingItem_ReturnExpectedItem()
        {
            // Arrange
            var products = GetProducts();
            var repository = new ProductRepository(_context, _mapper);

            // Act
            var actualProduct = await repository.FindByIdAsync(1);

            // Assert
            actualProduct.Should().NotBeNull();
            actualProduct.Should().BeEquivalentTo
                (
                    products[0],
                    options => options.ComparingByMembers<Product>()
                );
        }

        [Fact]
        public async Task FindByIdAsync_WithUnexistingItem_ReturnNoFound()
        {
            // Arrange
            var repository = new ProductRepository(_context, _mapper);

            // Act
            var actualProduct = await repository.FindByIdAsync(1);

            // Assert
            actualProduct.Should().BeNull();
        }

        [Fact]
        public async Task SaveAsync_WithValidItem_ReturnProductSaved()
        {
            // Arrange
            var product = new Product() 
            {
                 Id = 5,
                 Name="Car",
                 Description="",
                 Price=25m,
                 Status=1,
                 Stock=15
            };
            var repository = new ProductRepository(_context, _mapper);

            // Act
            var actualProduct = await repository.SaveAsync(product);

            // Assert
            actualProduct.Should().NotBeNull();
            actualProduct.Should().BeEquivalentTo
                (
                    product,
                    options => options.ComparingByMembers<Product>()
                );
        }

        private List<ProductEntity> GetProducts()
        {
            return new List<ProductEntity>
            {
                new ProductEntity { Id = 1, Name= "Coca-Cola", Description="Soda", Price=5.5m, Status=1, Stock=25},
                new ProductEntity { Id = 2, Name= "Inka-Cola", Description="Soda", Price=4.5m, Status=1, Stock=20},
                new ProductEntity { Id = 3, Name= "Fanta", Description="Soda", Price=3.5m, Status=1, Stock=30},
            };
        }
    }
}
