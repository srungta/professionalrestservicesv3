using Microsoft.AspNetCore.Mvc;
using Microsoft.Libraries.Models;
using System;

namespace Microsoft.Libraries.Api.Controllers.Interfaces
{
    public interface ICustomersApiController : IDisposable
    {
        IActionResult GetCustomers();
        IActionResult GetCustomer(int customerId);
        IActionResult FindCustomers(string customerName);
        IActionResult FindCustomersByAddress(string address);
        IActionResult SaveCustomer(Customer customer);
    }
}
