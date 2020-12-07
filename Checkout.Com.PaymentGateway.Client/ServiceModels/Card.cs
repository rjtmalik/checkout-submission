using Newtonsoft.Json;

namespace Checkout.Com.PaymentGateway.Client.ServiceModels
{
    public class Card
    {
        [JsonProperty("number", Required = Required.Always)]
        public string Number { get; set; }

        [JsonProperty("expiry", Required = Required.Always)]
        public string Expiry { get; set; }

        [JsonProperty("cvv", Required = Required.Always)]
        public int CVV { get; set; }

    }
}
