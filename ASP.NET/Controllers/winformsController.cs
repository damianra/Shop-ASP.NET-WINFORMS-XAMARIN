using ShopASP.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ShopASP.Controllers
{
    public class winformsController : ApiController
    {
        ProductosEntities pro = new ProductosEntities();
        // GET: api/winforms
        public IEnumerable<Productos> Get()
        {
            var productos = (from p in pro.Productos select p).ToList();

            return productos;
        }

        // GET: api/winforms/5
        public Productos Get(int id)
        {
            var producto = (from p in pro.Productos where p.Id == id select p).First();
            return producto;
        }

        // POST: api/winforms
        public RespuestaJSON Post([FromBody]ProPOST P)
        {
            byte[] arr = Convert.FromBase64String(P.imgB64);
            MemoryStream ms = new MemoryStream(arr);
            Image image = Image.FromStream(ms);
            var carpeta = Path.Combine(HttpContext.Current.Server.MapPath("~/img") + "\\" + P.Nimg);
            image.Save(carpeta);

            Productos Npro = new Productos();

            Npro.nom_pro = P.nombre;
            Npro.des_pro = P.descripcion;
            Npro.precio = P.precio;
            Npro.img = @"http://192.168.56.1:3099/img/" + P.Nimg;

            pro.Productos.Add(Npro);
            pro.SaveChanges();

            RespuestaJSON msj = new RespuestaJSON();
            msj.MSJ = "Se ha cargado un nuevo producto exitosamente";
            return msj;

        }

        // PUT: api/winforms/5
        public RespuestaJSON Put(int id, [FromBody]ProductoU Npro)
        {
            var producto = (from p in pro.Productos where p.Id == id select p).First();
            producto.nom_pro = Npro.nom_pro;
            producto.des_pro = Npro.des_pro;
            producto.precio = Npro.precio;
            pro.SaveChanges();

            RespuestaJSON msj = new RespuestaJSON();
            msj.MSJ = "Se han actualizado los datos del producto exitosamente";
            return msj;
        }

        // DELETE: api/winforms/5
        public RespuestaJSON Delete(int id)
        {
            var producto = (from p in pro.Productos where p.Id == id select p).First();
            string Nimg = producto.img.Replace("http://192.168.56.1:3099/img/", "");
            var img = Path.Combine(HttpContext.Current.Server.MapPath("~/img/") + Nimg);
            File.Delete(img);
            pro.Productos.Remove(producto);
            pro.SaveChanges();

            RespuestaJSON msj = new RespuestaJSON();
            msj.MSJ = "Se ha eliminado el producto exitosamente";
            return msj;
        }
    }
}
