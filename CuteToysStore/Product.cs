using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuteToysStore
{
    internal class Product
    {
        private string _image;
        public uint Id { get; }
        public string Name { get;}
        public decimal Price { get;}
        public string Image
        {
            get => "Assets/images/" + _image;
            set 
            {
                _image = value;
            }
        }

        public Product(uint id, string name, decimal price, string image)
        {
            Id = id;
            Name = name;
            Price = price;
            Image = image;
        }
    }
}
