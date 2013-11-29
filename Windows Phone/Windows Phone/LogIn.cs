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
    public partial class LogIn : Form
    {
        public LogIn()
        {
            InitializeComponent();
            variables.numeroorden = " - ";
        }

        string nombre, apellidos;

        public int VerificarUsuario()
        {
            try
            {
                string nickname, pass;
                nickname = textBox1.Text;
                pass = textBox2.Text;
                string[] Valores = new string[7];
                Conexion Datos = new Conexion();
                Datos.crearConexion();
                string Comando = "SELECT *FROM usuario WHERE (nickname = '" + nickname + "') AND (pass = '" + pass + "') AND (area = 'Mesero') AND (estatus = 'Disponible');";
                MySqlCommand Busqueda = new MySqlCommand(Comando, Datos.getConexion());
                MySqlDataAdapter Vuelta = new MySqlDataAdapter(Busqueda);
                DataSet Resultado = new DataSet();
                Busqueda.Connection = Datos.getConexion();
                Vuelta.Fill(Resultado, "Usuario");
                Valores[0] = Resultado.Tables["Usuario"].Rows[0][0].ToString(); //idusuario
                Valores[1] = Resultado.Tables["Usuario"].Rows[0][1].ToString(); //nombre
                Valores[2] = Resultado.Tables["Usuario"].Rows[0][2].ToString(); //apellidos
                Valores[3] = Resultado.Tables["Usuario"].Rows[0][3].ToString(); //nickname
                Valores[4] = Resultado.Tables["Usuario"].Rows[0][4].ToString(); //pass
                Valores[5] = Resultado.Tables["Usuario"].Rows[0][5].ToString(); //area
                Valores[6] = Resultado.Tables["Usuario"].Rows[0][6].ToString(); //estatus

                nombre = Valores[1] = Resultado.Tables["Usuario"].Rows[0][1].ToString();
                apellidos = Valores[2] = Resultado.Tables["Usuario"].Rows[0][2].ToString();
                return Convert.ToInt32(Valores[0] = Resultado.Tables["Usuario"].Rows[0][0].ToString());
            }
            catch (Exception)
            {
                return 0;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((VerificarUsuario()) != 0)
            {
                Conexion idMesero = new Conexion();
                idMesero.crearConexion();
                string Comando = "SELECT idmesero FROM mesero WHERE (nombre = '" + nombre + "') AND (apellidos = '" + apellidos + "');";
                MySqlCommand Busqueda = new MySqlCommand(Comando, idMesero.getConexion());
                string Resultado = (Busqueda.ExecuteScalar()).ToString();
                variables.nombre = nombre;
                variables.idmesero = Convert.ToInt32(Resultado);
                Menú crear = new Menú();
                this.Hide();
                crear.ShowDialog();
            }
            else
                MessageBox.Show("Usuario o contraseña incorrecta");
        }
    }
}
