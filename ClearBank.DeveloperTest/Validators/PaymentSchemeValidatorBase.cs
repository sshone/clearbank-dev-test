using ClearBank.DeveloperTest.Extensions;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public abstract class PaymentSchemeValidatorBase : IPaymentSchemeValidator
    {
        public abstract PaymentScheme PaymentScheme { get; }

        public bool ValidatePayment(MakePaymentRequest request, Account account)
        {
            return IsAccountValid(account) && ValidatePaymentInternal(request, account);
        }

        // Custom validation rules should be implemented using this method in derived classes.
        protected abstract bool ValidatePaymentInternal(MakePaymentRequest request, Account account);

        private bool IsAccountValid(Account account)
        {
            return account != null && account.AllowedPaymentSchemes.HasFlag(PaymentScheme.ToAllowedPaymentScheme());
        }
    }
}
