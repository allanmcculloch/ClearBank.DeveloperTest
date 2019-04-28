using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Factories;
using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;
using Moq;
using NUnit.Framework;

namespace ClearBank.DeveloperTest.Tests.Services
{
    public class AccountServiceTests
    {
        private IAccountService _accountService;
        private Mock<IConfigurationService> _configurationServiceMock;
        private Mock<IDataStoreFactory> _dataStoreFactoryMock;
        private Mock<IAccountDataStore> _accountDataStoreMock;

        [SetUp]
        public void SetUp()
        {
            _configurationServiceMock = new Mock<IConfigurationService>();
            _dataStoreFactoryMock = new Mock<IDataStoreFactory>();
            _accountDataStoreMock = new Mock<IAccountDataStore>();

            const string testDataStoreType = "live";
            _configurationServiceMock.Setup(c => c.GetConfiguration(Constants.DataStoreTypeConfigurationKey)).Returns(testDataStoreType);

            _dataStoreFactoryMock.Setup(d => d.Create(testDataStoreType)).Returns(_accountDataStoreMock.Object);

            _accountService = CreateService();
        }

        [Test]
        public void GetAccount_ReturnsAccount()
        {
            const string testDebtorAccountNumber = "12345678";
          
            _accountService.GetAccount(testDebtorAccountNumber);

            _accountDataStoreMock.Verify(a => a.GetAccount(testDebtorAccountNumber));
        }

        [Test]
        [TestCase(10000, 8000, 2000)]
        [TestCase(10000, 1, 9999)]
        [TestCase(10000, 11000, -1000)]
        public void ProcessPayment_UpdatesBankAccountBalance(int initialBalance,int debitAmount, int remainingBalance)
        {
            var testAccount = new Account();
            var makePaymentRequest = new MakePaymentRequest();

            testAccount.Balance = initialBalance;
            makePaymentRequest.Amount = debitAmount;
            
            _accountService.ProcessPayment(makePaymentRequest, testAccount);

            Assert.That(testAccount.Balance, Is.EqualTo(remainingBalance));
            _accountDataStoreMock.Verify(a => a.UpdateAccount(testAccount));
        }

        [TestCase("Backup")]
        [TestCase("Live")]
        public void Service_UsesCorrectDataStore(string testDataStoreType)
        {
            _configurationServiceMock.Setup(c => c.GetConfiguration(It.IsAny<string>())).Returns(testDataStoreType);

            CreateService();

            _dataStoreFactoryMock.Verify(d => d.Create(testDataStoreType));
        }

        public IAccountService CreateService() => new AccountService(_dataStoreFactoryMock.Object, _configurationServiceMock.Object);
    }
}
