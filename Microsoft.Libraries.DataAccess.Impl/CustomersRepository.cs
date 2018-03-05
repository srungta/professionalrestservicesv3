using Microsoft.Libraries.DataAccess.Interfaces;
using Microsoft.Libraries.Models;
using Microsoft.Libraries.ORM.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Libraries.DataAccess.Impl
{
    public class CustomersRepository : ICustomersRepository
    {
        private const string INVALID_CUSTOMERS_CONTEXT = "Invalid Customer(s) Context Specified!";
        private const string INVALID_ARGUMENTS = @"Invalid Argument(s) Specified!";

        private ICustomersContext customersContext = default(ICustomersContext);

        public CustomersRepository(ICustomersContext customersContext)
        {
            if (customersContext == default(ICustomersContext))
                throw new ArgumentException(INVALID_CUSTOMERS_CONTEXT);

            this.customersContext = customersContext;
        }

        public bool AddNewEntity(Customer entityType)
        {
            var status = default(bool);
            var validation = entityType != default(Customer) &&
                this.customersContext != default(ICustomersContext);

            if (!validation)
                throw new ArgumentException(INVALID_ARGUMENTS);

            this.customersContext.Customers.Add(entityType);
            status = this.customersContext.CommitChanges();

            return status;
        }

        public void Dispose() => this.customersContext?.Dispose();

        public IEnumerable<Customer> GetAllEntities()
        {
            var customersList = this.customersContext
                .Customers
                .ToList();

            return customersList;
        }

        public IEnumerable<Customer> GetCustomersByAddress(string address)
        {
            var validation = !string.IsNullOrEmpty(address) &&
                this.customersContext != default(ICustomersContext);

            if (!validation)
                throw new ArgumentException(INVALID_ARGUMENTS);

            var filteredCustomers =
                this.customersContext
                    .Customers
                    .Where(customer => customer.Address.Contains(address))
                    .ToList();

            return filteredCustomers;
        }

        public IEnumerable<Customer> GetCustomersByName(string customerName)
        {
            var validation = !string.IsNullOrEmpty(customerName) &&
                this.customersContext != default(ICustomersContext);

            if (!validation)
                throw new ArgumentException(INVALID_ARGUMENTS);

            var filteredCustomers =
                this.customersContext
                    .Customers
                    .Where(customer => customer.CustomerName.Contains(customerName))
                    .ToList();

            return filteredCustomers;
        }

        public Customer GetEntityByKey(int entityKey)
        {
            var filteredCustomer = default(Customer);
            var validation = entityKey != default(int) &&
                this.customersContext != default(ICustomersContext);

            if (!validation)
                throw new ArgumentException(INVALID_ARGUMENTS);

            filteredCustomer =
                this.customersContext
                .Customers
                .Where(customer => customer.CustomerId.Equals(entityKey))
                .FirstOrDefault();

            return filteredCustomer;
        }
    }
}
