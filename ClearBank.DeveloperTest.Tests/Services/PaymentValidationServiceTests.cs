using ClearBank.DeveloperTest.Factories;
using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Services.PaymentValidators;
using ClearBank.DeveloperTest.Types;
using Moq;
using NUnit.Framework;

namespace ClearBank.DeveloperTest.Tests.Services
{
    public class PaymentValidationServiceTests
    {
        private IPaymentValidationService _paymentValidationService;
        private Mock<IPaymentValidatorFactory> _paymentValidatorFactoryMock;
        private Mock<IPaymentValidator> _paymentValidatorMock;

        [SetUp]
        public void SetUp()
        {
            _paymentValidatorFactoryMock = new Mock<IPaymentValidatorFactory>();
            _paymentValidatorMock = new Mock<IPaymentValidator>();

            _paymentValidationService = new PaymentValidationService(_paymentValidatorFactoryMock.Object);
        }

        [Test]
        public void IsValidPaymentRequest_WhenValidatorExistsForPaymentScheme_CallsValidator()
        {
            _paymentValidatorFactoryMock.Setup(p => p.Create(It.IsAny<PaymentScheme>())).Returns(_paymentValidatorMock.Object);

            var testPaymentRequest = new MakePaymentRequest();
            var testAccount = new Account();

            _paymentValidationService.IsValidPaymentRequest(testPaymentRequest, testAccount);

            _paymentValidatorMock.Verify(p => p.IsValidPayment(testPaymentRequest, testAccount), Times.Once);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void IsValidPaymentRequest_WhenValidatorExistsForPaymentScheme_ReturnsCorrectValue(bool isValidReturned)
        {
            _paymentValidatorMock.Setup(p => p.IsValidPayment(It.IsAny<MakePaymentRequest>(), It.IsAny<Account>())).Returns(isValidReturned);
            _paymentValidatorFactoryMock.Setup(p => p.Create(It.IsAny<PaymentScheme>())).Returns(_paymentValidatorMock.Object);

            var isValid = _paymentValidationService.IsValidPaymentRequest(new MakePaymentRequest(), new Account());

            Assert.That(isValid, Is.EqualTo(isValidReturned));
        }

        [Test]
        public void IsValidPaymentRequest_WhenNoValidatorExistsForPaymentScheme_ReturnsFalse()
        {
            var isValid = _paymentValidationService.IsValidPaymentRequest(new MakePaymentRequest(), new Account());

            Assert.IsFalse(isValid);
        }
    }
}
