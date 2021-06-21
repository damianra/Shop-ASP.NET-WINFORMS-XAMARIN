using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Data;

namespace WinShop
{
    public partial class AgregarProducto : Form
    {
        public AgregarProducto()
        {
            InitializeComponent();
        }

        OpenFileDialog SelIMG = new OpenFileDialog();

        private void button2_Click(object sender, EventArgs e)
        {
            SelIMG.InitialDirectory = "C://";
            SelIMG.Filter = "JPEG(*.JPG)|*.JPG|BMP(*.BMP)|*.BMP|GIF(*.GIF)|*.GIF";

            if (SelIMG.ShowDialog() == DialogResult.OK)
            {
                string imagen = SelIMG.FileName;
                pictureBox1.Image = Image.FromFile(imagen);
             /*   textBox1.Text = SelIMG.FileName;
                string nomYext = (Path.GetFileNameWithoutExtension(SelIMG.FileName)) + " " + (Path.GetExtension(SelIMG.FileName));
                richTextBox1.Text = nomYext;*/
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Image img = Image.FromFile(SelIMG.FileName);
            byte[] arr;
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                arr = ms.ToArray();
            }
            string B64 = Convert.ToBase64String(arr);
            string nomYext = (Path.GetFileNameWithoutExtension(SelIMG.FileName)) + (Path.GetExtension(SelIMG.FileName));

            ProPOST PP = new ProPOST();

            PP.nombre = textBox1.Text;
            PP.descripcion = richTextBox1.Text;
            PP.precio = Convert.ToDouble(textBox2.Text);
            PP.imgB64 = B64;
            PP.Nimg = nomYext;

            RespJSON resp = JsonData.POSTdata(PP);

            MessageBox.Show(resp.MSJ);

            pictureBox1.Image = null ;
            textBox1.Text = "";
            richTextBox1.Text = "";
            textBox2.Text = "";
        }
    }
}
