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
    public partial class panelcobro : Form
    {

        Timer timer1 = new Timer();
        double iva = 15;

        public void cerrarOrden(int idorden)
        {
            conexion ins_pro = new conexion();
            ins_pro.crearConexion();
            string inserta = "UPDATE orden SET estatus='CERRADA' WHERE idorden=" + idorden + "";
            MySqlCommand pra = new MySqlCommand(inserta);
            pra.Connection = ins_pro.getConexion();
            pra.ExecuteNonQuery();
        }

        public int contarOrdenesNopagadas()
        {
            int resultadoQuery;
            conexion search = new conexion();
            search.crearConexion();
            string search3 = "SELECT COUNT(*) FROM orden WHERE estatus <> 'PAGADA' ";
            MySqlCommand buscarproductos = new MySqlCommand(search3, search.getConexion());
            resultadoQuery = Convert.ToInt32(buscarproductos.ExecuteScalar());
            search.cerrarConexion();

            return resultadoQuery;
        }

        public void llenarCombo()
        {
            //Vaciando los elementos del combobox antes de agregar elementos
            comboBox1.Items.Clear();

            int numUsuarios = contarOrdenesNopagadas();
            conexion search = new conexion();
            search.crearConexion();
            string search3 = "SELECT idorden FROM orden WHERE estatus <> 'PAGADA' ";
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
            //Agregando toda la tabla de pedidos dependiendo de pedidos
            try
            {
                conexion search7 = new conexion();
                search7.crearConexion();
                string dia = "02";
                string mes = "12";
                string anio = "2013";
                string comando = "SELECT pedido.idorden, count(*), tablota.nombre, tablota.precio, count(*) * tablota.precio  FROM pedido  LEFT JOIN (SELECT *FROM alimento AS alimentodia WHERE idalimento IN (SELECT idalimento FROM pedido AS pedidos WHERE idorden IN (SELECT idorden FROM orden AS ordenes WHERE idorden = "+idorden+")))  AS tablota USING(idalimento) GROUP BY nombre;";
                MySqlCommand buscarpedidos = new MySqlCommand(comando, search7.getConexion());
                MySqlDataAdapter cmc4 = new MySqlDataAdapter(buscarpedidos);
                DataSet tht4 = new DataSet();
                cmc4.Fill(tht4, "orden");
                int rowCount = dataGridView1.Rows.Count;
                int n;
                double Suma = 0;
                for (n = 0; n < rowCount; n++)
                {
                    if (dataGridView1.Rows[0].IsNewRow == false)
                        dataGridView1.Rows.RemoveAt(0);
                }
                if (tht4.Tables["orden"].Rows[1][3].ToString() != "")
                {
                    dataGridView1.DataSource = tht4.Tables["orden"].DefaultView;
                    dataGridView1.Rows.RemoveAt(0);
                    dataGridView1.Columns[0].HeaderText = "Orden";
                    dataGridView1.Columns[0].Width = 100;
                    dataGridView1.Columns[1].HeaderText = "Cantidad";
                    dataGridView1.Columns[1].Width = 100;
                    dataGridView1.Columns[2].HeaderText = "Alimento";
                    dataGridView1.Columns[2].Width = 200;
                    dataGridView1.Columns[3].HeaderText = "Precio";
                    dataGridView1.Columns[4].HeaderText = "Importe";
                }
                search7.cerrarConexion();
                rowCount = dataGridView1.Rows.Count;
                for (n = 0; n < rowCount; n++)
                {
                    Suma += Convert.ToDouble(dataGridView1.Rows[n].Cells[4].Value);
                }
                textBox1.Enabled = false;
                textBox1.Text = Suma.ToString();
                textBox2.Text = iva.ToString();
                //Guardando el total de la orden
                variables.TotalOrden = (Suma * ((iva / 100) + 1));
                textBox3.Text = variables.TotalOrden.ToString();

                //Guardando el id de la orden
                variables.IdordenApagar = idorden;


            }
            catch (Exception)
            {
                int rowCount = dataGridView1.Rows.Count;
                for (int n = 0; n < rowCount; n++)
                {
                    if (dataGridView1.Rows[0].IsNewRow == false)
                        dataGridView1.Rows.RemoveAt(0);
                }
                textBox1.Text = "0";
            }
        }

        //Evento en en el que se actualizan los datos(definido en el timer)
        public void timer_Tick(object sender, EventArgs e)
        {
            llenarCombo();
            if(comboBox1.SelectedItem != null)
                llenarGridOrdenes(Convert.ToInt32(comboBox1.SelectedItem));
        }

        public panelcobro()
        {
            InitializeComponent();

            //Creando un timer para manejar los intervalos de actualizacion

            timer1.Tick += new EventHandler(timer_Tick);
            timer1.Interval = 30000; //30 segundos
            timer1.Start();

            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;

            llenarCombo();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                llenarGridOrdenes(Convert.ToInt32(comboBox1.SelectedItem));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            this.Hide();
            Recepcion panelrecepcion = new Recepcion();
            panelrecepcion.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                cerrarOrden(Convert.ToInt32(comboBox1.SelectedItem));
                llenarGridOrdenes(Convert.ToInt32(comboBox1.SelectedItem));
                MessageBox.Show("Mesa cerrada!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                this.Hide();
                pago terminarpago = new pago();
                terminarpago.ShowDialog();
            }
        }
    }
}
