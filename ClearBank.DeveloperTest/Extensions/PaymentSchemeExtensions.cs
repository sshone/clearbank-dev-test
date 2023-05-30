using ClearBank.DeveloperTest.Types;
using System;

namespace ClearBank.DeveloperTest.Extensions
{
    public static class PaymentSchemeExtensions
    {
        public static AllowedPaymentSchemes ToAllowedPaymentScheme(this PaymentScheme scheme)
        {
            return scheme switch
            {
                PaymentScheme.FasterPayments => AllowedPaymentSchemes.FasterPayments,
                PaymentScheme.Bacs => AllowedPaymentSchemes.Bacs,
                PaymentScheme.Chaps => AllowedPaymentSchemes.Chaps,
                _ => throw new ArgumentOutOfRangeException(nameof(scheme), scheme, "Invalid PaymentScheme value."),
            };
        }
    }

}
