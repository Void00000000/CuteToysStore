using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace CuteToysStore
{
    internal static class NavigationService
    {
        private static Frame _frame;

        public static void SetFrame(Frame frame)
        {
            _frame = frame;
        }

        public static void Navigate(Type pageType)
        {
            if (_frame != null)
            {
                _frame.Navigate(pageType, null, new EntranceNavigationTransitionInfo());
            }
        }
    }
}
