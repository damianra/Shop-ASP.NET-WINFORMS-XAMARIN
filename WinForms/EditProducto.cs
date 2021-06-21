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
    public partial class EditProducto : Form
    {
        public EditProducto()
        {
            InitializeComponent();
        }

        public void EditProducto_Load(int id)
        {
            Producto pro = JsonData.ObProducto(id);

            picture.WaitOnLoad = false;
            picture.LoadAsync(pro.img);
            lblid.Text = id.ToString();
            nombre.Text = pro.nom_pro;
            descripcion.Text = pro.des_pro;
            precio.Text = pro.precio.ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            RespJSON res = new RespJSON();
            ProductoU PU = new ProductoU();
            PU.nom_pro = nombre.Text;
            PU.des_pro = descripcion.Text;
            PU.precio = Convert.ToDouble(precio.Text);
            res = JsonData.UpdatePro(Convert.ToInt32(lblid.Text), PU);
            MessageBox.Show(res.MSJ);
        }

        private void cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
