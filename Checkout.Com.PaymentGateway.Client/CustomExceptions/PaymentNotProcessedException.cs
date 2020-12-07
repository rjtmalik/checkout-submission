using System;

namespace Checkout.Com.PaymentGateway.Client.CustomExceptions
{
    public class PaymentNotProcessedException : Exception
    {
        public PaymentNotProcessedException(string message)
            :base(message)
        {

        }
    }
}
