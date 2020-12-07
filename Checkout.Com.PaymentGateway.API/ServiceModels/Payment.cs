using Checkout.Com.PaymentGateway.API.Validators;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Checkout.Com.PaymentGateway.API.ServiceModels
{
    public class Payment
    {
        [JsonProperty("card", Required = Required.Always)]
        public Card Card { get; set; }

        [JsonProperty("amount", Required = Required.Always)]
        [Range(0, Double.MaxValue, ErrorMessage ="The Amount is invalid")]
        public decimal Amount { get; set; }

        [JsonProperty("currency", Required = Required.Always)]
        [AllowedCurrency(new string[] { "EUR", "USD", "INR" }, ErrorMessage = "Currency is not supported")]
        public string Currency { get; set; }
    }
}
