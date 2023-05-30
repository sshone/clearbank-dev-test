using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public interface IPaymentSchemeValidator
    {
        // The PaymentScheme property is used to determine which validator to use for a given payment request.
        PaymentScheme PaymentScheme { get; }
        bool ValidatePayment(MakePaymentRequest request, Account account);
    }
}
