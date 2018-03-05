using Microsoft.Libraries.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Libraries.Business.Interfaces
{
    public interface ICustomersBusinessComponent : IBusinessComponent
    {
        IEnumerable<Customer> GetCustomers(
            string customerName = default(string),
            string customerAddress = default(string));

        Customer GetCustomer(int customerId);

        Customer SaveCustomer(Customer customer);
    }
}
