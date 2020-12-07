using Newtonsoft.Json;
using System;

namespace Checkout.Com.PaymentGateway.Client.ServiceModels
{
    public class Payment
    {
        [JsonProperty("card", Required = Required.Always)]
        public Card Card { get; set; }

        [JsonProperty("amount", Required = Required.Always)]
        public decimal Amount { get; set; }

        [JsonProperty("currency", Required = Required.Always)]
        public string Currency { get; set; }
    }
}
