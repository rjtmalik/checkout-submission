using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Checkout.Com.PaymentGateway.Services.Persistance.Models
{
    public class PaymentStatus
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Status { get; set; }
        public Guid PublicId { get; set; }
        public string BankId { get; set; }
        public Payment Payment { get; set; }
    }
}
