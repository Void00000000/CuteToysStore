using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CuteToysStore
{
    internal class CartViewModel
    {
        public ObservableCollection<CartProduct> CartProducts { get; set; }
        public ICommand RemoveCartProductCommand { get; }
        public CartViewModel(Dictionary<uint, CartProduct> cartDictionary)
        {
            CartProducts = new ObservableCollection<CartProduct>();
            foreach (CartProduct cartProduct in cartDictionary.Values)
            {
                CartProducts.Add(cartProduct);
            }
            RemoveCartProductCommand = new RelayCommands<uint>(RemoveCartProduct);
        }

        private void RemoveCartProduct(uint id)
        {
            ProductManager.RemoveCartProduct(id);
            NavigationService.Navigate(typeof(CartView));
        }
    }
}
