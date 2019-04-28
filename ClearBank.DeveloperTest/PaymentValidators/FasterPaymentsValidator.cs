
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services.PaymentValidators
{
    public class FasterPaymentsValidator : IPaymentValidator
    {
        public bool IsValidPayment(MakePaymentRequest request, Account account)
        {
            if (account == null)
            {
                return false;
            }

            if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.FasterPayments))
            {
                return false;
            }

            return account.Balance >= request.Amount;
        }
    }
}
