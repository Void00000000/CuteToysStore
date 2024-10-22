using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace CuteToysStore
{
    /// <summary>
    /// Неполная реализация аналогичного класса из WPF
    /// </summary>
    internal static class NavigationService
    {
        private static Frame _frame;

        /// <summary>
        /// Установка фрейма
        /// </summary>
        public static void SetFrame(Frame frame)
        {
            _frame = frame;
        }

        /// <summary>
        /// Переход на страницу pageType
        /// </summary>
        public static void Navigate(Type pageType)
        {
            if (_frame != null)
            {
                _frame.Navigate(pageType, null, new EntranceNavigationTransitionInfo());
            }
        }
    }
}
