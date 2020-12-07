using System;

namespace Checkout.Com.PaymentGateway.Client.CustomExceptions
{
    public class BaseAddressMissingException: Exception
    {
        public BaseAddressMissingException(string message)
            :base(message)
        {

        }
    }
}
