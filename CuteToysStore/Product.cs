namespace CuteToysStore
{
    internal class Product
    {
        public uint Id { get;}
        public string Name { get;}
        public decimal Price { get; }
        public string Image { get;}

        public Product(uint id, string name, decimal price, string image)
        {
            Id = id;
            Name = name;
            Price = price;
            Image = image;
        }
    }

    /// <summary>
    /// Продукты, добавленные в корзину
    /// </summary>
    internal class CartProduct
    {
        public Product Product { get; }
        public uint Quantity { get; set; }
        public decimal Price { get => Product.Price * Quantity; }
        public CartProduct(Product product, uint quantity)
        {
            Product = product;
            Quantity = quantity;
        }
    }
}
