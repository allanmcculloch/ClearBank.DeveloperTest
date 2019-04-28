using ClearBank.DeveloperTest.Services.PaymentValidators;
using ClearBank.DeveloperTest.Types;
using NUnit.Framework;

namespace ClearBank.DeveloperTest.Tests.Services.PaymentValidators
{
    [TestFixture]
    public class FasterPaymentValidatorTests
    {
        private FasterPaymentsValidator _fasterPaymentsValidator;
        private Account _account;
        private MakePaymentRequest _makePaymentRequest;
            
        [SetUp]
        public void Setup()
        {
            _fasterPaymentsValidator = new FasterPaymentsValidator();
            _makePaymentRequest = new MakePaymentRequest();
            _account = new Account();
        }

        [Test]
        public void IsValidPayment_IsAllowedPaymentSchemeAndAccountBalanceGood_ReturnsTrue()
        {
            _account.AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments;
            _account.Balance = 100;
            _makePaymentRequest.Amount = 99;

            var isValidPayment = _fasterPaymentsValidator.IsValidPayment(_makePaymentRequest, _account);

            Assert.IsTrue(isValidPayment);
        }

        [Test]
        public void IsValidPayment_AccountNull_ReturnsFalse()
        {
            _account.AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments;

            _account = null;

            var isValidPayment = _fasterPaymentsValidator.IsValidPayment(_makePaymentRequest, _account);

            Assert.IsFalse(isValidPayment);
        }
        
        [TestCase(AllowedPaymentSchemes.Bacs)]
        [TestCase(AllowedPaymentSchemes.Chaps)]
        public void IsValidPayment_NotValidPaymentScheme_ReturnsFalse(AllowedPaymentSchemes allowedPaymentSchemes)
        {
            _account.AllowedPaymentSchemes = allowedPaymentSchemes;

            var isValidPayment = _fasterPaymentsValidator.IsValidPayment(_makePaymentRequest, _account);

            Assert.IsFalse(isValidPayment);
        }

        [TestCase(AllowedPaymentSchemes.Bacs)]
        [TestCase(AllowedPaymentSchemes.Chaps)]
        public void IsValidPayment_PaymentHigherThanBalance_ReturnsFalse(AllowedPaymentSchemes allowedPaymentSchemes)
        {
            _account.AllowedPaymentSchemes = allowedPaymentSchemes;

            _account.Balance = 100;
            _makePaymentRequest.Amount = 101;

            var isValidPayment = _fasterPaymentsValidator.IsValidPayment(_makePaymentRequest, _account);

            Assert.IsFalse(isValidPayment);
        }
    }
}
