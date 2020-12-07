using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Com.PaymentGateway.Tests.Contract
{
    public class GetPaymentTests
        : IClassFixture<CustomWebApplicationFactory<Checkout.Com.PaymentGateway.API.Startup>>
    {
        private readonly CustomWebApplicationFactory<Checkout.Com.PaymentGateway.API.Startup> _factory;
        private readonly HttpClient _client;
        public GetPaymentTests(CustomWebApplicationFactory<Checkout.Com.PaymentGateway.API.Startup> factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Fact]
        public async Task Get_EndpointsReturnSuccess()
        {
            // Act
            var response = await _client.GetAsync("api/payments/02599216-92e5-416b-80d7-a0fc579072b5");

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Get_EndpointsReturn404()
        {
            // Act
            var response = await _client.GetAsync("api/payments/02599216-92e5-416b-80d7-a0fc579072b6");

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
