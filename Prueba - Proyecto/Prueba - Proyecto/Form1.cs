using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prueba___Proyecto
{
    public partial class Form1 : Form
    {
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
            }
        }

        private void textBox2_MouseClick(object sender, MouseEventArgs e)
        {
            if (click2 == 0)
            {
                textBox2.Text = "";
                click2 = 1;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            InicioSesion prueba = new InicioSesion();
            prueba.ShowDialog();
        }
    }
}
