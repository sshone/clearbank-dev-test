using ClearBank.DeveloperTest.Extensions;
using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace ClearBank.DeveloperTest.Tests.Validators
{
    [TestClass]
    public abstract class PaymentSchemeValidatorBaseTests
    {
        protected MakePaymentRequest PaymentRequest;
        protected PaymentSchemeValidatorBase Validator;

        public PaymentSchemeValidatorBaseTests()
        {
            PaymentRequest = new MakePaymentRequest { Amount = 100 };
            Validator = GetValidator();
        }

        [TestMethod]
        public void ValidatePayment_ReturnsFalseIfAccountIsNull()
        {
            Assert.IsFalse(Validator.ValidatePayment(PaymentRequest, null));
        }

        [TestMethod]
        public void ValidatePayment_ReturnsFalseIfAccountNotAllowedScheme()
        {
            var allowedPaymentSchemes = Enum.GetValues(typeof(AllowedPaymentSchemes))
                                            .Cast<AllowedPaymentSchemes>()
                                            .Where(scheme => scheme != Validator.PaymentScheme.ToAllowedPaymentScheme());

            var account = new Account
            {
                AllowedPaymentSchemes = allowedPaymentSchemes.Aggregate((current, scheme) => current | scheme)
            };

            Assert.IsFalse(Validator.ValidatePayment(PaymentRequest, account));
        }

        [TestMethod]
        public void ValidatePayment_ReturnsTrueIfAccountHasAllowedScheme()
        {
            var account = GetValidAccount();

            Assert.IsTrue(Validator.ValidatePayment(PaymentRequest, account));
        }

        protected abstract PaymentSchemeValidatorBase GetValidator();

        /// <summary>
        /// Provides a base valid account, can be overridden by derived classes if they have more specific rules.
        /// </summary>
        protected virtual Account GetValidAccount()
        {
            return new Account
            {
                AllowedPaymentSchemes = Validator.PaymentScheme.ToAllowedPaymentScheme()
            };
        }

    }
}
