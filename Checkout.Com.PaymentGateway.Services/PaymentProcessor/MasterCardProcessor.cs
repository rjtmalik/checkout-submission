using AutoMapper;
using Checkout.Com.PaymentGateway.Models;
using Checkout.Com.PaymentGateway.Services.PaymentProcessor.Contracts;
using Checkout.Com.PaymentGateway.Services.PaymentProcessor.CustomExceptions;
using Checkout.Com.PaymentGateway.Services.PaymentProcessor.Models;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.Com.PaymentGateway.Services.PaymentProcessor
{
    public class MasterCardProcessor : IProcessor
    {
         readonly IHttpClientFactory _httpClientFactory;
         readonly IMapper _mapper;
        public MasterCardProcessor(IHttpClientFactory httpClientFactory,
            IMapper mapper)
        {
            _httpClientFactory = httpClientFactory;
            _mapper = mapper;
        }

        public bool IsAppropriateProcessor(string cardNumber)
        {
            var initials = new string[] { "5555", "2223" };
            if (initials.Any(x => x == cardNumber.Substring(0, 4))) return true;
            return false;
        }

        public async Task<PaymentStatus> ProcessAsync(Payment paymentRequest)
        {
            var client = _httpClientFactory.CreateClient("mastercard");
            var msg = new HttpRequestMessage(HttpMethod.Post, "/payments");
            msg.Content = new StringContent(
               JsonConvert.SerializeObject(paymentRequest),
               Encoding.UTF8,
               "application/json");
            var response = await client.SendAsync(msg);
            if (response.IsSuccessStatusCode)
            {
                var responseStr = await response.Content.ReadAsStringAsync();
                return _mapper.Map<PaymentStatus>(JsonConvert.DeserializeObject<MastercardPaymentResponse>(responseStr));
            }
            throw new PaymentNotProcessedException($"payment not processed for card ending with {paymentRequest.Card.Number.Substring(paymentRequest.Card.Number.Length - 3, 3)}");
        }
    }
}
