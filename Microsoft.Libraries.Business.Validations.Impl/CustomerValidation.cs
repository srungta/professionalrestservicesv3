using Microsoft.Libraries.Business.Validations.Interfaces;
using Microsoft.Libraries.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Libraries.Business.Validations.Impl
{
    public class CustomerValidation : IBusinessValidation<Customer>
    {
        private const int MIN_CREDIT = 10000;

        public bool Validate(Customer tObject)
        {
            var validation = tObject != default(Customer) &&
                !string.IsNullOrEmpty(tObject.CustomerName) &&
                tObject.CreditLimit >= MIN_CREDIT;

            return validation;
        }
    }
}
