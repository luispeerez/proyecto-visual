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


namespace adminrestaurant
{
    public partial class Form1 : Form
    {

        //Funcion que cuenta los registros en los que el nombre de usuario y la contraseña que se ingresaron coincidan
        //en caso de devolver 0 significa que no existe ese usuario en la tabla
        public int verificarUsuario()
        {
            int resultadoQuery;
            conexion search = new conexion();
            search.crearConexion();
            string search3 = "SELECT COUNT(*) FROM usuario WHERE (nickname = '"+textBox1.Text+"') AND (pass = '"+textBox2.Text+"') ";
            MySqlCommand buscarproductos = new MySqlCommand(search3, search.getConexion());
            resultadoQuery = Convert.ToInt32(buscarproductos.ExecuteScalar());

            return resultadoQuery;
        }


        //Verificar que el usuario sea el administrador
        public bool verificarAdmin()
        {
            string resultadoQuery;
            bool resultado;
            conexion search = new conexion();
            search.crearConexion();
            string search3 = "SELECT area FROM usuario WHERE (nickname = '" + textBox1.Text + "') AND (pass = '" + textBox2.Text + "') ";
            MySqlCommand buscarproductos = new MySqlCommand(search3, search.getConexion());
            resultadoQuery = (buscarproductos.ExecuteScalar()).ToString();
            if (resultadoQuery == "Administrador")
                resultado = true;
            else
                resultado = false;
 
            return resultado;
        }

        public int contarUsuarios()
        {
            int resultadoQuery;
            conexion search = new conexion();
            search.crearConexion();
            string search3 = "SELECT COUNT(*) FROM usuario";
            MySqlCommand buscarproductos = new MySqlCommand(search3, search.getConexion());
            resultadoQuery = Convert.ToInt32(buscarproductos.ExecuteScalar());

            return resultadoQuery;
        }

        private void registrarAdmin()
        {
            conexion ins_pro = new conexion();
            ins_pro.crearConexion();
            string inserta = "INSERT INTO usuario (nombre, apellidos,nickname,pass,area,estatus) Values ('" + textBox3.Text + "', '" + textBox4.Text + "' , '" + textBox5.Text + "' , '" + textBox6.Text + "' , 'Administrador','Disponible')";
            MySqlCommand pro = new MySqlCommand(inserta);
            pro.Connection = ins_pro.getConexion();
            pro.ExecuteNonQuery();
            MessageBox.Show("Se ha ingresado satisfactoriamente", "Insercion exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void limpiar()
        {
            //Campos del login normal
            textBox1.Text = "";
            textBox2.Text = "";

            //Campos del sign up de administrador(ventana que sale por primera vez)
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Verificando que exista un usario administrador en la base de datos
            if (contarUsuarios() == 0)
            {
                groupBox1.Visible = true;
                button1.Visible = false;
                textBox3.Focus();
            }
            //De lo contrario aparece el formulario para registrar al administrador
            else
            {
                groupBox1.Visible = false;
                button1.Visible = true;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text != "" && textBox2.Text != "")
                {
                    //Verificando que el usuario este registrado previamente en la tabla
                    if (verificarUsuario() != 0)
                    {
                        //Verificando que el usuario tenga permisos de administrador
                        if (verificarAdmin() == true)
                        {
                            variables.UsuarioActivo = textBox1.Text;
                            this.Hide();
                            Form2 panel = new Form2();
                            panel.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("El usuario no cuenta con permisos de administrador");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Usuario o contraseña incorrecta");
                    }
                }
                else
                {
                    MessageBox.Show("El formato es invalido o incompleto");
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("El formato es invalido");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "" && textBox6.Text != "")
                {
                    registrarAdmin();
                    button1.Visible = true;
                    groupBox1.Visible = false;
                }
                else
                {
                    MessageBox.Show("El formato es invalido");
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("El formato es invalido");
            }
        }

        //Asignando eventos cuando se llena el ultimo campo de cada formulario
        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                button2.PerformClick();
                return;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                button1.PerformClick();
                return;
            }
        }
    }
}
