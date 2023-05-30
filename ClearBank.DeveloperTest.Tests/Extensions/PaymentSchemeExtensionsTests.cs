using ClearBank.DeveloperTest.Extensions;
using ClearBank.DeveloperTest.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace ClearBank.DeveloperTest.Tests.Extensions
{
    [TestClass]
    public class PaymentSchemeExtensionsTests
    {
        /// <summary>
        /// Verifies that the number of payment schemes matches the expected count since the last test run
        /// </summary>
        /// <remarks>
        /// This test ensures that the extension method and tests are updated if the number of payment schemes changes.
        /// If this test fails, it serves as a reminder to update the extension method and tests accordingly.
        /// </remarks>
        [TestMethod]
        public void ToAllowedPaymentScheme_ChecksNumberOfPaymentSchemes()
        {
            // Arrange
            const int expectedCount = 3; // Update the count if the number of allowed payment schemes changes
            var paymentSchemes = Enum.GetValues(typeof(PaymentScheme)).Cast<PaymentScheme>();

            // Act
            var actualCount = paymentSchemes.Count();

            // Assert
            Assert.AreEqual(expectedCount, actualCount, $"Number of allowed payment schemes ({actualCount}) does not match the expected count ({expectedCount}). " +
                $"Please update the extension method and tests accordingly.");
        }

        [DataTestMethod]
        [DataRow(PaymentScheme.FasterPayments, AllowedPaymentSchemes.FasterPayments)]
        [DataRow(PaymentScheme.Bacs, AllowedPaymentSchemes.Bacs)]
        [DataRow(PaymentScheme.Chaps, AllowedPaymentSchemes.Chaps)]
        public void ToAllowedPaymentScheme_ReturnsExpectedAllowedPaymentScheme(PaymentScheme paymentScheme, AllowedPaymentSchemes expectedAllowedScheme)
        {
            // Act
            var actualAllowedScheme = paymentScheme.ToAllowedPaymentScheme();

            // Assert
            Assert.AreEqual(expectedAllowedScheme, actualAllowedScheme);
        }

        [TestMethod]
        public void ToAllowedPaymentScheme_ThrowsExceptionForInvalidPaymentScheme()
        {
            // Arrange
            var invalidScheme = (PaymentScheme)99; // Invalid payment scheme value

            // Act and Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => invalidScheme.ToAllowedPaymentScheme());
        }
    }

}
