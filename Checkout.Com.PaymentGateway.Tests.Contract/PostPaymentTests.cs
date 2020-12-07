using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Com.PaymentGateway.Tests.Contract
{
    public class PostPaymentTests
        : IClassFixture<CustomWebApplicationFactory<Checkout.Com.PaymentGateway.API.Startup>>
    {
        private readonly CustomWebApplicationFactory<Checkout.Com.PaymentGateway.API.Startup> _factory;
        private readonly HttpClient _client;
        public PostPaymentTests(CustomWebApplicationFactory<Checkout.Com.PaymentGateway.API.Startup> factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Fact]
        public async Task Post_EndpointsReturnSuccess()
        {
            // Act
            var msg = new HttpRequestMessage(HttpMethod.Post, "api/payments");
            msg.Content = new StringContent(
               JsonConvert.SerializeObject(new
               {
                   card = new
                   {
                       number = "4111 1111 1111 1111",
                       expiry = "12/2021",
                       cvv = 123
                   },
                   amount = 1,
                   currency = "EUR"
               }),
               Encoding.UTF8,
               "application/json");
            var response = await _client.SendAsync(msg);

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task Post_EndpointsReturn400_wheninvalidexpiry()
        {
            // Act
            var msg = new HttpRequestMessage(HttpMethod.Post, "api/payments");
            msg.Content = new StringContent(
               JsonConvert.SerializeObject(new
               {
                   card = new
                   {
                       number = "4111 1111 1111 1111",
                       expiry = "23/2021",
                       cvv = 123
                   },
                   amount = 1,
                   currency = "EUR"
               }),
               Encoding.UTF8,
               "application/json");
            var response = await _client.SendAsync(msg);

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Post_EndpointsReturn400_wheninvalidcvv()
        {
            // Act
            var msg = new HttpRequestMessage(HttpMethod.Post, "api/payments");
            msg.Content = new StringContent(
               JsonConvert.SerializeObject(new
               {
                   card = new
                   {
                       number = "4111 1111 1111 1111",
                       expiry = "12/2021",
                       cvv = 1234
                   },
                   amount = 1,
                   currency = "EUR"
               }),
               Encoding.UTF8,
               "application/json");
            var response = await _client.SendAsync(msg);

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Post_EndpointsReturn400_wheninvalidcardnumber()
        {
            // Act
            var msg = new HttpRequestMessage(HttpMethod.Post, "api/payments");
            msg.Content = new StringContent(
               JsonConvert.SerializeObject(new
               {
                   card = new
                   {
                       number = "4111a 1111 1111 1111",
                       expiry = "23/2021",
                       cvv = 123
                   },
                   amount = 1,
                   currency = "EUR"
               }),
               Encoding.UTF8,
               "application/json");
            var response = await _client.SendAsync(msg);

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Post_EndpointsReturn400_wheninvalidamount()
        {
            // Act
            var msg = new HttpRequestMessage(HttpMethod.Post, "api/payments");
            msg.Content = new StringContent(
               JsonConvert.SerializeObject(new
               {
                   card = new
                   {
                       number = "4111a 1111 1111 1111",
                       expiry = "23/2021",
                       cvv = 123
                   },
                   amount = -1,
                   currency = "EUR"
               }),
               Encoding.UTF8,
               "application/json");
            var response = await _client.SendAsync(msg);

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
