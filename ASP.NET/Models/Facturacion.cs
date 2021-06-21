
namespace ShopASP.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Facturacion
    {
        public int Id_fac { get; set; }
        public double Total { get; set; }
        public System.DateTime Fecha_com { get; set; }
    }
}
