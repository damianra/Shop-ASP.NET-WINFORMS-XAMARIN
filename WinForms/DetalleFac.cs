using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Data;

namespace WinShop
{
    public partial class DetalleFac : Form
    {
        public DetalleFac()
        {
            InitializeComponent();
        }

        public void DetalleFac_Load(int id)
        {
            Factura Fac = JsonData.ObFactura(id);

            label4.Text = Fac.Id_fac.ToString();
            label5.Text = Fac.Total.ToString();
            label6.Text = string.Format("{0:dd/MM/yyyy}", Fac.Fecha_com);

        }
    }
}
