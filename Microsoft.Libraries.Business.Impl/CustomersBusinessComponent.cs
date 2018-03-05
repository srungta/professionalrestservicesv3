using Microsoft.Libraries.Business.Interfaces;
using Microsoft.Libraries.Business.Validations.Interfaces;
using Microsoft.Libraries.DataAccess.Interfaces;
using Microsoft.Libraries.Models;
using System;
using System.Collections.Generic;

namespace Microsoft.Libraries.Business.Impl
{
    public class CustomersBusinessComponent : ICustomersBusinessComponent
    {
        private const string INVALID_DEPENDENCIES = @"Invalid Customers Repository or Business Validations Specified!";
        private const string BUSINESS_VALIDAION_FAILED = "Business Validation Failed!";
        private const string INVALID_ARGUMENTS = "Invalid Argument(s) Specified!";

        private ICustomersRepository customersRepository = default(ICustomersRepository);
        private IBusinessValidation<string> customerNameValidation = default(IBusinessValidation<string>);
        private IBusinessValidation<Customer> customerValidation = default(IBusinessValidation<Customer>);
        public CustomersBusinessComponent(ICustomersRepository customersRepository,
            IBusinessValidation<string> customerNameValidation,
            IBusinessValidation<Customer> customerValidation)
        {
            var validation = customersRepository != default(ICustomersRepository) &&
                customerNameValidation != default(IBusinessValidation<string>) &&
                customerValidation != default(IBusinessValidation<Customer>);

            if (!validation)
                throw new ArgumentException(INVALID_DEPENDENCIES);

            this.customersRepository = customersRepository;
            this.customerNameValidation = customerNameValidation;
            this.customerValidation = customerValidation;
        }
        public void Dispose() => this.customersRepository?.Dispose();

        public IEnumerable<Customer> GetCustomers(
            string customerName = default(string), string customerAddress = default(string))
        {
            var filteredCustomers = default(IEnumerable<Customer>);

            if (string.IsNullOrEmpty(customerName) &&
                string.IsNullOrEmpty(customerAddress))
                filteredCustomers = this.customersRepository.GetAllEntities();
            else
            {
                if (!string.IsNullOrEmpty(customerName))
                {
                    var validationStatus = this.customerNameValidation.Validate(customerName);

                    if (!validationStatus)
                        throw new ApplicationException(BUSINESS_VALIDAION_FAILED);

                    filteredCustomers = this.customersRepository.GetCustomersByName(customerName);
                }

                if (!string.IsNullOrEmpty(customerAddress))
                    filteredCustomers = this.customersRepository.GetCustomersByAddress(customerAddress);
            }

            return filteredCustomers;
        }

        public Customer GetCustomer(int customerId)
        {
            var filteredCustomer = default(Customer);
            var validation = customerId != default(int);

            if (!validation)
                throw new ArgumentException(INVALID_ARGUMENTS);

            filteredCustomer = this.customersRepository.GetEntityByKey(customerId);

            return filteredCustomer;
        }

        public Customer SaveCustomer(Customer customer)
        {
            var savedCustomer = default(Customer);
            var validation = customer != default(Customer) &&
                this.customerValidation.Validate(customer);

            if (!validation)
                throw new ApplicationException(BUSINESS_VALIDAION_FAILED);

            var saveStatus = this.customersRepository.AddNewEntity(customer);

            savedCustomer = saveStatus ? customer : default(Customer);

            return savedCustomer;
        }
    }
}
