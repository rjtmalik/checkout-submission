using System;

namespace Checkout.Com.PaymentGateway.Services.Persistance.PersistanceExceptions
{
    public class PaymentNotFoundException: Exception
    {
        public PaymentNotFoundException(string message)
            :base(message)
        {

        }
    }
}
