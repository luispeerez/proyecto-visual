using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MySql.Data.MySqlClient;


namespace Prueba___Proyecto
{
    public partial class Recepcion : Form
    {
        
        //Declarando las frases para los placeholders en los textbox, elementos ordenados segun el textbox(ascendente)
        string[] placeholders = { "       Ingresa el nombre(s) del cliente.", "       Ingresa los apellidos del cliente.", "    Ingresa el número de acompañantes.", "           Ingresa el número de mesa." };
        int numMesas;
        //Variable para identificar el proceso actual
        /*
         * 1.-Nuevo cliente
         * 2.-Reservacion
         * 3.-Cobro
        */
        int accionactual;


        public int contarRegistros(string tabla)
        {
            int resultadoQuery;
            conexion search = new conexion();
            search.crearConexion();
            string search3 = "SELECT COUNT(*) FROM "+tabla;
            MySqlCommand buscarproductos = new MySqlCommand(search3, search.getConexion());
            resultadoQuery = Convert.ToInt32(buscarproductos.ExecuteScalar());

            return resultadoQuery;
        }

        public void insertarCliente()
        {
            conexion ins_pro = new conexion();
            ins_pro.crearConexion();
            string inserta = "INSERT INTO cliente (nombre,apellido) Values ('" + textBox1.Text + "','" + textBox2.Text  + "')";
            MySqlCommand pro = new MySqlCommand(inserta);
            pro.Connection = ins_pro.getConexion();
            pro.ExecuteNonQuery();
        }

        public string disponiblidad(int indice)
        {
            string resultado;
            conexion search = new conexion();
            search.crearConexion();
            string search3 = "SELECT *FROM mesa";
            MySqlCommand buscarproductos = new MySqlCommand(search3, search.getConexion());
            MySqlDataAdapter cmc = new MySqlDataAdapter(buscarproductos);
            DataSet tht = new DataSet();
            buscarproductos.Connection = search.getConexion();
            cmc.Fill(tht, "mesa");
            resultado = tht.Tables["mesa"].Rows[indice][4].ToString();
            return resultado.ToString();
        }

        public void vaciarTextboxs()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
        }

        public bool verificarCamposLlenos()
        {
            bool llenos;
            if (textBox1.Text != "" && textBox2.Text != ""/* && textBox3.Text != "" && textBox4.Text != ""*/)
                llenos = true;
            else
                llenos = false;
           return llenos;
        }
        public Recepcion()
        {
            InitializeComponent();
            
            this.ShowInTaskbar = false;
            
            //Asignando accion por default : 1(nuevo cliente)
            accionactual = 1;
            
            //Nombres de las imagenes
            string mesadisponible = "mesasimple";
            string mesareservada = "mesareservada";
            string mesaocupada = "mesaocupada";

            //Asignando disponible por default
            string fondo = mesadisponible;

            //Usando una imagen como fondo para las mesas
            Image mesaBack;
            string absolute;
            numMesas = contarRegistros("mesa");
            variables.CantidadMesas = numMesas;
            
            //Creando un array para desplegar las mesas en el restaurant
            PictureBox[] mesas = new PictureBox[numMesas];

            //Ancho del contenedor de mesas:
            int anchoCont = splitContainer1.Panel1.Width;
            //Variables para calcular el eje x y
            int posX, posY, contIndependienteX, contIndependienteY;
            contIndependienteX = 0;
            contIndependienteY = 0;
            
            //Iniciando la primera fila en el punto 56 del eje Y
            posY = 56;
            for (int i = 0; i < numMesas; i++)
            {
                //Cambiando el background de la mesa dependiendo su disponibilidad
                if (disponiblidad(i) == "Si")
                    fondo = mesadisponible;
                else if (disponiblidad(i) == "Ocupada")
                    fondo = mesaocupada;
                else if (disponiblidad(i) == "Reservada")
                    fondo = mesareservada;


                absolute = Path.GetFullPath(@""+fondo+".png");
                mesaBack = Image.FromFile(absolute);

                //Aumentando a uno la variable filas cada que se impriman 5 mesas
                if (i % 5 == 0 && i!=0)
                {
                    //Aumentando en 1 el contador independiente Y
                    contIndependienteY++;

                    //Vaciando el contador independiente de X
                    contIndependienteX = 0;
                    posY = 56 + (contIndependienteY * 90);
                }

                //Asignando su coordenada en X
                posX = 32 + (contIndependienteX * 100);
                
                //Creando la mesa
                mesas[i] = new PictureBox();
                //Añadiendole propiedades a cada mesa
                mesas[i].Size = new Size(80, 80);
                mesas[i].Location = new Point(posX, posY);
                mesas[i].Image = mesaBack;
                mesas[i].SizeMode = PictureBoxSizeMode.StretchImage;
                mesas[i].Visible = true;
                mesas[i].Cursor = Cursors.Hand;
                splitContainer1.Panel2.Controls.Add(mesas[i]);
                

                contIndependienteX++;
            }
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

        private void button2_MouseHover(object sender, EventArgs e)
        {
            button13.Visible = true;
            button2.Visible = false;
        }

        private void button13_MouseLeave(object sender, EventArgs e)
        {
            button2.Visible = true;
            button13.Visible = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            textBox1.ForeColor = Color.DimGray;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.ForeColor = Color.DimGray;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            textBox3.ForeColor = Color.DimGray;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            textBox4.ForeColor = Color.DimGray;
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBox1.Text == placeholders[0])
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.DimGray;
            }

        }

        private void textBox2_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBox2.Text == placeholders[1])
            {
                textBox2.Text = "";
                textBox2.ForeColor = Color.DimGray;
            }
        }

        private void textBox3_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBox3.Text == placeholders[2])
            {
                textBox3.Text = "";
                textBox3.ForeColor = Color.DimGray;
            }
        }

        private void textBox4_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBox4.Text == placeholders[3])
            {
                textBox4.Text = "";
                textBox4.ForeColor = Color.DimGray;
            }
        }
        //Boton reservar
        private void button7_Click(object sender, EventArgs e)
        {
            //Asigando como accion actual 2(reservar)
            accionactual = 2;
            //Vaciando posible informacion ingresada en otros campos 
            vaciarTextboxs();

            //Cambia imagenes al dar click
            button2.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.Reservar));
            button13.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.Reservar_Seleccionado));
            pictureBox4.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.Datos_Reservacion));
        }

        //Boton nuevo cliente
        private void button4_Click(object sender, EventArgs e)
        {
            //Asigando como accion actual 1(nuevo cliente)
            accionactual = 1;
            //Vaciando posible informacion ingresada en otros campos 
            vaciarTextboxs();

            //Cambia Imagenes al dar click
            button2.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.Registrar));
            button13.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.Registrar_Seleccionado));
            pictureBox4.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.Datos_Cliente));
        }

        private void button3_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void button6_Click(object sender, EventArgs e)
        {
        }


        //Boton de cobrar
        private void button3_Click_1(object sender, EventArgs e)
        {
            //Asigando como accion actual 3(cobrar)
            accionactual = 3;
            //Vaciando posible informacion ingresada en otros campos 
            vaciarTextboxs();
        }
        private void button13_Click(object sender, EventArgs e)
        {

        }

        private void button13_Click_1(object sender, EventArgs e)
        {
            if (accionactual == 1)
            {
                if (verificarCamposLlenos() == true)
                {
                    insertarCliente();
                    MessageBox.Show("Insertado!");

                }
            }
            else
                MessageBox.Show("No es la accion");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (accionactual == 1)
            {
                if (verificarCamposLlenos() == true)
                {
                    insertarCliente();
                    MessageBox.Show("Insertado!");

                }
            }
            else
                MessageBox.Show("No es la accion");
        }
    }
}
