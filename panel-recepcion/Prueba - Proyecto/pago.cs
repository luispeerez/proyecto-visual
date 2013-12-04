using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

using System.IO;
//Libreria del pdf

using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Prueba___Proyecto
{
    public partial class pago : Form
    {
        double pagocliente, cambio;


        public void terminarProcesoOrden(int idorden)
        {
            conexion ins_pro = new conexion();
            ins_pro.crearConexion();
            string inserta = "UPDATE orden SET estatus='PAGADA' WHERE idorden=" + idorden + "";
            MySqlCommand pra = new MySqlCommand(inserta);
            pra.Connection = ins_pro.getConexion();
            pra.ExecuteNonQuery();
        }

        public string obtenerNombreMesero(int idorden)
        {
            string resultadoQuery;
            conexion search = new conexion();
            search.crearConexion();
            string search3 = "SELECT CONCAT(nombre,' ',apellidos) AS atendio FROM mesa WHERE idmesero IN( SELECT idmesero FROM orden WHERE idorden=" + idorden + ")";
            MySqlCommand buscarproductos = new MySqlCommand(search3, search.getConexion());
            resultadoQuery = (buscarproductos.ExecuteScalar()).ToString();
            search.cerrarConexion();

            return resultadoQuery;
        }

        public void resetearMesa(int idorden)
        {
            conexion ins_pro = new conexion();
            ins_pro.crearConexion();
            string inserta = "UPDATE mesa SET estatus='Disponible',idcliente=null  WHERE idmesa IN( SELECT idmesa FROM orden WHERE idorden="+ idorden+")";
            MySqlCommand pra = new MySqlCommand(inserta);
            pra.Connection = ins_pro.getConexion();
            pra.ExecuteNonQuery();
        }

        public void generarFactura()
        {
        }

        public pago()
        {

            InitializeComponent();
            textBox1.Enabled = false;
            textBox3.Enabled = false;
            textBox1.Text = String.Format("{0:0.00}", variables.TotalOrden);
        }

        private void pago_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                pagocliente = Convert.ToDouble(textBox2.Text);
                if (pagocliente > variables.TotalOrden)
                {
                    cambio = pagocliente - variables.TotalOrden;
                    textBox3.Text = String.Format("{0:0.00}", cambio);
                    terminarProcesoOrden(variables.IdordenApagar);
                    resetearMesa(variables.IdordenApagar);
                    //MessageBox.Show("Orden pagada!");

                    //Abriendo el pdf
                    System.Diagnostics.Process.Start("Factura.pdf");

                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            panelcobro menucobro = new panelcobro();
            menucobro.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Recepcion panelrecepcion = new Recepcion();
            panelrecepcion.ShowDialog();
        }
    }
}
