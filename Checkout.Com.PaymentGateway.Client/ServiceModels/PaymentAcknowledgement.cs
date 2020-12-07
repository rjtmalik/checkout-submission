using Newtonsoft.Json;
using System;

namespace Checkout.Com.PaymentGateway.Client.ServiceModels
{
    public class PaymentAcknowledgement
    {
        [JsonProperty("id")]
        public Guid PublicId { get; set; }
    }
}
