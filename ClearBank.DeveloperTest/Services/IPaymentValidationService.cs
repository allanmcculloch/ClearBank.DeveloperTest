using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public interface IPaymentValidationService
    {
        bool IsValidPaymentRequest(MakePaymentRequest request, Account account);
    }
}