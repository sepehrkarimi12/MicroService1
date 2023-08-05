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
            return new List<Product>()
            {
                new Product
                {
                    Id="602d2149e773f2a3990b47f5",
                    Name="Iphone X",
                    Summary="This Phone is the company's biggest change to its flagship smartphone in years :)",
                    Description="sjgjgsjkhgsdjhadgjahgsdjhasgdhjagdjhjd jgdsjgsakkhsagdjhasgdjhagsdjhgdhjagdsh hgajsgdhjags",
                    ImageFile="Prod_1.png",
                    Price= 950.00M,
                    Category = "SmartPhone"
                },
                new Product
                {
                    Id="602d2149e773f2a3990b47f6",
                    Name="Samsung A10",
                    Summary="This Phone is the company's biggest change to its flagship smartphone in years :)",
                    Description="dkhdlkjdhkjd djhd jdhkjdh khjdh kdjhdkjhdkjhopsiuwjisjik oidhj",
                    ImageFile="Prod_2.png",
                    Price= 840.00M,
                    Category = "SmartPhone"
                },
                new Product
                {
                    Id="602d2149e773f2a3990b47f7",
                    Name="Xiaomi Poco X5 Pro",
                    Summary="This Phone is the company's biggest change to its flagship smartphone in years :)",
                    Description="dl;kjldjlkdjskjskllsihdjjhsghs hgs hsghs ghgshgshgshgss",
                    ImageFile="Prod_3.png",
                    Price= 900.00M,
                    Category = "SmartPhone"
                },
            };
        }
    }
}
