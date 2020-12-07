using Checkout.Com.PaymentGateway.API.Validators;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Checkout.Com.PaymentGateway.API.ServiceModels
{
    public class Card
    {
        [RegularExpression(@"^[0-9 ]+$", ErrorMessage = "number is invalid")]
        [JsonProperty("number", Required = Required.Always)]
        public string Number { get; set; }

        [JsonProperty("expiry", Required = Required.Always)]
        [AllowedCardExpiryDate(ErrorMessage = "The expiry date is invalid")]
        public string Expiry { get; set; }

        [JsonProperty("cvv", Required = Required.Always)]
        [RegularExpression(@"^[0-9][0-9][0-9]$", ErrorMessage = "CVV is invalid")]
        public int CVV { get; set; }

    }
}
