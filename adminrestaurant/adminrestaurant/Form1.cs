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

        int click1 = 0, click2 = 0;

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
            search.cerrarConexion();
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
            search.cerrarConexion();
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
            ins_pro.cerrarConexion();
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
                panel1.Visible = true;
                button1.Visible = false;
                textBox3.Focus();
            }
            //De lo contrario aparece el formulario para registrar al administrador
            else
            {
                panel1.Visible = false;
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
                            Form2 panel = new Form2();
                            this.Hide();
                            panel.ShowDialog();
                        }
                        else
                        {
                            pictureBox1.Visible = true;
                            pictureBox2.Visible = true;
                            pictureBox3.Visible = true;
                            pictureBox4.Visible = true;
                        }
                    }
                    else
                    {
                        pictureBox1.Visible = true;
                        pictureBox2.Visible = true;
                        pictureBox3.Visible = true;
                        pictureBox4.Visible = true;
                    }
                }
                else
                {
                    pictureBox1.Visible = true;
                    pictureBox2.Visible = true;
                    pictureBox3.Visible = true;
                    pictureBox4.Visible = true;
                }
            }
            catch (Exception error)
            {
                pictureBox1.Visible = true;
                pictureBox2.Visible = true;
                pictureBox3.Visible = true;
                pictureBox4.Visible = true;
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
                    panel1.Visible = false;
                }
                else
                {
                    pictureBox5.Visible = true;
                    pictureBox6.Visible = true;
                    pictureBox7.Visible = true;
                    pictureBox8.Visible = true;
                }
            }
            catch (Exception error)
            {
                pictureBox5.Visible = true;
                pictureBox6.Visible = true;
                pictureBox7.Visible = true;
                pictureBox8.Visible = true;
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (click1 == 0)
            {
                textBox1.Text = "";
                click1 = 1;
                textBox1.ForeColor = Color.DimGray;
            }

            textBox1.ForeColor = Color.DimGray;
            //Ocultando los mensajes de error
            pictureBox1.Visible = false;
            pictureBox2.Visible = false;
            pictureBox3.Visible = false;
            pictureBox4.Visible = false;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (click2 == 0)
            {
                //Vaciando el placeholder del textbox y editando el formato de entrada de texto
                textBox2.Text = "";
                click2 = 1;
            }
            textBox2.ForeColor = Color.DimGray;
            textBox2.UseSystemPasswordChar = true;
            //Ocultando los mensajes de error
            pictureBox1.Visible = false;
            pictureBox2.Visible = false;
            pictureBox3.Visible = false;
            pictureBox4.Visible = false;
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (click1 == 0)
            {
                textBox1.Text = "";
                click1 = 1;
                textBox1.ForeColor = Color.DimGray;
            }
            //Ocultando los mensajes de error
            pictureBox1.Visible = false;
            pictureBox2.Visible = false;
            pictureBox3.Visible = false;
            pictureBox4.Visible = false;
        }

        private void textBox2_MouseClick(object sender, MouseEventArgs e)
        {
            if (click2 == 0)
            {
                //Vaciando el placeholder del textbox y editando el formato de entrada de texto
                textBox2.Text = "";
                click2 = 1;

            }

            textBox2.ForeColor = Color.DimGray;
            textBox2.UseSystemPasswordChar = true;
            //Ocultando los mensajes de error
            pictureBox1.Visible = false;
            pictureBox2.Visible = false;
            pictureBox3.Visible = false;
            pictureBox4.Visible = false;
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

        private void button8_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
