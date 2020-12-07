using Checkout.Com.PaymentGateway.Models;
using Checkout.Com.PaymentGateway.Services.Persistance.Contracts;
using Checkout.Com.PaymentGateway.Services.Persistance.PersistanceExceptions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;
using AutoMapper;
using DBMODEL = Checkout.Com.PaymentGateway.Services.Persistance.Models;
using Microsoft.Extensions.Options;
using Checkout.Com.PaymentGateway.Models.Options;

namespace Checkout.Com.PaymentGateway.Services.Persistance
{
    public class QueryService : IQueryService
    {
        private readonly MongoClient _mongoClient;
        private readonly IOptions<DatabaseOptions> _options;
        private readonly IMapper _mapper;
        public QueryService(MongoClient mongoClient, 
            IMapper mapper,
            IOptions<DatabaseOptions> options)
        {
            _mongoClient = mongoClient;
            _options = options;
            _mapper = mapper;
        }

        public async Task<Payment> GetAsync(Guid paymentId)
        {
            var collection = _mongoClient.GetDatabase(_options.Value.Name).GetCollection<BsonDocument>("payments");
            var filter = Builders<BsonDocument>.Filter.Eq("PublicId", paymentId);
            var result = await collection.FindAsync(filter);
            var document = await result.FirstOrDefaultAsync();
            if (document != null)
            {
                var k = BsonSerializer.Deserialize<DBMODEL.PaymentStatus>(document);
                return _mapper.Map<Payment>(k.Payment);
            }
            throw new PaymentNotFoundException($"There is no payment with id {paymentId}");
        }
    }
}
