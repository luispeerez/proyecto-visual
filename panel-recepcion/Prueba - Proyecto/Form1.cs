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

namespace Prueba___Proyecto
{
    public partial class Form1 : Form
    {

        //Validando que el usuario este registrado
        public int verificarUsuario()
        {
            int resultadoQuery;
            conexion search = new conexion();
            search.crearConexion();
            string search3 = "SELECT COUNT(*) FROM usuario WHERE (nickname = '" + textBox1.Text + "') AND (pass = '" + textBox2.Text + "') ";
            MySqlCommand buscarproductos = new MySqlCommand(search3, search.getConexion());
            resultadoQuery = Convert.ToInt32(buscarproductos.ExecuteScalar());

            return resultadoQuery;
        }

        public Form1()
        {
            InitializeComponent();
        }

        int click1 = 0, click2 = 0;

        private void button1_MouseHover(object sender, EventArgs e)
        {
            button1.Visible = false;
            button3.Visible = true;
        }

        private void button2_MouseHover(object sender, EventArgs e)
        {
            button2.Visible = false;
            button4.Visible = true;
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            button1.Visible = true;
            button3.Visible = false;
        }

        private void button4_MouseLeave(object sender, EventArgs e)
        {
            button2.Visible = true;
            button4.Visible = false;
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

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (click1 == 0)
            {
                textBox1.Text = "";
                click1 = 1;
                textBox1.ForeColor = Color.DimGray;
            }
            //Ocultando los mensajes de error
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
            pictureBox3.Visible = false;
            pictureBox4.Visible = false;
        }

        //Boton para inicio de sesion en hover(por eso no aparece en el form)
        private void button4_Click(object sender, EventArgs e)
        {
            //Evaluando que el formulario este completo
            //Evaluando que el usuario exista en la base de datos
            if (textBox1.Text != "" && textBox2.Text != "" && verificarUsuario() != 0)
            {
                this.Close();
                InicioSesion verifica = new InicioSesion();
                verifica.ShowDialog();
            }
            else
            {
                pictureBox3.Visible = true;
                pictureBox4.Visible = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

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
            pictureBox3.Visible = false;
            pictureBox4.Visible = false;
        }
    }
}
