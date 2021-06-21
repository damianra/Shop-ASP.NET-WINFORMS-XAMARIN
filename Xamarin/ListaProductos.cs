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
{
    public class ListaProductos
    {  // objeto lista estatica de ProductosL para el carrito
      public static List<ProductoL> productos { get; set; } = new List<ProductoL>();
    }
}