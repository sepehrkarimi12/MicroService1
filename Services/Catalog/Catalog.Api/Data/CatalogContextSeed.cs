using Catalog.Api.Entities;
using MongoDB.Driver;

namespace Catalog.Api.Data
{
    public class CatalogContextSeed
    {
        public static void SeedData(IMongoCollection<Product> products)
        {
            var existProduct = products.Find(p => true).Any();
            if (existProduct) return;
            products.InsertManyAsync(GetSeedData());
        }

        private static IEnumerable<Product> GetSeedData()
        {
            return new List<Product>
            {
                new Product
                {
                    Id = "1",
                    Name = "Product1 Name",
                    Summary = "Product1 Summary",
                    Description = "Product1 Description",
                    ImageFile = "Product1 ImageFile",
                    Price = 240.00M,
                    Category = "Product1 Category",
                },
                new Product
                {
                    Id = "2",
                    Name = "Product2 Name",
                    Summary = "Product2 Summary",
                    Description = "Product2 Description",
                    ImageFile = "Product2 ImageFile",
                    Price = 200.00M,
                    Category = "Product2 Category",
                },
                new Product
                {
                    Id = "3",
                    Name = "Product3 Name",
                    Summary = "Product3 Summary",
                    Description = "Product3 Description",
                    ImageFile = "Product3 ImageFile",
                    Price = 300.00M,
                    Category = "Product3 Category",
                },
                new Product
                {
                    Id = "4",
                    Name = "Product4 Name",
                    Summary = "Product4 Summary",
                    Description = "Product4 Description",
                    ImageFile = "Product4 ImageFile", 
                    Price = 400.00M,
                    Category = "Product4 Category",
                },
                new Product
                {
                    Id = "5",
                    Name = "Product5 Name",
                    Summary = "Product5 Summary",
                    Description = "Product5 Description",
                    ImageFile = "Product5 ImageFile",
                    Price = 500.00M,
                    Category = "Product5 Category",
                },
                new Product
                {
                    Id = "6",
                    Name = "Product6 Name",
                    Summary = "Product6 Summary",
                    Description = "Product6 Description",
                    ImageFile = "Product6 ImageFile",
                    Price = 600.00M,
                    Category = "Product6 Category",
                },
            };
        }
    }
}
