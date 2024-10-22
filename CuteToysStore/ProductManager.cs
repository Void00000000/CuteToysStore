using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Windows.Storage;

namespace CuteToysStore
{
    internal enum SortParams
    {
        AscendingPrice,
        DescendingPrice,
        AscendingName,
        DescendingName,
    }
    /// <summary>
    /// Класс, содержащий методы для управлением продуктами из панели магазина и корзины
    /// </summary> 
    static internal class ProductManager
    {
        // Есть ли продукт с id в магазине
        private static Dictionary<uint, bool> isAvailable;
        // Продукты из панели магазина
        public static ObservableCollection<Product> Products { get; private set; }
        // Продукты из корзины
        public static ObservableCollection<CartProduct> CartProducts { get; private set; }

        /// <summary>
        /// Читает продукты из панели магазина из .json файла
        /// </summary>
        static ProductManager()
        {
            // Загрузка продуктов для панели магазина
            JArray a;
            if (File.Exists(ApplicationData.Current.LocalFolder.Path + "\\products.json"))
                a = JArray.Parse(File.ReadAllText(ApplicationData.Current.LocalFolder.Path + "\\products.json"));
            else
                a = JArray.Parse(File.ReadAllText(@"Assets\json\products.json"));
            Products = a.ToObject<ObservableCollection<Product>>();

            // Загрузка продуктов для корзины
            if (File.Exists(ApplicationData.Current.LocalFolder.Path + "\\cart_products.json"))
            {
                a = JArray.Parse(File.ReadAllText(ApplicationData.Current.LocalFolder.Path + "\\cart_products.json"));
                CartProducts = a.ToObject<ObservableCollection<CartProduct>>();
            }
            else
                CartProducts = new ObservableCollection<CartProduct>();

            // Создание словаря isAvailable
            isAvailable = new Dictionary<uint, bool>();
            foreach (Product product in Products)
            {
                isAvailable.Add(product.Id, false);
            }
            foreach (CartProduct cartProduct in CartProducts) {
                isAvailable[cartProduct.Product.Id] = true;
            }
        }

        /// <summary>
        /// Добавление продукта в корзину
        /// </summary>
        static public void AddProductToCart(Product product)
        {
            if (isAvailable[product.Id])
            {
                foreach (CartProduct cartProduct in CartProducts)
                    if (cartProduct.Product.Id == product.Id)
                        ++cartProduct.Quantity;
            }
            else
            {
                isAvailable[product.Id] = true;
                CartProducts.Add(new CartProduct(product, 1));
            }
        }

        /// <summary>
        /// Увеличение количества продукта в корзине на единицу
        /// Максимальное количество продуктов 99
        /// </summary>
        static public void IncreaseProductQuantity(CartProduct cartProduct)
        {
            if (cartProduct.Quantity < 99)
                ++cartProduct.Quantity;
        }
        /// <summary>
        /// Уменьшение количества продукта в корзине на единицу
        /// </summary>
        static public void DecreaseProductQuantity(CartProduct cartProduct)
        {
            if (cartProduct.Quantity <= 1) {
                isAvailable[cartProduct.Product.Id] = false;
                CartProducts.Remove(cartProduct);
            }
            else
                --cartProduct.Quantity;
        }

        /// <summary>
        /// Удаление продукта из корзины
        /// </summary>
        static public void RemoveCartProduct(CartProduct cartProduct)
        {
            isAvailable[cartProduct.Product.Id] = false;
            CartProducts.Remove(cartProduct);
        }

        /// <summary>
        /// Сортировка продуктов из панели магазина
        /// </summary>
        static public void SortProducts(SortParams sortParam)
        {
            List<Product> sortedProducts = new List<Product>(Products);
            switch (sortParam)
            {
                case SortParams.AscendingPrice:
                    sortedProducts = sortedProducts.OrderBy(p => p.Price).ToList();
                    break;

                case SortParams.DescendingPrice:
                    sortedProducts = sortedProducts.OrderByDescending(p => p.Price).ToList();
                    break;
                case SortParams.AscendingName:
                    sortedProducts = sortedProducts.OrderBy(p => p.Name).ToList();
                    break;
                case SortParams.DescendingName:
                    sortedProducts = sortedProducts.OrderByDescending(p => p.Name).ToList();
                    break;
            }
            Products = new ObservableCollection<Product>(sortedProducts);
        }

        /// <summary>
        /// Сортировка продуктов из корзины
        /// </summary>
        static public void SortCartProducts(SortParams sortParam)
        {
            List<CartProduct> sortedProducts = new List<CartProduct>(CartProducts);
            switch (sortParam)
            {
                case SortParams.AscendingPrice:
                    sortedProducts = sortedProducts.OrderBy(p => p.Product.Price).ToList();
                    break;

                case SortParams.DescendingPrice:
                    sortedProducts = sortedProducts.OrderByDescending(p => p.Product.Price).ToList();
                    break;
                case SortParams.AscendingName:
                    sortedProducts = sortedProducts.OrderBy(p => p.Product.Name).ToList();
                    break;
                case SortParams.DescendingName:
                    sortedProducts = sortedProducts.OrderByDescending(p => p.Product.Name).ToList();
                    break;
            }
            CartProducts = new ObservableCollection<CartProduct>(sortedProducts);
        }

        /// <summary>
        /// Сохранение продуктов из панели магазина в папку ApplicationData.Current.LocalFolder
        /// перед закрытием приложения
        /// </summary>
        static public void SaveProducts()
        {
            string path = ApplicationData.Current.LocalFolder.Path;
            string json = JsonConvert.SerializeObject(Products);
            File.WriteAllText(path + "\\products.json", json);
        }

        /// <summary>
        /// Сохранение продуктов из корзины в папку ApplicationData.Current.LocalFolder
        /// перед закрытием приложения
        /// </summary>
        static public void SaveCartProducts()
        {
            string path = ApplicationData.Current.LocalFolder.Path;
            string json = JsonConvert.SerializeObject(CartProducts);
            File.WriteAllText(path + "\\cart_products.json", json);
        }
    }
}
