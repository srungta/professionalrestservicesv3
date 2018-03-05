using Microsoft.Libraries.Business.Validations.Interfaces;
using System;
using System.Linq;

namespace Microsoft.Libraries.Business.Validations.Impl
{
    public class CustomerNameValidation : IBusinessValidation<string>
    {
        public bool Validate(string tObject)
        {
            var badKeywords = new string[] { "bad", "worse", "not good", "awful" };
            var validation = !string.IsNullOrEmpty(tObject) &&
                !badKeywords.Contains(tObject);

            return validation;
        }
    }
}
