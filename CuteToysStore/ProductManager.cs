using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Controls;

namespace CuteToysStore
{
    internal enum SortParams
    {
        AscendingPrice,
        DescendingPrice,
        AscendingName,
        DescendingName,
    }
    static internal class ProductManager
    {
        static public List<Product> Products { get; private set; }
        static ProductManager() 
        {
            JArray a;
            if (File.Exists(ApplicationData.Current.LocalFolder.Path + "\\products.json"))
                a = JArray.Parse(File.ReadAllText(ApplicationData.Current.LocalFolder.Path + "\\products.json"));
            else
                a = JArray.Parse(File.ReadAllText(@"Assets\json\products.json"));
            Products = a.ToObject<List<Product>>();
        }
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

        static public void SaveProducts()
        {
            string path = ApplicationData.Current.LocalFolder.Path;
            string json = JsonConvert.SerializeObject(Products);
            File.WriteAllText(path + "\\products.json", json);
        }
    }
}
