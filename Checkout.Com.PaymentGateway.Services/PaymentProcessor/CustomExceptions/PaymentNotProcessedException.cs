using System;

namespace Checkout.Com.PaymentGateway.Services.PaymentProcessor.CustomExceptions
{
    public class PaymentNotProcessedException: Exception
    {
        public PaymentNotProcessedException(string message)
            : base(message)
        {

        }
    }
}
