using System;

namespace Checkout.Com.PaymentGateway.Models
{
    public class PaymentStatus
    {
        public Status Status { get; set; }
        public Guid PublicId { get; set; }
        public string BankId { get; set; }
        public Payment Payment { get; set; }
    }

    public enum Status
    {
        YetToTransact,
        Success,
        Failure
    }
}
