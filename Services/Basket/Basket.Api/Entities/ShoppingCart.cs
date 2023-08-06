namespace Basket.Api.Entities
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            
        }

        public ShoppingCart(string userName)
        {
            this.UserName = userName;
        }

        public string UserName { get; set; }
        public List<ShoppingCartItem> Items { get; set; }

        public decimal TotalPrice => Items.Sum(item => item.Price * item.Quantity);
    }
}
