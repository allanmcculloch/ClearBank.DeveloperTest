using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public interface IAccountService
    {
        Account GetAccount(string debtorAccountNumber);

        void ProcessPayment(MakePaymentRequest request, Account account);
    }
}