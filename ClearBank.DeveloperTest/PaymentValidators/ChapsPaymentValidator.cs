using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services.PaymentValidators
{
    public class ChapsPaymentValidator : IPaymentValidator
    {
        public bool IsValidPayment(MakePaymentRequest request, Account account)
        {
            if (account == null)
            {
                return false;
            }

            if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Chaps))
            {
                return false;
            }

            return account.Status == AccountStatus.Live;
        }
    }
}
