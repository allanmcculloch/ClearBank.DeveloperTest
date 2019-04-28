using ClearBank.DeveloperTest.Services.PaymentValidators;
using ClearBank.DeveloperTest.Types;
using NUnit.Framework;

namespace ClearBank.DeveloperTest.Tests.Services.PaymentValidators
{
    [TestFixture]
    public class BacsPaymentValidatorTests
    {
        private BacsPaymentValidator _bacsPaymentValidator;
        private Account _account;
        private MakePaymentRequest _makePaymentRequest;
            
        [SetUp]
        public void Setup()
        {
            _bacsPaymentValidator = new BacsPaymentValidator();
            _makePaymentRequest = new MakePaymentRequest();
            _account = new Account();
        }

        [Test]
        public void IsValidPayment_IsAllowedPaymentScheme_ReturnsTrue()
        {
            _account.AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs;

            var isValidPayment = _bacsPaymentValidator.IsValidPayment(_makePaymentRequest, _account);

            Assert.IsTrue(isValidPayment);
        }

        [Test]
        public void IsValidPayment_AccountNull_ReturnsFalse()
        {
            _account.AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs;

            _account = null;

            var isValidPayment = _bacsPaymentValidator.IsValidPayment(_makePaymentRequest, _account);

            Assert.IsFalse(isValidPayment);
        }

        [TestCase(AllowedPaymentSchemes.Chaps)]
        [TestCase(AllowedPaymentSchemes.FasterPayments)]
        public void IsValidPayment_NotValidPaymentScheme_ReturnsFalse(AllowedPaymentSchemes allowedPaymentSchemes)
        {
            _account.AllowedPaymentSchemes = allowedPaymentSchemes;

            var isValidPayment = _bacsPaymentValidator.IsValidPayment(_makePaymentRequest, _account);

            Assert.IsFalse(isValidPayment);
        }
    }
}
