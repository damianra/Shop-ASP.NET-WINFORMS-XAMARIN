using ShopASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ShopASP.Controllers
{
    public class shopController : ApiController
    {
        ProductosEntities pro = new ProductosEntities();
        ProductosEntities2 fac = new ProductosEntities2();
        // GET: api/shop
        public IEnumerable<Productos> Get()
        {          
            var productos = (from p in pro.Productos select p).ToList();

            return productos;
        }

        // GET: api/shop/5
        public Productos Get(int id)
        {
            var producto = (from p in pro.Productos where p.Id == id select p).First();
            return producto;
        }

        // POST: api/shop
        public RespuestaJSON Post([FromBody]Facturacion value)
        {
            fac.Facturacion.Add(value);
            fac.SaveChanges();
            RespuestaJSON msj = new RespuestaJSON();
            msj.MSJ = "Su Compra se ha realizado exitosamente";
            return msj;
        }

        // PUT: api/shop/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/shop/5
        public void Delete(int id)
        {
        }
    }
}
