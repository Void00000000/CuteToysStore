using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuteToysStore
{
    internal class Product
    {
        public uint Id { get; }
        public string Name { get; set; }
        public decimal Price { get;}
        public string Image { get; set; }

        public Product(uint id, string name, decimal price, string image)
        {
            Id = id;
            Name = name;
            Price = price;
            Image = image;
        }
    }
}
