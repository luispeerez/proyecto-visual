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
            VerificaOrdenes();
        }

        public void VerificaOrdenes()
        {
            Conexion CantidadOrdenes = new Conexion();
            CantidadOrdenes.crearConexion();
            string Comando = "SELECT idorden FROM orden ORDER BY idorden DESC LIMIT 1;";
            MySqlCommand Busqueda = new MySqlCommand(Comando, CantidadOrdenes.getConexion());
            string Resultado = (Busqueda.ExecuteScalar()).ToString();
            Button[] Objeto = new Button[Convert.ToInt32(Resultado) - 4]; // Resta 4 porque el id de orden comienza en 5, requiere reseteo.
            int y = 29;
            for (int contador = 0; contador < Objeto.Length; contador++)
            {
                Objeto[contador] = new Button();
                Objeto[contador].Size = new Size(260, 52);
                Objeto[contador].Location = new Point(3, y);
                Objeto[contador].FlatStyle = FlatStyle.Flat;
                Objeto[contador].BackColor = Color.SlateGray;
                Objeto[contador].ForeColor = Color.White;
                panel1.Controls.Add(Objeto[contador]);
                Objeto[contador].Click += new EventHandler(ClickOrden);
                Comando = "SELECT idorden FROM orden WHERE idorden = " + (contador + 5) + ";"; // Suma 5 porque el id de orden comienza en 5, requiere reseteo.
                MySqlCommand BusquedaIdOrden = new MySqlCommand(Comando, CantidadOrdenes.getConexion());
                Resultado = (BusquedaIdOrden.ExecuteScalar()).ToString();
                Objeto[contador].Name = Resultado;
                Objeto[contador].Text = "ORDEN " + Resultado;
                Comando = "SELECT idmesa FROM orden WHERE idorden = " + (contador + 5) + ";"; // Suma 5 porque el id de orden comienza en 5, requiere reseteo.
                MySqlCommand BusquedaMesa = new MySqlCommand(Comando, CantidadOrdenes.getConexion());
                Resultado = (BusquedaMesa.ExecuteScalar()).ToString();
                Objeto[contador].Text += " - MESA " + Resultado;
                Comando = "SELECT estatus FROM orden WHERE idorden = " + (contador + 5) + ";"; // Suma 5 porque el id de orden comienza en 5, requiere reseteo.
                MySqlCommand BusquedaEstatus = new MySqlCommand(Comando, CantidadOrdenes.getConexion());
                Resultado = (BusquedaEstatus.ExecuteScalar()).ToString();
                Objeto[contador].Text += " - ESTATUS " + Resultado;
                y += 60;
            }
        }

        public void ClickOrden(object sender, EventArgs e)
        {
            string IdObjeto = "";
            string[] Valores = new string[5];
            Button boton = sender as Button;
            if (boton != null)
                IdObjeto = boton.Name;
            Conexion Datos = new Conexion();
            Datos.crearConexion();
            string Comando = "SELECT *FROM orden WHERE idorden = " + IdObjeto + ";";
            MySqlCommand Busqueda = new MySqlCommand(Comando, Datos.getConexion());
            MySqlDataAdapter Vuelta = new MySqlDataAdapter(Busqueda);
            DataSet Resultado = new DataSet();
            Busqueda.Connection = Datos.getConexion();
            Vuelta.Fill(Resultado, "Orden");
            Valores[0] = Resultado.Tables["Orden"].Rows[0][0].ToString(); //idalimento
            Valores[1] = Resultado.Tables["Orden"].Rows[0][1].ToString(); //nombre
            Valores[2] = Resultado.Tables["Orden"].Rows[0][2].ToString(); //tipoalimento
            Valores[3] = Resultado.Tables["Orden"].Rows[0][3].ToString(); //descripcion
            Valores[4] = Resultado.Tables["Orden"].Rows[0][4].ToString(); //precio
        }
    }
}
