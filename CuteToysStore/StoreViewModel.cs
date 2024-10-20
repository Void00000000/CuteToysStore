﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CuteToysStore
{
    internal class StoreViewModel
    {
        public ObservableCollection<Product> Products { get; }
        public StoreViewModel() {
            Products = new ObservableCollection<Product>(ProductManager.Products);
        }
    }
}
