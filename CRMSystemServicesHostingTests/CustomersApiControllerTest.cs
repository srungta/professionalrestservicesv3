using Microsoft.AspNetCore.Mvc;
using Microsoft.Libraries.Api.Controllers.Impl;
using Microsoft.Libraries.Business.Interfaces;
using Microsoft.Libraries.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CRMSystemServicesHostingTests
{
    public class UnitTest1
    {
        [Fact]
        public void ShouldGetCustomersReturnCustomerRecords()
        {
            var mockRepository = new MockRepository(MockBehavior.Default);
            var mockCustomers = new List<Customer>
            {
                new Customer { CustomerId = 11, CustomerName = "Northwind", Address = "Bangalore", CreditLimit = 23000, ActiveStatus = true, EmailId = "info@email.com", PhoneNumber = "080-49834343", Remarks = "Simple" },
                new Customer { CustomerId = 12, CustomerName = "Southwind", Address = "Bangalore", CreditLimit = 23000, ActiveStatus = true, EmailId = "info@email.com", PhoneNumber = "080-49834343", Remarks = "Simple" },
                new Customer { CustomerId = 13, CustomerName = "Westwind", Address = "Bangalore", CreditLimit = 23000, ActiveStatus = true, EmailId = "info@email.com", PhoneNumber = "080-49834343", Remarks = "Simple" },
                new Customer { CustomerId = 14, CustomerName = "Eastwind", Address = "Bangalore", CreditLimit = 23000, ActiveStatus = true, EmailId = "info@email.com", PhoneNumber = "080-49834343", Remarks = "Simple" },
                new Customer { CustomerId = 15, CustomerName = "Oxyrich", Address = "Bangalore", CreditLimit = 23000, ActiveStatus = true, EmailId = "info@email.com", PhoneNumber = "080-49834343", Remarks = "Simple" }
            };

            var mockCustomersBusinessComponent = mockRepository.Create<ICustomersBusinessComponent>();

            mockCustomersBusinessComponent
                .Setup(businessComponent => businessComponent.GetCustomers(default(string), default(string)))
                .Returns(mockCustomers);

            var customersApiController = new CustomersApiController(mockCustomersBusinessComponent.Object);
            var result = customersApiController.GetCustomers() as OkObjectResult;
            var customersList = result.Value as IEnumerable<Customer>;

            Assert.NotNull(result);
            Assert.NotNull(customersList);

            var expectedNoOfCustomers = 5;
            var actualNoOfCustomers = customersList.Count();

            Assert.Equal<int>(expectedNoOfCustomers, actualNoOfCustomers);
        }
    }
}
