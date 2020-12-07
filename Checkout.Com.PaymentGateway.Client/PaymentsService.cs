using Checkout.Com.PaymentGateway.Client.CustomExceptions;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Checkout.Com.PaymentGateway.Client.ServiceModels;
using Newtonsoft.Json;
using System.Text;

namespace Checkout.Com.PaymentGateway.Client
{
    public class PaymentsService : IPaymentsService
    {
        private readonly HttpClient _httpClient;

        public PaymentsService(HttpClient httpClient)
        {
            if (_httpClient.BaseAddress == null)
            {
                throw new BaseAddressMissingException("base address should be set");
            }
            _httpClient = httpClient;
        }

        public PaymentsService(Uri baseAddress)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseAddress;
        }

        public async Task<Guid> CapturePaymentAsync(Payment payment)
        {
            var msg = new HttpRequestMessage(HttpMethod.Post, "/api/payments");
            msg.Content = new StringContent(
               JsonConvert.SerializeObject(payment),
               Encoding.UTF8,
               "application/json");
            var response = await _httpClient.SendAsync(msg);
            if (response.IsSuccessStatusCode)
            {
                var responseStr = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<PaymentAcknowledgement>(responseStr).PublicId;
            }
            throw new PaymentNotProcessedException($"payment could not be processed");
        }

        public async Task<Payment> RetrievePaymentAsync(Guid paymentId)
        {
            var msg = new HttpRequestMessage(HttpMethod.Get, $"/api/payments/{paymentId}");
            var response = await _httpClient.SendAsync(msg);
            if (response.IsSuccessStatusCode)
            {
                var responseStr = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Payment>(responseStr);
            }
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
            throw new PaymentNotRetrievedException($"payment could not be reqtrieved for {paymentId}");
        }
    }
}
