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
using ShopAPP.Resources;
using Newtonsoft.Json;
using System.Json;
using System.Net;

namespace ShopAPP
{
    [Activity(Label = "CarritoActivity")]
    public class CarritoActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {         
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Carrito);
            ListView list = FindViewById<ListView>(Resource.Id.listView1);
            Button comprar = FindViewById<Button>(Resource.Id.button1);
            TextView total = FindViewById<TextView>(Resource.Id.total);
            //si la lista es null o no contiene productos
            if (ListaProductos.productos == null || ListaProductos.productos.Count == 0)
            {

            }//caso contrario crear clase CarritoAdapter e introducir la ListaProductos
            else
            {
                var adapter = new CarritoAdapter(this, ListaProductos.productos);
                list.Adapter = adapter;
            }
            //metodo que suma los productos de la lista
            SumTotal();
            //boton comprar
            comprar.Click += delegate
            {   //objeto Facturar con los datos de la compra
                Facturar fac = new Facturar();
                fac.Total = Convert.ToDouble(total.Text);
                fac.Fecha_com = DateTime.Now;
                string json = JsonConvert.SerializeObject(fac);
                string resp = EnviarJSON(json);
                respuesta(resp);

            };

        }
        //metodo que suma y mustra el total del carrito
        private void SumTotal()
        {
            TextView total = FindViewById<TextView>(Resource.Id.total);

            if (ListaProductos.productos == null || ListaProductos.productos.Count == 0)
            {
                total.Text = "0";
            }
            else
            {
                var suma = (from p in ListaProductos.productos select p.precio).Sum();
                total.Text = suma.ToString();
            }
        }
        //metodo que recibe una respuesta en JSON y los muestra en un cartel de alerta
        private void respuesta(string json)
        {
            MsjResp res = JsonConvert.DeserializeObject<MsjResp>(json);
            ListaProductos.productos.Clear();
            AlertDialog.Builder msj = new AlertDialog.Builder(this);
            AlertDialog alert = msj.Create();
            alert.SetTitle("Mensaje de respuesta");
            alert.SetMessage(res.MSJ);
            alert.SetButton("ok", (s, e) => { Finish(); });
            alert.Show();

        }
        //metodo que envia los datos a la API
        private JsonValue EnviarJSON(JsonValue json)
        {   
            string urlAPI = GetString(Resource.String.urlAPI);
            string result = "";
            //variable webClient
            using (var Client = new WebClient())
            {   //Header del Cliente
                Client.Headers[HttpRequestHeader.ContentType] = "application/json";
                //encia el JSON por POST y guarda el resultado en la variable result
                result = Client.UploadString(urlAPI, "POST", json);
            }
            return result;
        }

        //clase facturar con datos de la compra
        class Facturar
        {
            public double Total { get; set; }
            public DateTime Fecha_com { get; set; }
        }
        //clase para el mensaje de respuesta
        class MsjResp
        {
            public string MSJ { get; set; }
        }

    }
}