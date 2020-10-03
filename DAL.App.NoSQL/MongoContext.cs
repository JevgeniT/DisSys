using Domain;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DAL.App.NoSQL
{
    public class MongoContext
    {
        private readonly IMongoCollection<Availability> _collection;
        private IMongoDatabase _db { get; set; }
        private MongoClient _mongoClient { get; set; }
        public MongoContext(INoSqlConnectionSettings settings)
        {
            var client = new MongoClient("mongodb://localhost:27017/dissys");
            var database = client.GetDatabase("dissys");
            _collection = database.GetCollection<Availability>("availability");
        }

        // public MongoContext(IOptions<MongoConnectionSettings> configuration)
        // {
        //     _mongoClient = new MongoClient(configuration.Value.ConnectionString);
        //     _db = _mongoClient.GetDatabase(configuration.Value.DatabaseName);
        // }
        public IMongoCollection<Availability> MongoAvailabilities => _collection;
        
        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _db.GetCollection<T>(name);
        }
    }
}