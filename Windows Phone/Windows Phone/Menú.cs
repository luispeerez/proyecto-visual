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
    public partial class Menú : Form
    {
        public Menú()
        {
            InitializeComponent();
            label2.Text="ORDEN " + variables.numeroorden + ".";
            if (variables.numeroorden != " - ")
                button7.Visible = true;
            var timer = new Timer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = 30000; //30 segundos
            timer.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Menú_Alimento crear = new Menú_Alimento();
            this.Hide();
            crear.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Ordenes crear = new Ordenes();
            this.Hide();
            crear.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Menú_Plato crear = new Menú_Plato();
            this.Hide();
            crear.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Menú_Postre crear = new Menú_Postre();
            this.Hide();
            crear.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Menú_Bebidas crear = new Menú_Bebidas();
            this.Hide();
            crear.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Pedidos crear = new Pedidos();
            this.Hide();
            crear.ShowDialog();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            int CambiosPedido = 0;
            panel1.Visible = false;
            Conexion CantidadPedidos = new Conexion();
            CantidadPedidos.crearConexion();
            string Comando = "SELECT idpedido FROM pedido ORDER BY idpedido DESC LIMIT 1;";
            MySqlCommand ObtenerCantidad = new MySqlCommand(Comando, CantidadPedidos.getConexion());
            int Resultado = Convert.ToInt32(ObtenerCantidad.ExecuteScalar());
            CantidadPedidos.cerrarConexion();
            for (int contador = 0; contador < Resultado; contador++)
            {
                Conexion Busqueda = new Conexion();
                Busqueda.crearConexion();
                Comando = "SELECT estatus FROM pedido WHERE idpedido = " + (contador + 1) + ";";
                MySqlCommand BusquedaIdO = new MySqlCommand(Comando, Busqueda.getConexion());
                string Estatus = (BusquedaIdO.ExecuteScalar()).ToString();
                if (Estatus != "Pendiente")
                {
                    //VERIFICA ESTADO DE ACTUALIZACION
                    Comando = "SELECT actualizado FROM pedido WHERE idpedido = " + (contador + 1) + ";";
                    MySqlCommand BusquedaAct = new MySqlCommand(Comando, Busqueda.getConexion());
                    int Actualizacion = Convert.ToInt32(BusquedaAct.ExecuteScalar());
                    if (Actualizacion == 1)
                    {
                        CambiosPedido++;
                        Conexion Modificar = new Conexion();
                        Modificar.crearConexion();
                        Comando = "UPDATE pedido SET actualizado = 0 WHERE idpedido = " + (contador + 1) + ";";
                        MySqlCommand Editar = new MySqlCommand(Comando);
                        Editar.Connection = Modificar.getConexion();
                        Editar.ExecuteNonQuery();
                        Modificar.cerrarConexion();
                    }
                }
                Busqueda.cerrarConexion();
            }
            if (CambiosPedido != 0)
            {
                panel1.Visible = true;
                Button Objeto = new Button();
                Objeto = new Button();
                Objeto.Size = new Size(306, 28);
                Objeto.Location = new Point(1, 1);
                Objeto.FlatStyle = FlatStyle.Flat;
                Objeto.BackColor = Color.Transparent;
                Objeto.FlatAppearance.BorderSize = 0;
                Objeto.ForeColor = Color.White;
                panel1.Controls.Add(Objeto);
                Objeto.Click += new EventHandler(ClickPedido);
                Objeto.Text = "*PEDIDOS ACTUALIZADOS";
                Objeto.Visible = true;
                CambiosPedido = 0;
            }
        }

        public void ClickPedido(object sender, EventArgs e)
        {
            panel1.Visible = false;
            Pedidos crear = new Pedidos();
            this.Hide();
            crear.ShowDialog();
        }
    }
}
