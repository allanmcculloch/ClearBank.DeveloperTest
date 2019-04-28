using ClearBank.DeveloperTest.Factories;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public class PaymentValidationService : IPaymentValidationService
    {
        private readonly IPaymentValidatorFactory _paymentValidatorFactory;

        public PaymentValidationService(IPaymentValidatorFactory paymentValidatorFactory)
        {
            _paymentValidatorFactory = paymentValidatorFactory;
        }

        public bool IsValidPaymentRequest(MakePaymentRequest request, Account account)
        {
            var validator = _paymentValidatorFactory.Create(request.PaymentScheme);
                       
            return validator?.IsValidPayment(request, account) ?? false;
        }
    }
}