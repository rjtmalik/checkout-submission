using Checkout.Com.PaymentGateway.Models;
using System;
using System.Threading.Tasks;

namespace Checkout.Com.PaymentGateway.Services.Persistance.Contracts
{
    public interface IQueryService
    {
        Task<Payment> GetAsync(Guid paymentId);
    }
}
