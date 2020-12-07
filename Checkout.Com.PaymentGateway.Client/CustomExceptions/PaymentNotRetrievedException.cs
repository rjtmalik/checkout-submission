using System;

namespace Checkout.Com.PaymentGateway.Client.CustomExceptions
{
    public class PaymentNotRetrievedException : Exception
    {
        public PaymentNotRetrievedException(string message)
            :base(message)
        {

        }
    }
}
