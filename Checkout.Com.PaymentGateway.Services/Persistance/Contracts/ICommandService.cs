using Checkout.Com.PaymentGateway.Models;
using System.Threading.Tasks;

namespace Checkout.Com.PaymentGateway.Services.Persistance.Contracts
{
    public interface ICommandService
    {
        Task SaveAsync(PaymentStatus paymentRequest);
    }
}
