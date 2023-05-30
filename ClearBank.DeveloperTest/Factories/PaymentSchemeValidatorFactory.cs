using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClearBank.DeveloperTest.Factories
{
    public class PaymentSchemeValidatorFactory : IPaymentSchemeValidatorFactory
    {
        private readonly IEnumerable<IPaymentSchemeValidator> _paymentSchemeValidators;

        public PaymentSchemeValidatorFactory(IEnumerable<IPaymentSchemeValidator> paymentSchemeValidators)
        {
            _paymentSchemeValidators = paymentSchemeValidators;
        }

        public IPaymentSchemeValidator GetValidator(PaymentScheme paymentScheme)
        {
            // ASSUMPTION: Throwing an exception if no validator is found, but could return a default validator instead.
            return _paymentSchemeValidators.FirstOrDefault(validator => validator.PaymentScheme == paymentScheme)
                ?? throw new InvalidOperationException($"No validator found for payment scheme {paymentScheme}");
        }
    }

}
