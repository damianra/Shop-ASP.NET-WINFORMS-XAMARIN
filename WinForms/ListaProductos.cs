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
    public partial class ListaProductos : Form
    {
        public ListaProductos()
        {
            InitializeComponent();
        }

        private void ListaProductos_Load(object sender, EventArgs e)
        {
            List<Producto> Lpro = JsonData.ObtenerProductos();

            DataTable DT = new DataTable();

            DT.Columns.Add("ID");
            DT.Columns.Add("Titulo");
            DT.Columns.Add("Descripcion");
            DT.Columns.Add("Precio");
            DT.Columns.Add("Imagen");

            foreach (var P in Lpro)
             {
                DataRow DR = DT.NewRow();

                DR["ID"] = P.id;
                DR["Titulo"] = P.nom_pro;
                DR["Descripcion"] = P.des_pro;
                DR["Precio"] = P.precio;
                DR["Imagen"] = P.img;

                DT.Rows.Add(DR);
            }
            dataGridView1.DataSource = DT;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EditProducto Edit = new EditProducto();
            Edit.EditProducto_Load(Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value));
            Edit.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RespJSON res = new RespJSON();
            res = JsonData.DELdata(Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value));
            MessageBox.Show(res.MSJ);
        }
    }
}
