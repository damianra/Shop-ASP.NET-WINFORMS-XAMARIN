using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ShopASP.Models;

namespace ShopASP.Controllers
{
    public class facController : ApiController
    {
        ProductosEntities2 fac = new ProductosEntities2();
        // GET: api/fac
        public IEnumerable<Facturacion> Get()
        {
            var facturas = (from f in fac.Facturacion select f).ToList();
            return facturas;
        }

        // GET: api/fac/5
        public Facturacion Get(int id)
        {
            var factura = (from f in fac.Facturacion where f.Id_fac == id select f).First();
            return factura;
        }

        // POST: api/fac
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/fac/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/fac/5
        public void Delete(int id)
        {
        }
    }
}
