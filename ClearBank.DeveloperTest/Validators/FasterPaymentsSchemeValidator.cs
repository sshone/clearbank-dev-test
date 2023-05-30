using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public class FasterPaymentsSchemeValidator : PaymentSchemeValidatorBase
    {
        public override PaymentScheme PaymentScheme => PaymentScheme.FasterPayments;

        protected override bool ValidatePaymentInternal(MakePaymentRequest request, Account account)
        {
            return account.Balance >= request.Amount;
        }
    }
}
