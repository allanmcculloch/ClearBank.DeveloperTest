using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services.PaymentValidators
{
    public interface IPaymentValidator
    {
        bool IsValidPayment(MakePaymentRequest request, Account account);
    }
}