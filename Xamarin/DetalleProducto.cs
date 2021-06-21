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
using System.Threading.Tasks;
using System.Json;
using System.Net;
using System.IO;
using Android.Graphics;
using Newtonsoft.Json;


namespace ShopApp
{
    [Activity(Label = "DetalleProducto")]
    public class DetalleProducto : Activity
    {
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.DetalleProducto);

            TextView titulo = FindViewById<TextView>(Resource.Id.titulo);
            ImageView foto = FindViewById<ImageView>(Resource.Id.imageView1);
            TextView des = FindViewById<TextView>(Resource.Id.des);
            Button precio = FindViewById<Button>(Resource.Id.com);


            JsonValue json = await ObtenerJSON();
            ParseAndDisplay(json);

            precio.Click += delegate
            {
                ProductoL pro = new ProductoL();
                pro.id = Convert.ToInt32(Intent.GetStringExtra("id"));
                pro.nombre = titulo.Text;
                pro.precio = Convert.ToDouble(precio.Text);
                ListaProductos.productos.Add(pro);
                Toast.MakeText(this, "El producto se añadio al carrito", ToastLength.Short).Show();
            };

            
        }


        private async Task<JsonValue> ObtenerJSON()
        {
            string id = Intent.GetStringExtra("id");

            string url = "http://192.168.56.1:3099/api/shop/" + id;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(url));
            request.ContentType = "application/json";
            request.Method = "GET";

            using (WebResponse response = await request.GetResponseAsync())
            {

                using (Stream stream = response.GetResponseStream())
                {

                    JsonValue jsonDoc = await Task.Run(() => JsonObject.Load(stream));
                    Console.Out.WriteLine("Response: {0}", jsonDoc.ToString());

                    return jsonDoc;
                }
            }
        }


        private void ParseAndDisplay(JsonValue json)
        {
            TextView titulo = FindViewById<TextView>(Resource.Id.titulo);
            ImageView foto = FindViewById<ImageView>(Resource.Id.imageView1);
            TextView des = FindViewById<TextView>(Resource.Id.des);
            Button precio = FindViewById<Button>(Resource.Id.com);

            var img = GetImageBitmapFromUrl(json["img"]);

                titulo.Text = json["nom_pro"];
                foto.SetImageBitmap(img);
                des.Text = json["des_pro"];
                precio.Text = json["precio"].ToString();

        }

        private Bitmap GetImageBitmapFromUrl(string url)
        {
            Bitmap imagenBitmap = null;

            using (var webclient = new WebClient())
            {
                var imgBytes = webclient.DownloadData(url);

                if (imgBytes != null && imgBytes.Length > 0)
                {
                    imagenBitmap = BitmapFactory.DecodeByteArray(imgBytes, 0, imgBytes.Length);
                }
            }
            return imagenBitmap;
        }

    }
}