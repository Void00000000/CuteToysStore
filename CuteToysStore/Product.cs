using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CuteToysStore
{
    internal class Product
    {
        public uint Id { get; }
        public string Name { get; }
        public decimal Price { get; }
        public string Image { get; }

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
    internal class CartProduct : INotifyPropertyChanged
    {
        private uint _quantity;
        public Product Product { get; }
        public uint Quantity { 
            get 
            { 
                return _quantity; 
            } 
            set 
            { 
                _quantity = value; 
                OnPropertyChanged(nameof(Quantity)); 
            } 
        }
        public decimal Price { get => Product.Price * Quantity; }
        public CartProduct(Product product, uint quantity)
        {
            Product = product;
            Quantity = quantity;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propname)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propname));
        }
    }
}