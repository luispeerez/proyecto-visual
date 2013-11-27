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
    public partial class Menú_Alimento : Form
    {
        public Menú_Alimento()
        {
            InitializeComponent();
            VerificaAlimentos();
        }

        public void VerificaAlimentos()
        {
            Conexion CantidadAlimentos = new Conexion();
            CantidadAlimentos.crearConexion();
            string Comando = "SELECT idalimento FROM alimento ORDER BY idalimento DESC LIMIT 1;";
            MySqlCommand Busqueda = new MySqlCommand(Comando, CantidadAlimentos.getConexion());
            string Resultado = (Busqueda.ExecuteScalar()).ToString();
            Button[] Objeto = new Button[Convert.ToInt32(Resultado)];
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
                Objeto[contador].Click += new EventHandler(ClickAlimento);
                Comando = "SELECT idalimento FROM alimento WHERE idalimento = " + (contador + 1) + ";";
                MySqlCommand BusquedaId = new MySqlCommand(Comando, CantidadAlimentos.getConexion());
                Resultado = (BusquedaId.ExecuteScalar()).ToString();
                Objeto[contador].Name = Resultado;
                Comando = "SELECT nombre FROM alimento WHERE idalimento = " + (contador + 1) + ";";
                MySqlCommand BusquedaNombre = new MySqlCommand(Comando, CantidadAlimentos.getConexion());
                Resultado = (BusquedaNombre.ExecuteScalar()).ToString();
                Objeto[contador].Text = Resultado;
                y += 60;
            }
            
        }

        public void ClickAlimento(object sender, EventArgs e)
        {
            string IdObjeto="";
            string[] Valores = new string[5];
            Button boton = sender as Button;
            if (boton != null)
                IdObjeto = boton.Name;
            Conexion Datos = new Conexion();
            Datos.crearConexion();
            string Comando = "SELECT *FROM alimento WHERE idalimento = " + IdObjeto + ";";
            MySqlCommand Busqueda = new MySqlCommand(Comando, Datos.getConexion());
            MySqlDataAdapter Vuelta = new MySqlDataAdapter(Busqueda);
            DataSet Resultado = new DataSet();
            Busqueda.Connection = Datos.getConexion();
            Vuelta.Fill(Resultado, "Alimento");
            Valores[0] = Resultado.Tables["Alimento"].Rows[0][0].ToString(); //idalimento
            Valores[1] = Resultado.Tables["Alimento"].Rows[0][1].ToString(); //nombre
            Valores[2] = Resultado.Tables["Alimento"].Rows[0][2].ToString(); //tipoalimento
            Valores[3] = Resultado.Tables["Alimento"].Rows[0][3].ToString(); //descripcion
            Valores[4] = Resultado.Tables["Alimento"].Rows[0][4].ToString(); //precio
        }
    }
}
