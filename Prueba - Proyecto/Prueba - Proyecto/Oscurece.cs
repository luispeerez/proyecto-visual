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
    public partial class Oscurece : Form
    {
        public Oscurece()
        {
            InitializeComponent();
        }

        private void Oscurece_Load(object sender, EventArgs e)
        {
            this.ShowInTaskbar = false;
            Form1 Inicio = new Form1();
            Inicio.ShowDialog();
            this.Close();
        }
    }
}
