using ClearBank.DeveloperTest.Factories;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IAccountService _accountService;
        private readonly IPaymentSchemeValidatorFactory _paymentSchemeValidatorFactory;

        public PaymentService(IAccountService accountService,
                              IPaymentSchemeValidatorFactory paymentSchemeValidatorFactory)
        {
            _accountService = accountService;
            _paymentSchemeValidatorFactory = paymentSchemeValidatorFactory;
        }

        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
            var account = _accountService.GetAccount(request.CreditorAccountNumber);

            var paymentSchemeValidator = _paymentSchemeValidatorFactory.GetValidator(request.PaymentScheme);
            var paymentSchemeIsValid = paymentSchemeValidator.ValidatePayment(request, account);

            if (!paymentSchemeIsValid)
            {
                return new MakePaymentResult()
                {
                    Success = false
                };
            }

            _accountService.DeductFromBalance(account, request.Amount);

            return new MakePaymentResult
            {
                Success = paymentSchemeIsValid
            };
        }
    }
}
