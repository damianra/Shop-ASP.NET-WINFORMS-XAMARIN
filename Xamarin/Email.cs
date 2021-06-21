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
    [Activity(Label = "ShopApp - Email")]
    public class Email : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Email);
            EditText msj = FindViewById<EditText>(Resource.Id.mensaje);
            Button enviar = FindViewById<Button>(Resource.Id.enviar);
            //boton enviar
            enviar.Click += (s, e) =>
            {   //se crea un intent con la accion enviar
                Intent email = new Intent(Intent.ActionSend);
                email.PutExtra(Intent.ExtraText, msj.Text.ToString());// se pasa el texto del EditText
                email.SetType("message/rfc822");
                StartActivity(Intent.CreateChooser(email, "Send Email Via")); //se elije la app con la cual se pueda enviar emails
            };



        }




    }
}