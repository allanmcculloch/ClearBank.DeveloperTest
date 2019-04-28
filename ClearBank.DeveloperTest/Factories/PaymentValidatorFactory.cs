using System.Collections.Generic;
using ClearBank.DeveloperTest.Services.PaymentValidators;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Factories
{
    public class PaymentValidatorFactory : IPaymentValidatorFactory
    {
        private readonly Dictionary<PaymentScheme, IPaymentValidator> _validators =
            new Dictionary<PaymentScheme, IPaymentValidator> {
                { PaymentScheme.Bacs, new BacsPaymentValidator() },
                { PaymentScheme.Chaps, new ChapsPaymentValidator() },
                { PaymentScheme.FasterPayments, new FasterPaymentsValidator() }
            };

        public IPaymentValidator Create(PaymentScheme paymentScheme)
        {
            return _validators[paymentScheme];
        }
    }
}
