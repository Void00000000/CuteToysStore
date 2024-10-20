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

namespace CuteToysStore
{
    static internal class ProductManager
    {
        static public List<Product> Products { get; }
        static ProductManager() 
        {
            JArray a = JArray.Parse(File.ReadAllText(@"Assets\json\products.json"));
            Products = a.ToObject<List<Product>>();
        }

    }
}
