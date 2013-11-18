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
    public partial class Recepcion : Form
    {
        public Recepcion()
        {
            InitializeComponent();
        }

        private void Recepcion_Load(object sender, EventArgs e)
        {

        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            button1.Visible = false;
            button4.Visible = true;
        }

        private void button4_MouseLeave(object sender, EventArgs e)
        {
            button1.Visible = true;
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
            button3.Visible = true;
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            button6.Visible = true;
            button3.Visible = false;
        }

        private void button8_MouseHover(object sender, EventArgs e)
        {
            button8.Visible = false;
            button11.Visible = true;
        }

        private void button11_MouseLeave(object sender, EventArgs e)
        {
            button8.Visible = true;
            button11.Visible = false;
        }

        private void button9_MouseHover(object sender, EventArgs e)
        {
            button9.Visible = false;
            button10.Visible = true;
        }

        private void button10_MouseLeave(object sender, EventArgs e)
        {
            button9.Visible = true;
            button10.Visible = false;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
    }
}
