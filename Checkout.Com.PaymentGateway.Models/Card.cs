﻿namespace Checkout.Com.PaymentGateway.Models
{
    public class Card
    {
        public string Number { get;  set; }
        public int ExpiryMonth { get;  set; }
        public int ExpiryYear { get;  set; }
        public int CVV { get;  set; }
    }
}
