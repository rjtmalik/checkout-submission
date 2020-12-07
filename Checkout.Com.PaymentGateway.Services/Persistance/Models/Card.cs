namespace Checkout.Com.PaymentGateway.Services.Persistance.Models
{
    public class Card
    {
        public string MaskedNumber { get; set; }
        public string EncryptedNumber { get;  set; }
        public int ExpiryMonth { get;  set; }
        public int ExpiryYear { get;  set; }
        public int CVV { get;  set; }
    }
}