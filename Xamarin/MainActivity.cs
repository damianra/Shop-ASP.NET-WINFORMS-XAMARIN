using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;
using System.Threading.Tasks;
using System.Net;
using System.Json;
using System.IO;
using System;
using System.Collections.Generic;
using ShopAPP.Resources;
using Android.Content;
using Android;

namespace ShopAPP
{
    [Activity(Label = "ShopAPP", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        ListView list;

        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            list = FindViewById<ListView>(Resource.Id.listView1);

            try
            {   
                JsonValue json = await ObtenerJSON();
                ParseAndDisplay(json);
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message, ToastLength.Long).Show();
            }
            //se utiliza la accion para cuando se seleccionta un producto de la lista
            list.ItemClick += (object sender, AdapterView.ItemClickEventArgs args)
                => listView_ItemClick(sender, args);

        }

        //Metodo que devuelve como resultado un objeto JsonValue 
        private async Task<JsonValue> ObtenerJSON()
        {   //obtiene la url de la app guardada en String.xml
            string urlAPI = GetString(Resource.String.urlAPI);
            //crea una consulta HTTP usando la URL de la API
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(urlAPI));
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
        {   //listview donde se mostraran los datos
            ListView list = FindViewById<ListView>(Resource.Id.listView1);
            //Lista de objetos producto donde deserializar los datos
            List<Producto> Lpro = new List<Producto>();
            //se recorren todos los datos del documento JSON
            foreach (JsonValue P in json)
            {   //se guardan los datos en un objeto producto
                Producto producto = new Producto();
                producto.id = P["Id"];
                producto.nombre = P["nom_pro"];
                producto.img = P["img"];
                producto.precio = P["precio"];
                Lpro.Add(producto); //adiere los datos a la lista
            }//variable adapter, objeto de CustomAdapter que se encarga de acomodar los datos en los controladores
            var adapter = new CustomAdapter(this, Lpro);
            list.Adapter = adapter;

        }

        //metodo que realiza una accion al presionar un objeto de la lista
        public void listView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {   //la variable item guarda el ID del objeto en la posicion que se a presionado
            var item = this.list.GetItemIdAtPosition(e.Position);
            //objeto Intent de la activity DetalleActivity
            Intent id = new Intent(this, typeof(DetalleActivity));
            //se envia el ID a al activity del Intent
            id.PutExtra("id", item.ToString());
            StartActivity(id);//se inicia la Activity del intent
        }

        //botones del ActionBar
        public override bool OnCreateOptionsMenu(IMenu menu)
        {   //se crea un MenuInflater con el menu_main.xml creado
            var inflater = MenuInflater;
            inflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }
        //metodo que realiza la accion correspondiente dependiendo el item seleccionado del Menu
        public override bool OnOptionsItemSelected(IMenuItem item)
        {   //se guarda el id del item seleccionado y se lo compara con los del menu abriendo la ventana correspondiente
            int id = item.ItemId;
            if (id == Resource.Id.Carrito)
            {
                Intent carr = new Intent(this, typeof(CarritoActivity));
                StartActivity(carr);
            }
            if (id == Resource.Id.Email)
            {
                 Intent mail = new Intent(this, typeof(Email));
                 StartActivity(mail);
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}

