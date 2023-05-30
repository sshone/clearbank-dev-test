using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public class BacsPaymentSchemeValidator : PaymentSchemeValidatorBase
    {
        public override PaymentScheme PaymentScheme => PaymentScheme.Bacs;

        protected override bool ValidatePaymentInternal(MakePaymentRequest request, Account account)
        {
            // ASSUMPTION: I have left the Bacs validator as an independant class for this exercise,
            //             but could have a default validator for payment schemes that require no further validation
            
            // No further custom validation required for Bacs payments.
            return true;
        }
    }
}
