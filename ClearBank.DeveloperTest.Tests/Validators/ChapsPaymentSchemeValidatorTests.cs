using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClearBank.DeveloperTest.Tests.Validators
{
    [TestClass]
    public class ChapsPaymentSchemeValidatorTests : PaymentSchemeValidatorBaseTests
    {
        private MakePaymentRequest _paymentRequest;

        [TestInitialize]
        public void TestInitialize()
        {
            _paymentRequest = new MakePaymentRequest { Amount = 100 };
        }

        [DataTestMethod]
        [DataRow(AccountStatus.Live)]
        public void ValidatePayment_ReturnsTrueForValidAccountStatuses(AccountStatus validStatus)
        {
            var account = new Account()
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps,
                Status = validStatus
            };

            Assert.IsTrue(Validator.ValidatePayment(_paymentRequest, account));
        }

        [DataTestMethod]
        [DataRow(AccountStatus.Disabled)]
        [DataRow(AccountStatus.InboundPaymentsOnly)]
        public void ValidatePayment_ReturnsFalseForInvalidAccountStatuses(AccountStatus invalidStatus)
        {
            var account = new Account()
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps,
                Status = invalidStatus
            };

            Assert.IsFalse(Validator.ValidatePayment(_paymentRequest, account));
        }

        protected override PaymentSchemeValidatorBase GetValidator()
        {
            return new ChapsPaymentSchemeValidator();
        }
    }
}
