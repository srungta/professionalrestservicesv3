using Microsoft.EntityFrameworkCore;
using Microsoft.Libraries.Models;
using Microsoft.Libraries.ORM.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Libraries.ORM.Impl
{
    public class CustomersContext : DbContext, ICustomersContext
    {
        private const int MIN_ROWS = 1;
        public CustomersContext(
            DbContextOptions<CustomersContext> customersDbContextOptions)
            : base(customersDbContextOptions) { }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration<Customer>(
                new CustomerEntityTypeConfiguration());
        }


        public bool CommitChanges()
        {
            var noOfRowsAffected = this.SaveChanges();

            return noOfRowsAffected >= MIN_ROWS;
        }
    }
}
