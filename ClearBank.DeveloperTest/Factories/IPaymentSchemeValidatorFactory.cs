using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Factories
{
    public interface IPaymentSchemeValidatorFactory
    {
        IPaymentSchemeValidator GetValidator(PaymentScheme paymentScheme);
    }

}
