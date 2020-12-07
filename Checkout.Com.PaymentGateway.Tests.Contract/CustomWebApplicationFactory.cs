using DM = Checkout.Com.PaymentGateway.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Checkout.Com.PaymentGateway.Services.Persistance.Contracts;
using Moq;
using System;
using Checkout.Com.PaymentGateway.Services.Persistance.PersistanceExceptions;
using Checkout.Com.PaymentGateway.Services.PaymentProcessor.Contracts;

namespace Checkout.Com.PaymentGateway.Tests.Contract
{
    public class CustomWebApplicationFactory<TStartup>
    : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override IWebHostBuilder CreateWebHostBuilder() => base.CreateWebHostBuilder().UseEnvironment("Testing");
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            var queryServiceMock = new Mock<IQueryService>();

            queryServiceMock.Setup(x => x.GetAsync(It.Is<Guid>(x => x == Guid.Parse("02599216-92e5-416b-80d7-a0fc579072b5"))))
                .ReturnsAsync(new Models.Payment());
            queryServiceMock.Setup(x => x.GetAsync(It.Is<Guid>(x => x == Guid.Parse("02599216-92e5-416b-80d7-a0fc579072b6"))))
                .ThrowsAsync(new PaymentNotFoundException("payment not found"));

            builder.ConfigureServices(services =>
            {
                services.AddScoped<IQueryService>(serviceProvider => queryServiceMock.Object);
            });
        }
    }
}
