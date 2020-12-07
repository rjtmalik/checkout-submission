using AutoMapper;
using Checkout.Com.PaymentGateway.Models.Options;
using Checkout.Com.PaymentGateway.Services.Encryption;
using Checkout.Com.PaymentGateway.Services.Encryption.Contracts;
using Checkout.Com.PaymentGateway.Services.PaymentProcessor;
using Checkout.Com.PaymentGateway.Services.PaymentProcessor.Contracts;
using Checkout.Com.PaymentGateway.Services.Persistance;
using Checkout.Com.PaymentGateway.Services.Persistance.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using Polly;
using Polly.Extensions.Http;
using Polly.Registry;
using System;

namespace Checkout.Com.PaymentGateway.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //swagger
            services.AddSwaggerGen();

            //Encryption
            services.Configure<EncryptionOptions>(Configuration.GetSection("Encryption"));
            services.AddSingleton<IEncryptionService, AesService>();

            //Persistance
            MongoClient dbClient = new MongoClient(Configuration.GetConnectionString("PaymentgatewayApp"));
            services.Configure<DatabaseOptions>(Configuration.GetSection("DB"));
            services.AddSingleton<MongoClient>(dbClient);
            services.AddScoped<ICommandService, CommandService>();
            services.AddScoped<IQueryService, QueryService>();

            //Automapper
            var mapperConfiguration = new MapperConfiguration(mc => { mc.AddProfile<MapperProfile>(); });
            services.AddSingleton(mapperConfiguration.CreateMapper());



            //Banks and httpClients
            
            var registry = new PolicyRegistry()
                    {
                        {
                    "defaultretrystrategy",
                            HttpPolicyExtensions.HandleTransientHttpError().WaitAndRetryAsync(
                            new[]
                                {
                                    TimeSpan.FromSeconds(1),
                                    TimeSpan.FromSeconds(5),
                                    TimeSpan.FromSeconds(10)
                                }
                            ) },
                        {
                    "defaultcircuitbreaker",
                            HttpPolicyExtensions.HandleTransientHttpError().CircuitBreakerAsync(
                            handledEventsAllowedBeforeBreaking: 3,
                            durationOfBreak: TimeSpan.FromSeconds(30)
                            ) },
                    };
            services.AddSingleton<IReadOnlyPolicyRegistry<string>>(registry);
            services.AddHttpClient("visa", c =>
            {
                c.BaseAddress = new Uri(Configuration.GetValue<string>("Visa:BaseUri"));
            })
                .AddPolicyHandlerFromRegistry("defaultretrystrategy")
                .AddPolicyHandlerFromRegistry("defaultcircuitbreaker");

            services.AddHttpClient("mastercard", c =>
            {
                c.BaseAddress = new Uri(Configuration.GetValue<string>("Mastercard:BaseUri"));
            })
                .AddPolicyHandlerFromRegistry("defaultretrystrategy")
                .AddPolicyHandlerFromRegistry("defaultcircuitbreaker");

            services.AddScoped<IProcessor, VisaProcessor>();
            services.AddScoped<IProcessor, MasterCardProcessor>();

            //Application Bootstraping
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
