using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace ShopAPP
{   //clase producto de lista
    public class ProductoL
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public double precio { get; set; }
        public string img { get; set; }
    }
}