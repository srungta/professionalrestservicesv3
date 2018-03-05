using Microsoft.AspNetCore.Mvc;
using Microsoft.Libraries.Api.Controllers.Interfaces;
using Microsoft.Libraries.Business.Interfaces;
using Microsoft.Libraries.Models;
using System;
using System.Collections.Generic;

namespace Microsoft.Libraries.Api.Controllers.Impl
{
    /// <summary>
    /// A Service that provides Customers Information connecting to CRM System.
    /// </summary>
    [Produces("application/json")]
    [Route("api/customers")]
    public class CustomersApiController : Controller, ICustomersApiController
    {
        private const string INVALID_CUSTOMERS_BUSINESS_COMPONENT = "Invalid Customers Business Component Specified!";
        private const string INVALID_CUSTOMER_RECORD = "Invalid Customer Record Specified!";

        private ICustomersBusinessComponent customersBusinessComponent = default(ICustomersBusinessComponent);


        /// <summary>
        /// Constructor which accepts business dependencies
        /// </summary>
        /// <param name="customersBusinessComponent">Customers Busines Component Dependency</param>
        public CustomersApiController(ICustomersBusinessComponent customersBusinessComponent)
        {
            if (customersBusinessComponent == default(ICustomersBusinessComponent))
                throw new ArgumentException(INVALID_CUSTOMERS_BUSINESS_COMPONENT);

            this.customersBusinessComponent = customersBusinessComponent;
        }

        /// <summary>
        /// An operation that helps to find customer records by customer name
        /// </summary>
        /// <param name="customerName">Search String</param>
        /// <returns>Array of Customer Records</returns>
        [HttpGet]
        [Route("search/{customerName}")]
        public IActionResult FindCustomers(string customerName)
        {
            var filteredCustomers = default(IEnumerable<Customer>);

            if (string.IsNullOrEmpty(customerName))
                return BadRequest();

            filteredCustomers = this.customersBusinessComponent.GetCustomers(customerName);

            return Ok(filteredCustomers);
        }

        /// <summary>
        /// An operation that finds customer records by address
        /// </summary>
        /// <param name="address">Address String</param>
        /// <returns>An array of customer records</returns>
        [HttpGet]
        [Route("search/address/{address}")]
        public IActionResult FindCustomersByAddress(string address)
        {
            var filteredCustomers = default(IEnumerable<Customer>);

            if (string.IsNullOrEmpty(address))
                return BadRequest();

            filteredCustomers =
                this.customersBusinessComponent.GetCustomers(default(string), address);

            return Ok(filteredCustomers);
        }
    
        /// <summary>
        /// An operation that finds a customer record by customer id
        /// </summary>
        /// <param name="customerId">Valid Customer Id</param>
        /// <returns>Complete Customer Profile</returns>
        [HttpGet]
        [Route("details/{customerId}")]
        public IActionResult GetCustomer(int customerId)
        {
            var filteredCustomer = default(Customer);
            var validation = customerId != default(int);

            if (!validation)
                return BadRequest();

            filteredCustomer = this.customersBusinessComponent.GetCustomer(customerId);

            return Ok(filteredCustomer);
        }


        /// <summary>
        /// An operation that retrieves all customer records.
        /// </summary>
        /// <returns>An array of customer records</returns>
        [HttpGet]
        public IActionResult GetCustomers()
        {
            var customers = this.customersBusinessComponent.GetCustomers();

            return Ok(customers);
        }

        /// <summary>
        /// An operation that allows to save a new customer
        /// </summary>
        /// <param name="customer">New Customer Record</param>
        /// <returns>Saved and Valid Customer Record</returns>
        [HttpPost]
        public IActionResult SaveCustomer([FromBody] Customer customer)
        {
            var validation = customer != default(Customer);

            if (!validation)
                return BadRequest();

            var savedCustomer = this.customersBusinessComponent.SaveCustomer(customer);

            if (savedCustomer == default(Customer))
                throw new ApplicationException(INVALID_CUSTOMER_RECORD);

            return Ok();
        }
    }
}
