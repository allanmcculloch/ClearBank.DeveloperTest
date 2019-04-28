using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Factories;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountDataStore _dataStore;

        public AccountService(IDataStoreFactory dataStoreFactory, IConfigurationService configurationService)
        {
            var dataStoreType = configurationService.GetConfiguration(Constants.DataStoreTypeConfigurationKey);
            _dataStore = dataStoreFactory.Create(dataStoreType);
        }

        public Account GetAccount(string debtorAccountNumber)
        {
            return _dataStore.GetAccount(debtorAccountNumber);
        }

        public void ProcessPayment(MakePaymentRequest request, Account account)
        {
            account.Balance -= request.Amount;
            _dataStore.UpdateAccount(account);
        }
    }
}