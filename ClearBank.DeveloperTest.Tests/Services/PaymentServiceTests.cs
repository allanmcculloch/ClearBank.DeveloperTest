using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;
using Moq;
using NUnit.Framework;

namespace ClearBank.DeveloperTest.Tests.Services
{
    public class PaymentServiceTests
    {
        private IPaymentService _paymentService;
        private Mock<IAccountService> _accountServiceMock;
        private Mock<IPaymentValidationService> _paymentValidationServiceMock;

        private const string TestDebitAccountNumber = "12345678";
        private Account _testAccount;
        private MakePaymentRequest _testMakePaymentRequest;

        [SetUp]
        public void SetUp()
        {
            _accountServiceMock = new Mock<IAccountService>();
            _paymentValidationServiceMock = new Mock<IPaymentValidationService>();

            _testAccount = new Account();
            _testMakePaymentRequest = new MakePaymentRequest { DebtorAccountNumber = TestDebitAccountNumber };

            _paymentService = new PaymentService(_accountServiceMock.Object, _paymentValidationServiceMock.Object);
        }

        [Test]
        public void MakePayment_WhenValid_UpdatesAccount()
        {
            _accountServiceMock.Setup(a => a.GetAccount(TestDebitAccountNumber)).Returns(_testAccount);
            _paymentValidationServiceMock.Setup(p => p.IsValidPaymentRequest(_testMakePaymentRequest, _testAccount)).Returns(true);

            var result = _paymentService.MakePayment(_testMakePaymentRequest);

            _paymentValidationServiceMock.Verify(p => p.IsValidPaymentRequest(_testMakePaymentRequest, _testAccount));
            _accountServiceMock.Verify(a => a.ProcessPayment(_testMakePaymentRequest, _testAccount), Times.Once);
            Assert.IsTrue(result.Success);
        }

        [Test]
        public void MakePayment_WhenInvalid_DoesNotUpdateAccount()
        {
            _accountServiceMock.Setup(a => a.GetAccount(TestDebitAccountNumber)).Returns(_testAccount);
            _paymentValidationServiceMock.Setup(p => p.IsValidPaymentRequest(_testMakePaymentRequest, _testAccount)).Returns(false);

            var result = _paymentService.MakePayment(_testMakePaymentRequest);

            _paymentValidationServiceMock.Verify(p => p.IsValidPaymentRequest(_testMakePaymentRequest, _testAccount));
            _accountServiceMock.Verify(a => a.ProcessPayment(It.IsAny<MakePaymentRequest>(), It.IsAny<Account>()), Times.Never);
            Assert.IsFalse(result.Success);
        }

        [Test]
        public void MakePayment_WhenInvalidAccount_ReturnsFailed()
        {
            var testMakePaymentRequest = new MakePaymentRequest { DebtorAccountNumber = "invalid" };
            
            var result = _paymentService.MakePayment(testMakePaymentRequest);
            
            _accountServiceMock.Verify(a => a.ProcessPayment(It.IsAny<MakePaymentRequest>(), It.IsAny<Account>()), Times.Never);
            Assert.IsFalse(result.Success);
        }
    }
}
