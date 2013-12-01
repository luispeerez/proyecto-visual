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
    public partial class Pedidos : Form
    {
        public Pedidos()
        {
            InitializeComponent();
            VerificaPedidos();
            panel2.Visible = false;
        }

        string IdObjeto = "";

        public void VerificaPedidos()
        {
            try
            {
                Conexion CantidadPedidos = new Conexion();
                CantidadPedidos.crearConexion();
                string Comando = "SELECT idpedido FROM pedido ORDER BY idpedido DESC LIMIT 1;";
                MySqlCommand ObtenerCantidad = new MySqlCommand(Comando, CantidadPedidos.getConexion());
                int Resultado = Convert.ToInt32(ObtenerCantidad.ExecuteScalar());
                CantidadPedidos.cerrarConexion();
                Button[] Objeto = new Button[Convert.ToInt32(Resultado)];
                int y = 29;
                for (int contador = 0; contador < Objeto.Length; contador++)
                {
                    Conexion Busqueda = new Conexion();
                    Busqueda.crearConexion();
                    Comando = "SELECT idorden FROM pedido WHERE idpedido = " + (contador + 1) + ";";
                    MySqlCommand BusquedaIdO = new MySqlCommand(Comando, Busqueda.getConexion());
                    string IdOrden = (BusquedaIdO.ExecuteScalar()).ToString();
                    if (IdOrden == variables.numeroorden)
                    {
                        Objeto[contador] = new Button();
                        Objeto[contador].Size = new Size(260, 52);
                        Objeto[contador].Location = new Point(3, y);
                        Objeto[contador].FlatStyle = FlatStyle.Flat;
                        Objeto[contador].BackColor = Color.SlateGray;
                        Objeto[contador].ForeColor = Color.White;
                        panel1.Controls.Add(Objeto[contador]);
                        Objeto[contador].Click += new EventHandler(ClickPedido);
                        Objeto[contador].Name = (contador + 1).ToString();
                        Objeto[contador].Text = "ORDEN " + variables.numeroorden;

                        Comando = "SELECT idalimento FROM pedido WHERE idpedido = " + (contador + 1) + ";";
                        MySqlCommand BusquedaAlimento = new MySqlCommand(Comando, Busqueda.getConexion());
                        string IdAlimento = (BusquedaAlimento.ExecuteScalar()).ToString();

                        Comando = "SELECT nombre FROM alimento WHERE idalimento = " + IdAlimento + ";";
                        MySqlCommand BusquedaNombre = new MySqlCommand(Comando, Busqueda.getConexion());
                        string Nombre = (BusquedaNombre.ExecuteScalar()).ToString();

                        Objeto[contador].Text += " - PEDIDO " + Nombre;

                        Comando = "SELECT estatus FROM pedido WHERE idpedido = " + (contador + 1) + ";";
                        MySqlCommand BusquedaEstatus = new MySqlCommand(Comando, Busqueda.getConexion());
                        string Estatus = (BusquedaEstatus.ExecuteScalar()).ToString();
                        Objeto[contador].Text += " - ESTADO " + Estatus;
                        y += 60;
                    }
                    Busqueda.cerrarConexion();
                }
            }
            catch (Exception)
            {
            }
            panel1.Focus();
        }

        public void ClickPedido(object sender, EventArgs e)
        {
            Button boton = sender as Button;
            if (boton != null)
                IdObjeto = boton.Name;
            panel2.Visible = true;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Menú crear = new Menú();
            this.Hide();
            crear.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            variables.numeroorden = " - ";
            Menú crear = new Menú();
            this.Hide();
            crear.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Menú crear = new Menú();
            this.Hide();
            crear.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Conexion Modificar = new Conexion();
            Modificar.crearConexion();
            string Comando = "UPDATE pedido SET estatus='Entregado', actualizado = 1 WHERE idpedido = " + IdObjeto + ";";
            MySqlCommand Editar = new MySqlCommand(Comando);
            Editar.Connection = Modificar.getConexion();
            Editar.ExecuteNonQuery();
            Modificar.cerrarConexion();
            panel1.Focus();
            panel2.Visible = false;
            Pedidos crear = new Pedidos();
            this.Hide();
            crear.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Conexion Modificar = new Conexion();
            Modificar.crearConexion();
            string Comando = "UPDATE pedido SET estatus='Cancelado', actualizado = 1 WHERE idpedido = " + IdObjeto + ";";
            MySqlCommand Editar = new MySqlCommand(Comando);
            Editar.Connection = Modificar.getConexion();
            Editar.ExecuteNonQuery();
            Modificar.cerrarConexion();
            panel1.Focus();
            panel2.Visible = false;
            Pedidos crear = new Pedidos();
            this.Hide();
            crear.ShowDialog();
        }
    }
}
