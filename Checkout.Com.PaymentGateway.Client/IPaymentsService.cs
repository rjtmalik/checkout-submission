using Checkout.Com.PaymentGateway.Client.ServiceModels;
using System;
using System.Threading.Tasks;

namespace Checkout.Com.PaymentGateway.Client
{
    public interface IPaymentsService
    {
        Task<Guid> CapturePaymentAsync(Payment payment);
        Task<Payment> RetrievePaymentAsync(Guid paymentId);
    }
}
