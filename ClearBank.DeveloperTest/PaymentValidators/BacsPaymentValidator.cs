using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services.PaymentValidators
{
    public class BacsPaymentValidator : IPaymentValidator
    {
        public bool IsValidPayment(MakePaymentRequest request, Account account)
        {
            return account != null && account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Bacs);
        }
    }
}
