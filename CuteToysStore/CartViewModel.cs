using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        public decimal OverallPrice => CalcOverallPrice();
        public uint OverallQuantity => CalcOverallQuantity();
        public CartViewModel()
        {
            CartProducts = ProductManager.CartProducts;
            RemoveCartProductCommand = new RelayCommands<CartProduct>(RemoveCartProduct);
            IncreaseProductQuantityCommand = new RelayCommands<CartProduct>(IncreaseProductQuantity);
            DecreaseProductQuantityCommand = new RelayCommands<CartProduct>(DecreaseProductQuantity);
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

        private void IncreaseProductQuantity(CartProduct cartProduct)
        {
            ProductManager.IncreaseProductQuantity(cartProduct);
            NavigationService.Navigate(typeof(CartView));
        }

        private void DecreaseProductQuantity(CartProduct cartProduct)
        {
            ProductManager.DecreaseProductQuantity(cartProduct);
            NavigationService.Navigate(typeof(CartView));
        }

        private void RemoveCartProduct(CartProduct cartProduct)
        {
            ProductManager.RemoveCartProduct(cartProduct);
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
