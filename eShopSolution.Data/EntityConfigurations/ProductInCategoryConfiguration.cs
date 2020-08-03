using eShopSolution.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.Data.EntityConfigurations
{
    class ProductInCategoryConfiguration : IEntityTypeConfiguration<ProductInCategory>
    {
        public void Configure(EntityTypeBuilder<ProductInCategory> builder)
        {
            builder.ToTable("ProductInCategories");
            builder.HasKey(t => new { t.CategoryId, t.ProductId });
            builder.HasOne(t => t.Product).WithMany(pc => pc.ProductInCategories)
                .HasForeignKey(pc => pc.ProductId);
            builder.HasOne(t => t.category).WithMany(pc => pc.ProductInCategories)
                .HasForeignKey(pc => pc.CategoryId);
        }
    }
}
