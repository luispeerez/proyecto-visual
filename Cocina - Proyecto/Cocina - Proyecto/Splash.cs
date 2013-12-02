using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cocina___Proyecto
{
    public partial class Splash : Form
    {
        public Splash()
        {
            InitializeComponent();
            this.ShowInTaskbar = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            this.Close();
            ListaPedidos Acceso = new ListaPedidos();
            Acceso.ShowDialog();
        }

        private void Splash_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }
    }
}
