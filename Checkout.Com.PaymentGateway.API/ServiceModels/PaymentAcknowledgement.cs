using Newtonsoft.Json;
using System;

namespace Checkout.Com.PaymentGateway.API.ServiceModels
{
    public class PaymentAcknowledgement
    {
        [JsonProperty("id")]
        public Guid PublicId { get; set; }
    }
}
