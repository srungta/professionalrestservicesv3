using System;

namespace Microsoft.Libraries.Business.Validations.Interfaces
{
    public interface IBusinessValidation<T>
    {
        bool Validate(T tObject);
    }
}
