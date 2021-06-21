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
using System.Json;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using Android.Graphics;

namespace ShopAPP
{
    [Activity(Label = "ShopAPP")]
    public class DetalleActivity : Activity
    {
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.DetProducto);
            //controladores de donde obtener los datos del producto
            TextView titulo = FindViewById<TextView>(Resource.Id.textView1);
            ImageView foto = FindViewById<ImageView>(Resource.Id.imageView1);
            TextView des = FindViewById<TextView>(Resource.Id.textView2);
            Button precio = FindViewById<Button>(Resource.Id.button1);

            JsonValue json = await ObtenerJSON();
            ParseAndDisplay(json);

            //Al precionar el boton del precio
            precio.Click += delegate
            {   //objeto producto de lisa
                ProductoL pro = new ProductoL();
                //se obtiene el id del producto enviado por el intent
                pro.id = Convert.ToInt32(Intent.GetStringExtra("id"));
                pro.nombre = titulo.Text;
                pro.precio = Convert.ToDouble(precio.Text);
                pro.img = json["img"];
                ListaProductos.productos.Add(pro);//se guarda el producto en la Lista estatica
                Toast.MakeText(this, "El producto se añadio al carrito", ToastLength.Short).Show();
            };
        }

        //Metodo que devuelve como resultado un objeto JsonValue 
        private async Task<JsonValue> ObtenerJSON()
        {   //obtiene el id del producto enviado por el Intent
            string id = Intent.GetStringExtra("id");
            //obtiene la url de la app guardada en String.xml
            string urlAPI = GetString(Resource.String.urlAPI);
            string url = urlAPI + "/" + id;
            //crea una consulta HTTP usando la URL de la API
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(url));
            //Tipo de consulta
            request.ContentType = "application/json";
            //Metodo de la consulta
            request.Method = "GET";
            //Envia la consulta y espera una respuesta
            using (WebResponse response = await request.GetResponseAsync())
            {   //obtiene un Stream que representa a la respuesta de la consulta HTTP
                using (Stream stream = response.GetResponseStream())
                {   //se utiliza el Stream para crear un documento JsonValue
                    JsonValue jsonDoc = await Task.Run(() => JsonObject.Load(stream));
                    Console.Out.WriteLine("Response: {0}", jsonDoc.ToString());

                    return jsonDoc;
                }
            }
        }
        //Metodo que obtiene un documento JSON y acomoda los datos para la visualización
        private void ParseAndDisplay(JsonValue json)
        {   //controladores donde se mostraran los datos del producto
            TextView titulo = FindViewById<TextView>(Resource.Id.textView1);
            ImageView foto = FindViewById<ImageView>(Resource.Id.imageView1);
            TextView des = FindViewById<TextView>(Resource.Id.textView2);
            Button precio = FindViewById<Button>(Resource.Id.button1);
            //se obtiene un objeto ImageBitMap desde la url del JSON
            var img = GetImageBitmapFromUrl(json["img"]);
            //se muestran los datos del JSON en los controladores
            titulo.Text = json["nom_pro"];
            foto.SetImageBitmap(img);
            des.Text = json["des_pro"];
            precio.Text = json["precio"].ToString();
        }
        //Metodo que obtiene un objeto BitMap (imagen) desde una URL
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