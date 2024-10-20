using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.ServiceModel.Channels;
using Windows.ApplicationModel.Store;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace CuteToysStore
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void NavigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            NavigationViewItem item = args.InvokedItemContainer as NavigationViewItem;
            if (item == null) return;
            string clickedViewItem = item.Tag?.ToString();
            var view = Assembly.GetExecutingAssembly().GetType($"CuteToysStore.{clickedViewItem}");
            if (string.IsNullOrEmpty(clickedViewItem) || view == null) return;
            ContentFrame.Navigate(view, null, new EntranceNavigationTransitionInfo());
        }

        private void NavigationView_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (NavigationViewItemBase item in NavView.MenuItems)
                if (item.Tag.ToString() == "StoreView")
                    NavView.SelectedItem = item;
            ContentFrame.Navigate(typeof(StoreView));
        }
    }
}
