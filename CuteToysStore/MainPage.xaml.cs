using Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection;
using Windows.UI.ViewManagement;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace CuteToysStore
{
    public sealed partial class MainPage : Page
    {
        Windows.ApplicationModel.Resources.ResourceLoader loader;
        public MainPage()
        {
            this.InitializeComponent();

            var appView = ApplicationView.GetForCurrentView();
            var titleBar = appView.TitleBar;
            titleBar.BackgroundColor = Colors.White;
            titleBar.ForegroundColor = Colors.Black;
            titleBar.ButtonBackgroundColor = Colors.White;
            titleBar.ButtonHoverBackgroundColor = Colors.LightGray;
            titleBar.ButtonPressedBackgroundColor = Colors.DarkGray;
            titleBar.ButtonInactiveBackgroundColor = Colors.Gray;

            NavigationService.SetFrame(ContentFrame);
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
