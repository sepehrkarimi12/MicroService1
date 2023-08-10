﻿using Dapper;
using Discount.Api.Entities;
using Npgsql;

namespace Discount.Api.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IConfiguration _configuration;

        public DiscountRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Coupon> GetDiscount(string productName)
        {
            using (var connection = new NpgsqlConnection(this._configuration.GetValue<string>("DatabaseSettings:ConnectionString")))
            {
                var coupon0 = await connection.QueryFirstOrDefaultAsync<Coupon>("SELECT * FROM Coupon");
                var coupon1 = await connection.QueryFirstOrDefaultAsync<Coupon>(
                    "SELECT * FROM Coupon WHERE Id = @Id",
                    new { Id = 1 });
                var coupon2 = await connection.QueryFirstOrDefaultAsync<Coupon>(
                    "SELECT * FROM Coupon WHERE Id = 1");
                var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>(
                        "SELECT * FROM Coupon WHERE ProductName = @ProductName", 
                        new { ProductName = productName });
                if (coupon == null)
                    return new Coupon { ProductName = "No Discount", Amount = 0, Description = "NO Discount"};
                return coupon;
            }
        }

        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            using (var connection = new NpgsqlConnection(this._configuration.GetValue<string>("DatabaseSettings:ConnectionString")))
            {
                var affected = await connection.ExecuteAsync(
                    "INSERT INTO Coupon (ProductName, Description, Amount) VALUES (@ProductName, @Description, @Amount)", 
                    new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount}
                );
                return affected != 0;
            }
        }

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            using (var connection = new NpgsqlConnection(this._configuration.GetValue<string>("DatabaseSettings:ConnectionString")))
            {
                var affected = await connection.ExecuteAsync(
                    "UPDATE Coupon SET ProductName = @ProductName, Description = @Description, Amount = @Amount Where Id=@id",
                    new { Id = coupon.Id, ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount }
                );
                return affected != 0;
            }
        }

        public async Task<bool> DeleteDiscount(string productName)
        {
            using (var connection = new NpgsqlConnection(this._configuration.GetValue<string>("DatabaseSettings:ConnectionString")))
            {
                var affected = await connection.ExecuteAsync(
                    "DELETE FROM Coupon WHERE ProductName = @ProductName",
                    new { ProductName = productName }
                );
                return affected != 0;
            }
        }
    }
}
