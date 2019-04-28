using System;
using ClearBank.DeveloperTest.Factories;
using ClearBank.DeveloperTest.Services.PaymentValidators;
using ClearBank.DeveloperTest.Types;
using NUnit.Framework;

namespace ClearBank.DeveloperTest.Tests.Factories
{
    [TestFixture]
    public class PaymentValidatorFactoryTests
    {
        private PaymentValidatorFactory _paymentValidatorFactory;

        [SetUp]
        public void Setup()
        {
            _paymentValidatorFactory = new PaymentValidatorFactory();
        }

        [TestCase(PaymentScheme.Bacs, typeof(BacsPaymentValidator))]
        [TestCase(PaymentScheme.FasterPayments, typeof(FasterPaymentsValidator))]
        [TestCase(PaymentScheme.Chaps, typeof(ChapsPaymentValidator))]
        public void Create_GetsCorrectType(PaymentScheme paymentScheme, Type type)
        {
            var paymentValidator = _paymentValidatorFactory.Create(paymentScheme);

            Assert.That(paymentValidator, Is.InstanceOf(type));
        }
    }
}
