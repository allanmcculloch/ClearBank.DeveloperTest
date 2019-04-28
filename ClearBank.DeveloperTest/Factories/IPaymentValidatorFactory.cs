using ClearBank.DeveloperTest.Services.PaymentValidators;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Factories
{
    public interface IPaymentValidatorFactory
    {
        IPaymentValidator Create(PaymentScheme paymentScheme);
    }
}