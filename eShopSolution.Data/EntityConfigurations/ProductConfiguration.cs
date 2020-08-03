using eShopSolution.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;


//public int id { get; set; }
//public decimal price { get; set; }
//public decimal originalprice { get; set; }
//public int stock { get; set; }
//public int viewcount { get; set; }
//public datetime datecreated { get; set; }
//public string seoalias { get; set; }
//public list<productincategory> productincategories { get; set; }

namespace eShopSolution.Data.EntityConfigurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.OriginalPrice).IsRequired();
            builder.Property(x => x.Stock).IsRequired().HasDefaultValue(0);
            builder.Property(x => x.ViewCount).IsRequired().HasDefaultValue(0);
            builder.Property(x => x.SeoAlias);
        }
    }
}
