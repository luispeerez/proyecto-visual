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
    public partial class cobro : Form
    {
        public int contarOrdenesCerradas()
        {
            int resultadoQuery;
            conexion search = new conexion();
            search.crearConexion();
            string search3 = "SELECT COUNT(*) FROM orden WHERE estatus='CERRADA'";
            MySqlCommand buscarproductos = new MySqlCommand(search3, search.getConexion());
            resultadoQuery = Convert.ToInt32(buscarproductos.ExecuteScalar());
            search.cerrarConexion();

            return resultadoQuery;
        }

        public void llenarCombo()
        {
            int numUsuarios = contarOrdenesCerradas();
            conexion search = new conexion();
            search.crearConexion();
            string search3 = "SELECT *FROM  orden WHERE estatus = 'CERRADA'";
            MySqlCommand buscarproductos = new MySqlCommand(search3, search.getConexion());
            MySqlDataAdapter cmc = new MySqlDataAdapter(buscarproductos);
            DataSet tht = new DataSet();
            buscarproductos.Connection = search.getConexion();
            cmc.Fill(tht, "orden");
            //Llenando el combobox con los ids de las ordenes
            for (int i = 0; i < numUsuarios; i++)
                comboBox1.Items.Add(Convert.ToInt32(tht.Tables["orden"].Rows[i][0]));

            search.cerrarConexion();
        }

        public void llenarGridOrdenes(int idorden)
        {
            //Agregando las ordenes cerradas en el datagrid
            conexion search = new conexion();
            search.crearConexion();
            string search3 = "SELECT *FROM usuario";
            MySqlCommand buscarproductos = new MySqlCommand(search3, search.getConexion());
            MySqlDataAdapter cmc = new MySqlDataAdapter(buscarproductos);
            DataSet tht = new DataSet();
            cmc.Fill(tht, "usuario");
            dataGridView1.DataSource = tht.Tables["usuario"].DefaultView;
            search.cerrarConexion();
        }

        //Evento en en el que se actualizan los datos(definido en el timer)
        public void timer_Tick(object sender, EventArgs e)
        {
            llenarCombo();
        }

        public cobro()
        {
            InitializeComponent();

            //Creando un timer para manejar los intervalos de actualizacion
            var timer = new Timer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = 30000; //30 segundos
            timer.Start();

            llenarCombo();
        }



        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
