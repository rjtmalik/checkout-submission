namespace Checkout.Com.PaymentGateway.Models
{
    public class Payment
    {
        public Card Card { get;  set; }
        public decimal Amount { get;  set; }
        public string Currency { get;  set; }
    }
}
