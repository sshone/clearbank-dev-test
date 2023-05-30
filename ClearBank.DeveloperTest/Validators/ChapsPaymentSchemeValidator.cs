using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public class ChapsPaymentSchemeValidator : PaymentSchemeValidatorBase
    {
        public override PaymentScheme PaymentScheme => PaymentScheme.Chaps;

        protected override bool ValidatePaymentInternal(MakePaymentRequest request, Account account)
        {
            return account.Status == AccountStatus.Live;
        }
    }
}
