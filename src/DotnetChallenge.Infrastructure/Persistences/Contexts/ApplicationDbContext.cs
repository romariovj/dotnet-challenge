﻿using DotnetChallenge.Infrastructure.Persistences.DataEntities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DotnetChallenge.Infrastructure.Persistences.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<ProductEntity> Products { get; set; }
    }
}
