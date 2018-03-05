using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Libraries.Models;

namespace Microsoft.Libraries.ORM.Impl
{
    public class CustomerEntityTypeConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(model => model.CustomerId);

            builder.Property(model => model.CustomerId)
                    .UseSqlServerIdentityColumn();

            builder.ToTable("Customers");
        }
    }
}