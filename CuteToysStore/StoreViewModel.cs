using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CuteToysStore
{
    internal class StoreViewModel
    {
        public ObservableCollection<Product> Products { get; }
        public ICommand SortCommand { get; }
        public StoreViewModel() {
            Products = new ObservableCollection<Product>(ProductManager.Products);
            SortCommand = new RelayCommands<string>(Sort, CanSort);
        }

        private void Sort(string sortParam)
        {
            SortParams sp = (SortParams)Enum.Parse(typeof(SortParams), sortParam);
            ProductManager.SortProducts(sp);
            NavigationService.Navigate(typeof(StoreView));
        }

        private bool CanSort(string sortParam)
        {
            if (Enum.TryParse(sortParam, out SortParams _)) return true;
            return false;
        }
    }
}
