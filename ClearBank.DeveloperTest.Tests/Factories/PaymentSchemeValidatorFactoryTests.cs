using ClearBank.DeveloperTest.Factories;
using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace ClearBank.DeveloperTest.Tests.Factories
{
    [TestClass]
    public class PaymentSchemeValidatorFactoryTests
    {
        private Mock<IPaymentSchemeValidator> _paymentSchemeValidatorMock;
        private PaymentSchemeValidatorFactory _paymentSchemeValidatorFactory;

        [TestInitialize]
        public void TestInitialize()
        {
            _paymentSchemeValidatorMock = new Mock<IPaymentSchemeValidator>();
            _paymentSchemeValidatorFactory = new PaymentSchemeValidatorFactory(new List<IPaymentSchemeValidator> { _paymentSchemeValidatorMock.Object });
        }

        [TestMethod]
        public void GetValidator_ValidPaymentScheme_ReturnsValidator()
        {
            // Arrange
            var paymentScheme = PaymentScheme.Bacs;
            _paymentSchemeValidatorMock.Setup(v => v.PaymentScheme).Returns(paymentScheme);

            // Act
            var validator = _paymentSchemeValidatorFactory.GetValidator(paymentScheme);

            // Assert
            Assert.AreEqual(_paymentSchemeValidatorMock.Object, validator);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetValidator_NoValidatorForPaymentScheme_ThrowsException()
        {
            // Arrange
            var paymentScheme = PaymentScheme.Chaps;
            _paymentSchemeValidatorMock.Setup(v => v.PaymentScheme).Returns(PaymentScheme.Bacs); // different payment scheme

            // Act
            _paymentSchemeValidatorFactory.GetValidator(paymentScheme);
        }
    }
}
