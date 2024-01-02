using DotnetChallenge.Infrastructure.Persistences.DataEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotnetChallenge.Infrastructure.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<ProductEntity>
    {
        public void Configure(EntityTypeBuilder<ProductEntity> builder)
        {
            builder.ToTable("product");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnName("id").UseIdentityColumn();
            builder.Property(p => p.Name).HasColumnName("name").HasMaxLength(255).IsRequired();
            builder.Property(p => p.Status).HasColumnName("status").HasColumnType("int");
            builder.Property(p => p.Stock).HasColumnName("stock").HasColumnType("int");
            builder.Property(p => p.Description).HasColumnName("description").HasColumnType("NVARCHAR(MAX)").IsRequired();
            builder.Property(p => p.Price).HasColumnName("price").HasColumnType("decimal(18,2)").IsRequired();
        }
    }
}
