using Catalog.Api.Entities;
using MongoDB.Driver;

namespace Catalog.Api.Data
{
    public class CatalogContext : ICatalogContext
    {
        public IMongoCollection<Product> Products { get; }

        public CatalogContext(IConfiguration _configuration)
        {
            var client = new MongoClient(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(_configuration.GetValue<string>("DatabaseSettings:DataBaseName"));
            this.Products = database.GetCollection<Product>(_configuration.GetValue<string>("DatabaseSettings:CollectionName"));
            CatalogContextSeed.SeedData(this.Products);
        }
    }
}
