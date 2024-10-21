using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    internal class CartProduct : Product
    {
        public uint Quantity { get; set; }
        public CartProduct(uint id, string name, decimal price, string image, uint quantity) : base(id, name, price, image)
        {
            Quantity = quantity;
        }

        public CartProduct(Product product, uint quantity)
        : base(product.Id, product.Name, product.Price, product.Image)
        {
            Quantity = quantity;
        }
    }
}
