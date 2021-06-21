
namespace ShopASP.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Productos
    {
        public int Id { get; set; }
        public string nom_pro { get; set; }
        public string des_pro { get; set; }
        public double precio { get; set; }
        public string img { get; set; }
    }
}
