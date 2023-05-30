using ClearBank.DeveloperTest.Factories;
using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ClearBank.DeveloperTest.Tests.Services
{
    [TestClass]
    public class PaymentServiceTests
    {
        private Mock<IAccountService> _accountServiceMock;
        private Mock<IPaymentSchemeValidator> _paymentSchemeValidatorMock;
        private Mock<IPaymentSchemeValidatorFactory> _paymentSchemeValidatorFactoryMock;
        private PaymentService _paymentService;

        [TestInitialize]
        public void TestInitialize()
        {
            _accountServiceMock = new Mock<IAccountService>();
            _paymentSchemeValidatorMock = new Mock<IPaymentSchemeValidator>();
            _paymentSchemeValidatorFactoryMock = new Mock<IPaymentSchemeValidatorFactory>();

            _paymentSchemeValidatorFactoryMock.Setup(f => f.GetValidator(It.IsAny<PaymentScheme>()))
                .Returns(_paymentSchemeValidatorMock.Object);

            _paymentService = new PaymentService(_accountServiceMock.Object, _paymentSchemeValidatorFactoryMock.Object);
        }

        [TestMethod]
        public void MakePayment_ValidRequest_SuccessfullyMakesPayment()
        {
            // Arrange
            var request = new MakePaymentRequest
            {
                CreditorAccountNumber = "12345678",
                PaymentScheme = PaymentScheme.Bacs,
                Amount = 100
            };

            var account = new Account { Balance = 500 };

            _accountServiceMock.Setup(s => s.GetAccount(request.CreditorAccountNumber)).Returns(account);
            _paymentSchemeValidatorMock.Setup(v => v.ValidatePayment(request, account)).Returns(true);

            // Act
            var result = _paymentService.MakePayment(request);

            // Assert
            Assert.IsTrue(result.Success);
            _paymentSchemeValidatorMock.Verify(v => v.ValidatePayment(request, account), Times.Once);
            _accountServiceMock.Verify(s => s.DeductFromBalance(account, request.Amount), Times.Once);
        }

        [TestMethod]
        public void MakePayment_InvalidRequest_ReturnsFailedResult()
        {
            // Arrange
            var request = new MakePaymentRequest
            {
                CreditorAccountNumber = "12345678",
                PaymentScheme = PaymentScheme.Chaps,
                Amount = 100
            };

            var account = new Account { Balance = 500 };

            _accountServiceMock.Setup(s => s.GetAccount(request.CreditorAccountNumber)).Returns(account);
            _paymentSchemeValidatorMock.Setup(v => v.ValidatePayment(request, account)).Returns(false);

            // Act
            var result = _paymentService.MakePayment(request);

            // Assert
            Assert.IsFalse(result.Success);
            _paymentSchemeValidatorMock.Verify(v => v.ValidatePayment(request, account), Times.Once);
            _accountServiceMock.Verify(s => s.DeductFromBalance(account, request.Amount), Times.Never);
        }
    }
}
