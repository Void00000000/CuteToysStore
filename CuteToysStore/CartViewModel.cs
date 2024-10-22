using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace CuteToysStore
{
    /// <summary>
    /// ViewModel класс для окна корзины продуктов
    /// </summary>
    internal class CartViewModel : INotifyPropertyChanged
    {
        public TrulyObservableCollection<CartProduct> CartProducts { get; private set; }
        public ICommand RemoveCartProductCommand { get; }
        public ICommand IncreaseProductQuantityCommand { get; }
        public ICommand DecreaseProductQuantityCommand { get; }
        public ICommand SortCartProductsCommand { get; }
        public decimal OverallPrice { get => ProductManager.CalcOverallPrice(); }
        public uint OverallQuantity { get => ProductManager.CalcOverallQuantity(); }
        public CartViewModel()
        {
            CartProducts = ProductManager.CartProducts;
            RemoveCartProductCommand = new RelayCommands<CartProduct>(RemoveCartProduct);
            IncreaseProductQuantityCommand = new RelayCommands<CartProduct>(IncreaseProductQuantity);
            DecreaseProductQuantityCommand = new RelayCommands<CartProduct>(DecreaseProductQuantity);
            SortCartProductsCommand = new RelayCommands<string>(SortCartProducts, CanSortCartProducts);
        }

        private void IncreaseProductQuantity(CartProduct cartProduct)
        {
            ProductManager.IncreaseProductQuantity(cartProduct);
            OnPropertyChanged(nameof(OverallPrice));
            OnPropertyChanged(nameof(OverallQuantity));
        }

        private void DecreaseProductQuantity(CartProduct cartProduct)
        {
            ProductManager.DecreaseProductQuantity(cartProduct);
            OnPropertyChanged(nameof(OverallPrice));
            OnPropertyChanged(nameof(OverallQuantity));
        }

        private void RemoveCartProduct(CartProduct cartProduct)
        {
            ProductManager.RemoveCartProduct(cartProduct);
            OnPropertyChanged(nameof(OverallPrice));
            OnPropertyChanged(nameof(OverallQuantity));
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

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propname)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propname));
        }
    }
}
