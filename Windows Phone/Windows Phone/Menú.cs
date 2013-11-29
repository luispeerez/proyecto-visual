using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Windows_Phone
{
    public partial class Menú : Form
    {
        public Menú()
        {
            InitializeComponent();
            label1.Text = variables.nombre;
            if (variables.numeroorden != " - ")
                button7.Visible = true;
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
    }
}
