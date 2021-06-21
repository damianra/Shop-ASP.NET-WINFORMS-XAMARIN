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
    public partial class Facturacion : Form
    {
        public Facturacion()
        {
            InitializeComponent();
        }

        private void Facturacion_Load(object sender, EventArgs e)
        {
            List<Factura> fac = JsonData.ObtenerFacturas();

            DataTable DT = new DataTable();

            DT.Columns.Add("ID");
            DT.Columns.Add("Total");
            DT.Columns.Add("Fecha");
            
            foreach(var f in fac)
            {
                DataRow DR = DT.NewRow();

                DR["ID"] = f.Id_fac;
                DR["Total"] = f.Total;
                DR["Fecha"] = string.Format("{0:dd/MM/yyyy}", f.Fecha_com);
                DT.Rows.Add(DR);
            }

            dataGridView1.DataSource = DT;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DetalleFac D = new DetalleFac();
            D.DetalleFac_Load(Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value));
            D.Show();
        }
    }
}
