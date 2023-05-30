using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClearBank.DeveloperTest.Tests.Validators
{
    [TestClass]
    public class FasterPaymentsSchemeValidatorTests : PaymentSchemeValidatorBaseTests
    {
        private MakePaymentRequest _paymentRequest;

        [TestInitialize]
        public void TestInitialize()
        {
            _paymentRequest = new MakePaymentRequest { Amount = 100 };
        }

        [TestMethod]
        public void ValidatePayment_ReturnsTrueWhenBalanceIsEnough()
        {
            var account = new Account()
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments,
                Balance = 100
            };

            Assert.IsTrue(Validator.ValidatePayment(_paymentRequest, account));
        }

        [TestMethod]
        public void ValidatePayment_ReturnsFalseWhenBalanceIsNotEnough()
        {
            var account = new Account()
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments,
                Balance = 0
            };

            Assert.IsFalse(Validator.ValidatePayment(_paymentRequest, account));
        }

        protected override PaymentSchemeValidatorBase GetValidator()
        {
            return new FasterPaymentsSchemeValidator();
        }

        protected override Account GetValidAccount()
        {
            var account = new Account()
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments,
                Balance = 100
            };

            return account;
        }
    }
}
