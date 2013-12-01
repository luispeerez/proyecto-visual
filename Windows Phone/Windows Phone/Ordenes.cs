using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MySql.Data.MySqlClient;

namespace Windows_Phone
{
    public partial class Ordenes : Form
    {
        public Ordenes()
        {
            InitializeComponent();
            panel2.Visible = false;
            VerificaOrdenes();
        }

        public void VerificaOrdenes()
        {
            try
            {
                Conexion CantidadOrdenes = new Conexion();
                CantidadOrdenes.crearConexion();
                string Comando = "SELECT idorden FROM orden ORDER BY idorden DESC LIMIT 1;";
                MySqlCommand Busqueda = new MySqlCommand(Comando, CantidadOrdenes.getConexion());
                string Resultado = (Busqueda.ExecuteScalar()).ToString();
                Button[] Objeto = new Button[Convert.ToInt32(Resultado) - 22]; // Resta 22 porque el id de orden comienza en 23, requiere reseteo.
                int y = 29;
                for (int contador = 0; contador < Objeto.Length; contador++)
                {
                    Conexion Busqueda2 = new Conexion();
                    Busqueda2.crearConexion();
                    Comando = "SELECT estatus FROM orden WHERE idorden = " + (contador + 23) + ";";
                    MySqlCommand BusquedaIdO = new MySqlCommand(Comando, Busqueda2.getConexion());
                    string Estatus = (BusquedaIdO.ExecuteScalar()).ToString();
                    Busqueda2.cerrarConexion();
                    if (Estatus == "ABIERTA")
                    {
                        Objeto[contador] = new Button();
                        Objeto[contador].Size = new Size(260, 62);
                        Objeto[contador].Location = new Point(3, y);
                        Objeto[contador].FlatStyle = FlatStyle.Flat;
                        Objeto[contador].BackColor = Color.Transparent;
                        Objeto[contador].FlatAppearance.MouseDownBackColor = Color.Transparent;
                        Objeto[contador].FlatAppearance.MouseOverBackColor = Color.Transparent;
                        Objeto[contador].FlatAppearance.BorderSize = 0;
                        Objeto[contador].ForeColor = Color.White;
                        Objeto[contador].TextAlign = ContentAlignment.MiddleLeft;
                        Objeto[contador].Cursor = Cursors.Cross;
                        Objeto[contador].Font = new Font("Microsoft Sans Serif", 10);
                        panel1.Controls.Add(Objeto[contador]);
                        Objeto[contador].Click += new EventHandler(ClickOrden);
                        Objeto[contador].LostFocus += new EventHandler(CambiarColor);
                        Comando = "SELECT idorden FROM orden WHERE idorden = " + (contador + 23) + ";"; // Suma 23 porque el id de orden comienza en 23, requiere reseteo.
                        MySqlCommand BusquedaIdOrden = new MySqlCommand(Comando, CantidadOrdenes.getConexion());
                        Resultado = (BusquedaIdOrden.ExecuteScalar()).ToString();
                        Objeto[contador].Name = Resultado;
                        Objeto[contador].Text = "ORDEN " + Resultado;
                        Comando = "SELECT idmesa FROM orden WHERE idorden = " + (contador + 23) + ";"; // Suma 23 porque el id de orden comienza en 23, requiere reseteo.
                        MySqlCommand BusquedaMesa = new MySqlCommand(Comando, CantidadOrdenes.getConexion());
                        Resultado = (BusquedaMesa.ExecuteScalar()).ToString();
                        Objeto[contador].Text += "\nMESA " + Resultado;
                        Comando = "SELECT estatus FROM orden WHERE idorden = " + (contador + 23) + ";"; // Suma 23 porque el id de orden comienza en 23, requiere reseteo.
                        MySqlCommand BusquedaEstatus = new MySqlCommand(Comando, CantidadOrdenes.getConexion());
                        Resultado = (BusquedaEstatus.ExecuteScalar()).ToString();
                        Objeto[contador].Text += " - ESTATUS " + Resultado;
                        y += 60;
                    }
                }
                CantidadOrdenes.cerrarConexion();
            }
            catch (Exception)
            {

            }
            panel1.Focus();
        }

        public void ClickOrden(object sender, EventArgs e)
        {
            string IdObjeto = "";
            Button boton = sender as Button;
            if (boton != null)
                IdObjeto = boton.Name;
            variables.numeroorden = IdObjeto;

            //Manda al Menú
            Menú crear = new Menú();
            this.Hide();
            crear.ShowDialog();
        }

        public void CambiarColor(object sender, EventArgs e)
        {
            Button boton = sender as Button;
            boton.ForeColor = Color.White;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            textBox1.Focus();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                //VERIFICA QUE EXISTA LA MESA
                Conexion Mesas = new Conexion();
                Mesas.crearConexion();
                string Comando = "SELECT estatus FROM mesa WHERE idmesa=" + textBox1.Text + ";";
                MySqlCommand Busqueda = new MySqlCommand(Comando, Mesas.getConexion());
                string Resultado = (Busqueda.ExecuteScalar()).ToString();
                Mesas.cerrarConexion();
                if (Resultado == "Ocupada")
                {
                    //CREA LA NUEVA ORDEN
                    Conexion Crear_Orden = new Conexion();
                    Crear_Orden.crearConexion();
                    Comando = "INSERT INTO orden (idmesa, idmesero, fecha, total, estatus, actualizado) values (" + textBox1.Text + ", " + variables.idmesero + ", NOW(), 0, 'ABIERTA', 1)";
                    MySqlCommand Insercion = new MySqlCommand(Comando);
                    Insercion.Connection = Crear_Orden.getConexion();
                    Insercion.ExecuteNonQuery();
                    Crear_Orden.cerrarConexion();
                    VerificaOrdenes();
                    panel1.Focus();
                    panel2.Visible = false;
                }
                else
                    textBox1.Text = "MESA " + textBox1.Text + "DISPONIBLE.";
            }
            catch (Exception)
            {
                textBox1.Text = "NO EXISTE LA MESA. " + textBox1.Text + ".";
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Menú crear = new Menú();
            this.Hide();
            crear.ShowDialog();
        }
    }
}
