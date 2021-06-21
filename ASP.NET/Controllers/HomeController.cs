using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopASP.Models;
using System.Web.Security;

namespace ShopASP.Controllers
{
    public class HomeController : Controller
    {
        ProductosEntities pro = new ProductosEntities();
        ProductosEntities2 fac = new ProductosEntities2();
        
        // GET: Home
        public ActionResult Index()
        {
            return View(pro.Productos.ToList());
        }

        // Detalles del Producto
        public ActionResult DetalleProducto(int id)
        {   //Selecciona producto por medio de consulta LINQ
            var producto = (from p in pro.Productos where p.Id == id select p).First();
            return View(producto);
        }

        //Recibe datos por POST
        [HttpPost]     //Recibe id del producto que se añade
        public ActionResult AnadirCarrito(int id)
        {
            //Lista con objetos ProductosCarrito
            List<ProductoCarrito> productosEnCarr = new List<ProductoCarrito>(); 
            var prod = (from p in pro.Productos where p.Id == id select p).First();
            //Objeto ProductoCarrito
            ProductoCarrito items = new ProductoCarrito();
            //se añaden los valores del producto al objeto ProductoCarrito
            items.id = prod.Id;
            items.nombre = prod.nom_pro;
            items.precio = prod.precio;
            //si la sesion "carrito" no contiene datos
           if(Session["carrito"] == null)
            {   //se añade el objeto en la list
                productosEnCarr.Add(items);
                Session["carrito"] = productosEnCarr; //se guarda la lista en la sesion "carrito"
            }//si no
            else
            {//se guardan los datos de la sesion
                List<ProductoCarrito> LisSess = Session["carrito"] as List<ProductoCarrito>;
                LisSess.Add(items);//se añaden datos a la lista de la sesion
                Session["carrito"] = LisSess;//se guarda la lista modificada en la sesion
            }

            return RedirectToAction("Index");
        }

        public ActionResult MostrarCarrito()
        {  //Datos de la sesion dentro de una Lista de objetos ProductosCarrito
            List<ProductoCarrito> LisSess = Session["carrito"] as List<ProductoCarrito>;
            //si la sesion no contiene datos
            if(Session["carrito"] == null)
            {   //Redirecciona a la pagina Index
                return RedirectToAction("Index");
            }//si contiena
            else
            {   //Suma los valores almacenados en la columna Precio
                var producto = (from p in LisSess select p.precio).Sum();
                ViewBag.Total = producto;//Variable ViewBag con el valor de la suma de la consulta
                return View(LisSess);//Devuelve vista con los datos de la sesion
            }
        }

        [HttpPost]
        public ActionResult QuitarDelCarro(int id)
        {   //Se tomn los valores de la sesion y se almacenan en una lista
            List<ProductoCarrito> LisSess = Session["carrito"] as List<ProductoCarrito>;
            var producto = (from p in LisSess where p.id == id select p).First(); //consulta de los valores de la lista segun id
            LisSess.Remove(producto);//se remueve el producto de la lista que se desea remover
            Session["carrito"] = LisSess;//se actualiza la sesion guardando los nuevos valores de la lista
            return RedirectToAction("MostrarCarrito");
        }

        [HttpPost]
        public ActionResult Facturar()
        {
            List<ProductoCarrito> LisSess = Session["carrito"] as List<ProductoCarrito>;
            //Si la lista o la sesion es null o esta vacia
            if (Session["carrito"] == null || LisSess.Count() == 0)
            {   //Redirecciona al Index
                return RedirectToAction("Index");
            }
            else
            {   //se crea objeto facturacion
                Facturacion F = new Facturacion();
                var Total = (from p in LisSess select p.precio).Sum();//Total de la compra
                var date = DateTime.Now;   //Fecha de la compra
                F.Total = Total;
                F.Fecha_com = date;
                fac.Facturacion.Add(F);    //se agrega a la facturacion el objeto facturacion
                fac.SaveChanges();        //se guardan los cambios en la BD
                Session["carrito"] = null;    //se reinician los valores de la sesion y se redirecciona al index
                return RedirectToAction("CompraRealizada");
            }
  
        }

        public ActionResult CompraRealizada()
        {
            ViewBag.Success = "La compra fue realizada exitosamente";
            return View();
        }

    }
}