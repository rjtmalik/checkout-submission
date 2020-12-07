using Newtonsoft.Json;

namespace Checkout.Com.PaymentGateway.Services.PaymentProcessor.Models
{
    public class MastercardPaymentResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("id")]
        public string UniqueId { get; set; }
    }
}
