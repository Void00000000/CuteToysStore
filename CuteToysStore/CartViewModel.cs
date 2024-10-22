using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CuteToysStore
{
    /// <summary>
    /// ViewModel класс для окна корзины продуктов
    /// </summary>
    internal class CartViewModel
    {
        public ObservableCollection<CartProduct> CartProducts { get; private set; }
        public ICommand RemoveCartProductCommand { get; }
        public ICommand IncreaseProductQuantityCommand { get; }
        public ICommand DecreaseProductQuantityCommand { get; }
        public ICommand SortCartProductsCommand { get; }
        public decimal OverallPrice { get => CalcOverallPrice(); }
        public uint OverallQuantity { get => CalcOverallQuantity(); }
        public CartViewModel(Dictionary<uint, CartProduct> cartDictionary)
        {
            CartProducts = new ObservableCollection<CartProduct>();
            foreach (CartProduct cartProduct in cartDictionary.Values)
            {
                CartProducts.Add(cartProduct);
            }
            RemoveCartProductCommand = new RelayCommands<uint>(RemoveCartProduct);
            IncreaseProductQuantityCommand = new RelayCommands<uint>(IncreaseProductQuantity);
            DecreaseProductQuantityCommand = new RelayCommands<uint>(DecreaseProductQuantity);
            SortCartProductsCommand = new RelayCommands<string>(SortCartProducts, CanSortCartProducts);
        }

        private decimal CalcOverallPrice()
        {
            decimal sum = 0;
            foreach (CartProduct cartProduct in CartProducts)
                sum += cartProduct.Price;
            return sum;
        }

        private uint CalcOverallQuantity()
        {
            uint sum = 0;
            foreach (CartProduct cartProduct in CartProducts)
                sum += cartProduct.Quantity;
            return sum;
        }

        private void IncreaseProductQuantity(uint id)
        {
            ProductManager.IncreaseProductQuantity(id);
            NavigationService.Navigate(typeof(CartView));
        }

        private void DecreaseProductQuantity(uint id)
        {
            ProductManager.DecreaseProductQuantity(id);
            NavigationService.Navigate(typeof(CartView));
        }

        private void RemoveCartProduct(uint id)
        {
            ProductManager.RemoveCartProduct(id);
            NavigationService.Navigate(typeof(CartView));
        }

        private void SortCartProducts(string sortParam)
        {
            SortParams sp = (SortParams)Enum.Parse(typeof(SortParams), sortParam);
            ProductManager.SortCartProducts(sp);
            NavigationService.Navigate(typeof(CartView));
        }

        private bool CanSortCartProducts(string sortParam)
        {
            if (Enum.TryParse(sortParam, out SortParams _)) return true;
            return false;
        }
    }
}
