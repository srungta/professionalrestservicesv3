using Microsoft.EntityFrameworkCore;
using Microsoft.Libraries.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Libraries.ORM.Interfaces
{
    public interface ICustomersContext : IDisposable
    {
        DbSet<Customer> Customers { get; set; }

        bool CommitChanges();
    }
}
