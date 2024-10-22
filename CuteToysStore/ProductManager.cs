using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
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
    /// Summary
    /// </summary> 
    static internal class ProductManager
    {
        // Словарь для быстрого получения доступа к продукту из панели магазина по его id
        static private Dictionary<uint, Product> productsDict;

        // Список продуктов из панели магазина
        static public List<Product> Products { get; private set; }
        
        // Словарь, хранящий продукты из корзины
        static public Dictionary<uint, CartProduct> CartProducts { get; private set; }
        static ProductManager()
        {
            // Если в папке ApplicationData.Current.LocalFolder ранее сохранялся список продуктов, то загружаются
            // продукты из него, иначе они загружаются из файла в папке Asssets
            JArray a;
            if (File.Exists(ApplicationData.Current.LocalFolder.Path + "\\products.json"))
                a = JArray.Parse(File.ReadAllText(ApplicationData.Current.LocalFolder.Path + "\\products.json"));
            else
                a = JArray.Parse(File.ReadAllText(@"Assets\json\products.json"));
            Products = a.ToObject<List<Product>>();
            productsDict = new Dictionary<uint, Product>();
            foreach (Product product in Products)
            {
                productsDict.Add(product.Id, product);
            }

            // Аналогично для продуктов из корзины
            if (File.Exists(ApplicationData.Current.LocalFolder.Path + "\\cart_products.json"))
            {
                CartProducts = JsonConvert.DeserializeObject<Dictionary<uint, CartProduct>>(File.ReadAllText(ApplicationData.Current.LocalFolder.Path + "\\cart_products.json"));
            }
            else
                CartProducts = new Dictionary<uint, CartProduct>();
        }

        /// <summary>
        /// Добавление продукта в корзину
        /// </summary>
        static public void AddProductToCart(uint id)
        {
            if (CartProducts.ContainsKey(id))
                CartProducts[id].Quantity++;
            else
                CartProducts.Add(id, new CartProduct(productsDict[id], 1));
        }

        /// <summary>
        /// Увеличение количества продукта в корзине на единицу
        /// Максимальное количество продуктов 99
        /// </summary>
        static public void IncreaseProductQuantity(uint id)
        {
            if (CartProducts[id].Quantity < 99)
                CartProducts[id].Quantity++;
        }
        /// <summary>
        /// Уменьшение количества продукта в корзине на единицу
        /// </summary>
        static public void DecreaseProductQuantity(uint id)
        {
            if (CartProducts[id].Quantity <= 1)
                CartProducts.Remove(id);
            else
                CartProducts[id].Quantity--;
        }

        /// <summary>
        /// Удаление продукта из корзины
        /// </summary>
        static public void RemoveCartProduct(uint id)
        {
            CartProducts.Remove(id);
        }

        /// <summary>
        /// Сортировка продуктов из панели магазина
        /// </summary>
        static public void SortProducts(SortParams sortParam)
        {
            switch (sortParam)
            {
                case SortParams.AscendingPrice:
                    Products = Products.OrderBy(p => p.Price).ToList();
                    break;

                case SortParams.DescendingPrice:
                    Products = Products.OrderByDescending(p => p.Price).ToList();
                    break;
                case SortParams.AscendingName:
                    Products = Products.OrderBy(p => p.Name).ToList();
                    break;
                case SortParams.DescendingName:
                    Products = Products.OrderByDescending(p => p.Name).ToList();
                    break;
            }
        }

        /// <summary>
        /// Сортировка продуктов из корзины
        /// </summary>
        static public void SortCartProducts(SortParams sortParam)
        {
            switch (sortParam)
            {
                case SortParams.AscendingPrice:
                    CartProducts = CartProducts.OrderBy(kvp => kvp.Value.Price)
                                       .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
                    break;

                case SortParams.DescendingPrice:
                    CartProducts = CartProducts.OrderByDescending(kvp => kvp.Value.Price)
                                       .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
                    break;
                case SortParams.AscendingName:
                    CartProducts = CartProducts.OrderBy(kvp => kvp.Value.Product.Name)
                                       .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
                    break;
                case SortParams.DescendingName:
                    CartProducts = CartProducts.OrderByDescending(kvp => kvp.Value.Product.Name)
                                       .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
                    break;
            }
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
