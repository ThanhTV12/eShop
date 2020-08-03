using eShopSolution.Data.Entities;
using eShopSolution.Data.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.Data.EntityConfigurations
{
    class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transaction");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).UseIdentityColumn();
            builder.Property(t => t.Status).HasDefaultValue(TransactionStatus.Success);
            builder.HasOne(t => t.AppUser).WithMany(au => au.Transactions).HasForeignKey(t => t.UserId);
        }
    }
}
