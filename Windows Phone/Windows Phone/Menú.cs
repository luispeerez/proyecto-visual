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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Menú_Alimento crear = new Menú_Alimento();
            crear.ShowDialog();
        }
    }
}
