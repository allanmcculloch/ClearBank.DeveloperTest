using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public class PaymentService : IPaymentService
    {
        readonly IAccountService _accountService;
        readonly IPaymentValidationService _paymentValidationService;

        public PaymentService(IAccountService accountService, IPaymentValidationService paymentValidationService)
        {
            _accountService = accountService;
            _paymentValidationService = paymentValidationService;
        }

        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
            var account = _accountService.GetAccount(request.DebtorAccountNumber);
                      
            var result = new MakePaymentResult();

            if (account == null)
                return result;

            if (_paymentValidationService.IsValidPaymentRequest(request, account))
            {
                _accountService.ProcessPayment(request, account);
                result.Success = true;
            }
            
            return result;
        }
    }
}
