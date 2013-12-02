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
    public partial class Oscurece : Form
    {
        public Oscurece()
        {
            InitializeComponent();
            this.ShowInTaskbar = false;
        }

        private void Oscurece_Load(object sender, EventArgs e)
        {
            Splash Acceso = new Splash();
            Acceso.ShowDialog();
            this.Close();
        }
    }
}
