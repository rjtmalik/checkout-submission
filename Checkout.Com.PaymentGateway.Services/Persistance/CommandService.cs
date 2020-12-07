using AutoMapper;
using Checkout.Com.PaymentGateway.Models;
using Checkout.Com.PaymentGateway.Models.Options;
using Checkout.Com.PaymentGateway.Services.Encryption.Contracts;
using Checkout.Com.PaymentGateway.Services.Persistance.Contracts;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace Checkout.Com.PaymentGateway.Services.Persistance
{
    public class CommandService : ICommandService
    {
        private readonly MongoClient _mongoClient;
        private readonly IOptions<DatabaseOptions> _options;
        private readonly IEncryptionService _encryptionService;
        private readonly IMapper _mapper;
        public CommandService(MongoClient mongoClient,
            IEncryptionService encryptionService,
            IMapper mapper,
            IOptions<DatabaseOptions> options)
        {
            _mapper = mapper;
            _mongoClient = mongoClient;
            _encryptionService = encryptionService;
            _options = options;
        }

        public async Task SaveAsync(PaymentStatus paymentRequest)
        {
            var encrypted = _encryptionService.Encrypt(paymentRequest.Payment.Card.Number);
            var encString = Convert.ToBase64String(encrypted);
            var data = _mapper.Map<Models.PaymentStatus>(paymentRequest);
            data.Payment.Card.EncryptedNumber = encString;
            var collection = _mongoClient.GetDatabase(_options.Value.Name).GetCollection<BsonDocument>("payments");
            await collection.InsertOneAsync(data.ToBsonDocument());
        }
    }
}
