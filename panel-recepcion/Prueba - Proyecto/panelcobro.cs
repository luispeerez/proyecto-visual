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

using System.IO;
//Libreria del pdf

using iTextSharp.text;
using iTextSharp.text.pdf;

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

        public string obtenerNombreMesero(int idorden)
        {
            string resultadoQuery;
            conexion search = new conexion();
            search.crearConexion();
            string search3 = "SELECT CONCAT(nombre,' ',apellidos) AS atendio FROM mesero WHERE idmesero IN( SELECT idmesero FROM orden WHERE idorden=" + idorden + ")";
            MySqlCommand buscarproductos = new MySqlCommand(search3, search.getConexion());
            resultadoQuery = (buscarproductos.ExecuteScalar()).ToString();
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

        public void generarFactura()
        {
            //Creando una fuente customizada
            iTextSharp.text.Font fontH1 = new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, 10, iTextSharp.text.Font.NORMAL);
            iTextSharp.text.Font fontfecha = new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, 8, iTextSharp.text.Font.NORMAL);
            iTextSharp.text.Font encabezados = new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.GRAY);

            Document doc = new Document(iTextSharp.text.PageSize.A6, 1, 1, 42, 35);
            PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream("Factura.pdf", FileMode.Create));

            doc.Open();//Abre el documento
            //Escribe contenido en el

            iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance("Logo.png");
            imagen.ScalePercent(20f);
            imagen.Alignment = iTextSharp.text.Image.ALIGN_CENTER;
            //imagen.SetAbsolutePosition(doc.PageSize.Width - 36f - 72f, doc.PageSize.Height - 36f - 216.6f);
            /*imagen.ScaleToFit(50f, 100f);
            imagen.Border = iTextSharp.text.Rectangle.BOX;
            imagen.BorderColor = iTextSharp.text.BaseColor.YELLOW;*/
            doc.Add(imagen);

            Paragraph parrafo = new Paragraph("Restaurant Manager System", fontH1);
            parrafo.Alignment = Element.ALIGN_CENTER;
            //parrafo.Add(imagen);
            doc.Add(parrafo);
            Paragraph parrafo2 = new Paragraph("Gracias por su visita",fontH1);
            parrafo2.Alignment = Element.ALIGN_CENTER;
            doc.Add(parrafo2);

            string nombreMesero = obtenerNombreMesero(Convert.ToInt32(comboBox1.SelectedItem));
            Paragraph atendio = new Paragraph("Le atendio "+nombreMesero, fontH1);
            atendio.Alignment = Element.ALIGN_CENTER;
            doc.Add(atendio);
            
            Paragraph parrafo3 = new Paragraph("Cancun, Q.Roo Mexico " + DateTime.Now.ToString() + " \n \n ", fontfecha);
            parrafo3.Alignment = Element.ALIGN_CENTER;
            doc.Add(parrafo3);


            //PdfPTable tablapedidos = new PdfPTable(dataGridView1.Columns.Count);
            PdfPTable tablapedidos = new PdfPTable(4);
            tablapedidos.TotalWidth = 100f;
            tablapedidos.DefaultCell.BorderColor = iTextSharp.text.BaseColor.WHITE;

            //Añadiendo los encabezados
            for (int i = 1; i < dataGridView1.Columns.Count; i++)
            {
                tablapedidos.AddCell(new Phrase(dataGridView1.Columns[i].HeaderText, encabezados));
            }

            //Poniendo como encabezado la primera fila
            tablapedidos.HeaderRows = 1;


            //Añadiendo las filas del datagrid
            for (int j = 0; j < dataGridView1.Rows.Count; j++)
            {
                //for (int k = 0; k < dataGridView1.Columns.Count; k++)
                for (int k = 1; k < dataGridView1.Columns.Count; k++)
                {
                    //Verificando que la celda este llena
                    if (dataGridView1[k, j].Value != null)
                    {
                        if(k==3 || k==4)
                            tablapedidos.AddCell(new Phrase("$"+dataGridView1[k, j].Value.ToString(), fontfecha));
                        else
                            tablapedidos.AddCell(new Phrase(dataGridView1[k, j].Value.ToString(), fontfecha));
                    }
                }
            }

            doc.Add(tablapedidos);

                /*RomanList lista2 = new RomanList(true,20);
                lista2.IndentationLeft = 30f;

                lista2.Add("dos");
                lista2.Add("tres");
                //doc.Add(lista2);

                List lista = new List(List.UNORDERED);
                //lista.SetListSymbol("/u2022");
                lista.IndentationLeft = 30f;
                lista.Add(new ListItem("Uno"));
                lista.Add("dos");
                lista.Add("tres");
                lista.Add(lista2);
                lista.Add(new ListItem("Uno"));
                doc.Add(lista);

                //indicando el numero de columnas
                PdfPTable tabla = new PdfPTable(3);

                PdfPCell celda = new PdfPCell(new Phrase("Header spanning 3 columns", new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, 8f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.RED)));
                //solo se puede dar BG en RGB
                celda.BackgroundColor = new iTextSharp.text.BaseColor(0, 150, 0);
                celda.Colspan = 3;
                celda.HorizontalAlignment = 1;//0=Left , 1=Centre , 2=Right
                tabla.AddCell(celda);

                tabla.AddCell("Col 1 Row 1");
                tabla.AddCell("Col 2 Row 1");
                tabla.AddCell("Col 3 Row 1");
                tabla.AddCell("Col 1 Row 2");
                tabla.AddCell("Col 2 Row 2");
                tabla.AddCell("Col 3 Row 2");
                doc.Add(tabla);
                 * 
                */

                Paragraph datos = new Paragraph("\nSubtotal $"+textBox1.Text+"\nI.V.A %"+textBox2.Text+"\nTotal $"+textBox3.Text, fontfecha);
                datos.Alignment = Element.ALIGN_RIGHT;
                datos.IndentationRight = 45f;
                doc.Add(datos);

                Paragraph parrafo4 = new Paragraph("\n \n \n \nTodos los derechos reservados Restaurant Manager System", fontfecha);
                parrafo4.Alignment = Element.ALIGN_CENTER;
                doc.Add(parrafo4);

                iTextSharp.text.Image cc = iTextSharp.text.Image.GetInstance("cc.png");
                cc.ScalePercent(3f);
                cc.Alignment = iTextSharp.text.Image.ALIGN_CENTER;
                doc.Add(cc);

                //Y se cierra la escritura
                doc.Close();

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
                generarFactura();
                this.Hide();
                pago terminarpago = new pago();
                terminarpago.ShowDialog();
            }
        }

    }
}
