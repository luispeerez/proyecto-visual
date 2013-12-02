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

namespace Cocina___Proyecto
{
    public partial class ListaPedidos : Form
    {
        public ListaPedidos()
        {
            InitializeComponent();
            VerificaPedidos();
            label1.Visible = false;
            var timer = new Timer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = 30000; //30 segundos
            timer.Start();
            Conexion VerificaCambios = new Conexion();
            VerificaCambios.crearConexion();
            string Comando = "SELECT Pedido FROM actualizacion WHERE Nombre = 'Cambio';";
            MySqlCommand ObtenerCantidad = new MySqlCommand(Comando, VerificaCambios.getConexion());
            int Resultado = Convert.ToInt32(ObtenerCantidad.ExecuteScalar());
            if (Resultado == 1)
            {
                label1.Visible = true;
                Comando = "SELECT NumCamPedido FROM actualizacion WHERE Nombre = 'Cambio';";
                MySqlCommand ObtenerCant = new MySqlCommand(Comando, VerificaCambios.getConexion());
                int NumeroCambios = Convert.ToInt32(ObtenerCant.ExecuteScalar());
                VerificaCambios.cerrarConexion();
                label1.Text = NumeroCambios.ToString() + " NUEVO(S) PEDIDOS - HORA DE ACTUALIZACION " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
                Conexion Modificar = new Conexion();
                Modificar.crearConexion();
                Comando = "UPDATE actualizacion SET Pedido = 0, NumCamPedido = 0 WHERE Nombre = 'Cambio';";
                MySqlCommand Editar = new MySqlCommand(Comando);
                Editar.Connection = Modificar.getConexion();
                Editar.ExecuteNonQuery();
                Modificar.cerrarConexion();

            }
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
                Button[] Listo = new Button[Convert.ToInt32(Resultado)];
                int y = 10;
                for (int contador = 0; contador < Objeto.Length; contador++)
                {
                    Conexion Busqueda = new Conexion();
                    Busqueda.crearConexion();
                    Comando = "SELECT estatus FROM pedido WHERE idpedido = " + (contador + 1) + ";";
                    MySqlCommand BusquedaIdO = new MySqlCommand(Comando, Busqueda.getConexion());
                    string Estatus = (BusquedaIdO.ExecuteScalar()).ToString();
                    if (Estatus == "Pendiente")
                    {
                        Objeto[contador] = new Button();
                        Listo[contador] = new Button();
                        Objeto[contador].Size = new Size(660, 52);
                        Listo[contador].Size = new Size(100, 52);
                        Objeto[contador].Location = new Point(3, y);
                        Listo[contador].Location = new Point(670, y);
                        Objeto[contador].FlatStyle = FlatStyle.Flat;
                        Listo[contador].FlatStyle = FlatStyle.Flat;
                        Objeto[contador].BackColor = Color.DimGray;
                        Listo[contador].BackColor = Color.LimeGreen;
                        Objeto[contador].ForeColor = Color.White;
                        Listo[contador].ForeColor = Color.White;
                        Objeto[contador].FlatAppearance.MouseDownBackColor = SystemColors.InactiveCaptionText;
                        Objeto[contador].FlatAppearance.MouseOverBackColor = SystemColors.InactiveCaptionText;
                        Listo[contador].FlatAppearance.MouseDownBackColor = Color.ForestGreen;
                        Listo[contador].FlatAppearance.MouseOverBackColor = Color.ForestGreen;
                        Objeto[contador].FlatAppearance.BorderSize = 0;
                        Listo[contador].FlatAppearance.BorderSize = 0;
                        Objeto[contador].TextAlign = ContentAlignment.MiddleCenter;
                        Listo[contador].TextAlign = ContentAlignment.MiddleCenter;
                        Objeto[contador].Cursor = Cursors.Hand;
                        Listo[contador].Cursor = Cursors.Hand;
                        Objeto[contador].Font = new Font("Microsoft Sans Serif", 12);
                        Objeto[contador].Font = new Font("Microsoft Sans Serif", 12);
                        panel1.Controls.Add(Objeto[contador]);
                        panel1.Controls.Add(Listo[contador]);
                        Listo[contador].Click += new EventHandler(ClickListo);
                        Objeto[contador].Name = (contador + 1).ToString();
                        Listo[contador].Name = (contador + 1).ToString();
                        Listo[contador].Text = "LISTO";
                        //Obtiene numero de Orden
                        Comando = "SELECT idorden FROM pedido WHERE idpedido = " + (contador + 1) + ";";
                        MySqlCommand BusquedaOrden = new MySqlCommand(Comando, Busqueda.getConexion());
                        string IdOrden = (BusquedaOrden.ExecuteScalar()).ToString();
                        Objeto[contador].Text = "ORDEN " + IdOrden;
                        //Obtiene Nombre de Alimento
                        Comando = "SELECT idalimento FROM pedido WHERE idpedido = " + (contador + 1) + ";";
                        MySqlCommand BusquedaAlimento = new MySqlCommand(Comando, Busqueda.getConexion());
                        string IdAlimento = (BusquedaAlimento.ExecuteScalar()).ToString();
                        Comando = "SELECT nombre FROM alimento WHERE idalimento = " + IdAlimento + ";";
                        MySqlCommand BusquedaNombre = new MySqlCommand(Comando, Busqueda.getConexion());
                        string Nombre = (BusquedaNombre.ExecuteScalar()).ToString();
                        Objeto[contador].Text += " - PEDIDO " + (contador + 1) + " - " + Nombre;
                        //Obtiene Estado de pedido
                        Comando = "SELECT estatus FROM pedido WHERE idpedido = " + (contador + 1) + ";";
                        MySqlCommand BusquedaEstatus = new MySqlCommand(Comando, Busqueda.getConexion());
                        string EstatusP = (BusquedaEstatus.ExecuteScalar()).ToString();
                        Objeto[contador].Text += " - ESTADO " + EstatusP;
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

        public void ClickListo(object sender, EventArgs e)
        {
            Button boton = sender as Button;
            if (boton != null)
                IdObjeto = boton.Name;
            Conexion Modificar = new Conexion();
            Modificar.crearConexion();
            string Comando = "UPDATE pedido SET estatus='Listo', actualizado = 1 WHERE idpedido = " + IdObjeto + ";";
            MySqlCommand Editar = new MySqlCommand(Comando);
            Editar.Connection = Modificar.getConexion();
            Editar.ExecuteNonQuery();
            Modificar.cerrarConexion();
            panel1.Focus();
            ListaPedidos crear = new ListaPedidos();
            this.Hide();
            crear.ShowDialog();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            int Cambios = 0;
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
                if (Estatus == "Pendiente")
                {
                    //VERIFICA ESTADO DE ACTUALIZACION
                    Comando = "SELECT actualizado FROM pedido WHERE idpedido = " + (contador + 1) + ";";
                    MySqlCommand BusquedaAct = new MySqlCommand(Comando, Busqueda.getConexion());
                    int Actualizacion = Convert.ToInt32(BusquedaAct.ExecuteScalar());
                    if (Actualizacion == 1)
                    {
                        Cambios++;
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
            if (Cambios != 0)
            {
                Conexion Modificar = new Conexion();
                Modificar.crearConexion();
                Comando = "UPDATE actualizacion SET Pedido = 1, NumCamPedido = " + Cambios + " WHERE Nombre = 'Cambio';";
                MySqlCommand Editar = new MySqlCommand(Comando);
                Editar.Connection = Modificar.getConexion();
                Editar.ExecuteNonQuery();
                Modificar.cerrarConexion();
                ListaPedidos crear = new ListaPedidos();
                this.Hide();
                crear.ShowDialog();
            }
        }

        private void button5_MouseHover(object sender, EventArgs e)
        {
            button5.Visible = false;
            button7.Visible = true;
        }

        private void button7_MouseLeave(object sender, EventArgs e)
        {
            button5.Visible = true;
            button7.Visible = false;
        }

        private void button6_MouseHover(object sender, EventArgs e)
        {
            button6.Visible = false;
            button8.Visible = true;
        }

        private void button8_MouseLeave(object sender, EventArgs e)
        {
            button6.Visible = true;
            button8.Visible = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
    }
}
