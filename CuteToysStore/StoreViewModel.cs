using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CuteToysStore
{
    internal class StoreViewModel
    {
        public ObservableCollection<Product> Products { get; }
        public ICommand SortProductsCommand { get; }
        public ICommand AddProductToCartCommand { get; }
        public StoreViewModel() {
            Products = new ObservableCollection<Product>(ProductManager.Products);
            AddProductToCartCommand = new RelayCommands<uint>(AddProductToCart);
            SortProductsCommand = new RelayCommands<string>(SortProducts, CanSortProducts);
        }

        private void AddProductToCart(uint id)
        {
            ProductManager.AddProductToCart(id);
        }

        private void SortProducts(string sortParam)
        {
            SortParams sp = (SortParams)Enum.Parse(typeof(SortParams), sortParam);
            ProductManager.SortProducts(sp);
            NavigationService.Navigate(typeof(StoreView));
        }

        private bool CanSortProducts(string sortParam)
        {
            if (Enum.TryParse(sortParam, out SortParams _)) return true;
            return false;
        }
    }
}
