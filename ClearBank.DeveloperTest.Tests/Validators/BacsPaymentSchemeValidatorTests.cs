using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClearBank.DeveloperTest.Tests.Validators
{
    [TestClass]
    public class BacsPaymentSchemeValidatorTests : PaymentSchemeValidatorBaseTests
    {
        private Account _accountMock;
        private MakePaymentRequest _paymentRequest;

        [TestInitialize]
        public void TestInitialize()
        {
            _accountMock = new Account()
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs,
                Balance = 100
            };

            _paymentRequest = new MakePaymentRequest { Amount = 100 };
        }

        [TestMethod]
        public void ValidatePayment_ReturnsTrue()
        {
            Assert.IsTrue(Validator.ValidatePayment(_paymentRequest, _accountMock));
        }

        protected override PaymentSchemeValidatorBase GetValidator()
        {
            return new BacsPaymentSchemeValidator();
        }
    }
}
