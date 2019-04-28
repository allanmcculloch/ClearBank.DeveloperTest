using ClearBank.DeveloperTest.Services.PaymentValidators;
using ClearBank.DeveloperTest.Types;
using NUnit.Framework;

namespace ClearBank.DeveloperTest.Tests.Services.PaymentValidators
{
    [TestFixture]
    public class ChapsPaymentValidatorTests
    {
        private ChapsPaymentValidator _chapsPaymentValidator;
        private Account _account;
        private MakePaymentRequest _makePaymentRequest;
            
        [SetUp]
        public void Setup()
        {
            _chapsPaymentValidator = new ChapsPaymentValidator();
            _makePaymentRequest = new MakePaymentRequest();
            _account = new Account();
        }

        [Test]
        public void IsValidPayment_IsAllowedPaymentSchemeAndLive_ReturnsTrue()
        {
            _account.AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps;
            _account.Status = AccountStatus.Live;

            var isValidPayment = _chapsPaymentValidator.IsValidPayment(_makePaymentRequest, _account);

            Assert.IsTrue(isValidPayment);
        }

        [Test]
        public void IsValidPayment_AccountNull_ReturnsFalse()
        {
            _account.AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps;

            _account = null;

            var isValidPayment = _chapsPaymentValidator.IsValidPayment(_makePaymentRequest, _account);

            Assert.IsFalse(isValidPayment);
        }

        [TestCase(AccountStatus.Disabled)]
        [TestCase(AccountStatus.InboundPaymentsOnly)]
        public void IsValidPayment_AccountNotLive_ReturnsFalse(AccountStatus accountStatus)
        {
            _account.AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps;

            _account = new Account { Status = accountStatus };

            var isValidPayment = _chapsPaymentValidator.IsValidPayment(_makePaymentRequest, _account);

            Assert.IsFalse(isValidPayment);
        }

        [TestCase(AllowedPaymentSchemes.Bacs)]
        [TestCase(AllowedPaymentSchemes.FasterPayments)]
        public void IsValidPayment_NotValidPaymentScheme_ReturnsFalse(AllowedPaymentSchemes allowedPaymentSchemes)
        {
            _account.AllowedPaymentSchemes = allowedPaymentSchemes;

            var isValidPayment = _chapsPaymentValidator.IsValidPayment(_makePaymentRequest, _account);

            Assert.IsFalse(isValidPayment);
        }
    }
}
