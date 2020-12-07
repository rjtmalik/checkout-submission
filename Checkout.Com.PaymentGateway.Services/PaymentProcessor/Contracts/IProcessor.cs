using Checkout.Com.PaymentGateway.Models;
using Checkout.Com.PaymentGateway.Services.PaymentProcessor.Models;
using System.Threading.Tasks;

namespace Checkout.Com.PaymentGateway.Services.PaymentProcessor.Contracts
{
    public interface IProcessor
    {
        bool IsAppropriateProcessor(string cardNumber);
        Task<PaymentStatus> ProcessAsync(Payment paymentRequest); 
    }
}
