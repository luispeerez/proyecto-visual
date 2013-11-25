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
        string idmesa, insertado;
        
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
            string resultadoestatus = disponiblidad(Convert.ToInt32(idmesa) - 1);

            if (resultadoestatus == "Disponible")
            {
                conexion ins_pro = new conexion();
                ins_pro.crearConexion();
                string inserta = "INSERT INTO cliente (nombre, apellidos) Values ('" + textBox1.Text + "', '" + textBox2.Text + "')";
                MySqlCommand pro = new MySqlCommand(inserta);
                pro.Connection = ins_pro.getConexion();
                pro.ExecuteNonQuery();

                conexion search = new conexion();
                search.crearConexion();
                string search4 = "SELECT idcliente FROM cliente ORDER BY idcliente DESC LIMIT 1;";
                MySqlCommand buscarproductos = new MySqlCommand(search4, search.getConexion());
                string resultadoQuery = (buscarproductos.ExecuteScalar()).ToString();

                inserta = "UPDATE mesa SET idcliente='" + resultadoQuery + "', estatus='Ocupada' WHERE idmesa='" + idmesa + "';";
                MySqlCommand pra = new MySqlCommand(inserta);
                pra.Connection = ins_pro.getConexion();
                pra.ExecuteNonQuery();
                insertado = "OK";
                ActualizarMesas();
            }
            else
                MessageBox.Show("NO DISPONIBLE.");
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
            resultado = tht.Tables["mesa"].Rows[indice][2].ToString();
            return resultado.ToString();
        }

        public string[] informacionMesa(int indice)
        {
            string[] resultados = new string[4];
            conexion search = new conexion();
            search.crearConexion();
            string search3 = "SELECT *FROM mesa";
            MySqlCommand buscarproductos = new MySqlCommand(search3, search.getConexion());
            MySqlDataAdapter cmc = new MySqlDataAdapter(buscarproductos);
            DataSet tht = new DataSet();
            buscarproductos.Connection = search.getConexion();
            cmc.Fill(tht, "mesa");
            resultados[0] = tht.Tables["mesa"].Rows[indice][0].ToString();
            resultados[1] = tht.Tables["mesa"].Rows[indice][1].ToString();
            resultados[2] = tht.Tables["mesa"].Rows[indice][2].ToString();
            resultados[3] = tht.Tables["mesa"].Rows[indice][3].ToString();
            return resultados;
        }
        public void vaciarTextboxs()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
        }

        public void LlenarTextboxs()
        {
            textBox1.Text = placeholders[0];
            textBox2.Text = placeholders[1];
            textBox3.Text = placeholders[2];
            textBox4.Text = placeholders[3];
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
            
            //Ocultando por default el groupbox que muestra la informacion de la mesa
            groupBox1.Visible = false;


            //Asignando accion por default : 1(nuevo cliente)
            accionactual = 1;

            ActualizarMesas();
            
        }

        public void ActualizarMesas()
        {
            //Nombres de las imagenes
            string mesadisponible = "mesasimple";
            string mesareservada = "mesareservada";
            string mesaocupada = "mesaocupada";

            //Asignando disponible por default
            string fondo = mesadisponible;

            //Usando una imagen como fondo para las mesas
            Image mesaBack;
            string absolute;

            //Llenando la variable numMesas con la funcion de contarregistros de la tabla
            numMesas = contarRegistros("mesa");

            variables.CantidadMesas = numMesas;

            //Creando un array para desplegar las mesas en el restaurant
            PictureBox[] mesas = { pictureBox2, pictureBox3, pictureBox5, pictureBox6, pictureBox7, pictureBox8, pictureBox9, pictureBox10, pictureBox11, pictureBox12, pictureBox13, pictureBox14, pictureBox15, pictureBox16, pictureBox17, pictureBox18, pictureBox19, pictureBox20, pictureBox21, pictureBox22, pictureBox23, pictureBox24, pictureBox24, pictureBox25, pictureBox26, pictureBox27 };

            //Ocultando los picturebox de las mesas no inicializadas
            for (int contador = 0; contador < 25; contador++)
            {
                if (contador < numMesas)
                    mesas[contador].Visible = true;
                else
                    mesas[contador].Visible = false;
            }

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
                if (disponiblidad(i) == "Disponible")
                    fondo = mesadisponible;
                else if (disponiblidad(i) == "Ocupada" || disponiblidad(i) == "No disponible")
                    fondo = mesaocupada;
                else if (disponiblidad(i) == "Reservada")
                    fondo = mesareservada;


                absolute = Path.GetFullPath(@"" + fondo + ".png");
                mesaBack = Image.FromFile(absolute);

                //Aumentando a uno la variable filas cada que se impriman 5 mesas
                if (i % 5 == 0 && i != 0)
                {
                    //Aumentando en 1 el contador independiente Y
                    contIndependienteY++;

                    //Vaciando el contador independiente de X
                    contIndependienteX = 0;
                    posY = 56 + (contIndependienteY * 90);
                }

                //Asignando su coordenada en X
                posX = 32 + (contIndependienteX * 100);

                /*
                //Creando la mesa
                mesas[i] = new PictureBox();*/
                //mesas[i].DoubleClick += new EventHandler(DobleClick);


                //Añadiendole propiedades a cada mesa
                mesas[i].Size = new Size(80, 80);
                mesas[i].Location = new Point(posX, posY);
                mesas[i].Image = mesaBack;
                mesas[i].SizeMode = PictureBoxSizeMode.StretchImage;
                mesas[i].Visible = true;
                mesas[i].Cursor = Cursors.Hand;
                //splitContainer1.Panel2.Controls.Add(mesas[i]);


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
            LlenarTextboxs();
            textBox1.ForeColor = Color.Silver;
            textBox2.ForeColor = Color.Silver;
            textBox3.ForeColor = Color.Silver;
            textBox4.ForeColor = Color.Silver;

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
            LlenarTextboxs();
            textBox1.ForeColor = Color.Silver;
            textBox2.ForeColor = Color.Silver;
            textBox3.ForeColor = Color.Silver;
            textBox4.ForeColor = Color.Silver;

            //Cambia Imagenes al dar click
            button2.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.Registrar));
            button13.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.Registrar_Seleccionado));
            pictureBox4.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.Datos_Cliente));
        }

        //Boton de cobrar
        private void button3_Click_1(object sender, EventArgs e)
        {
            //Asigando como accion actual 3(cobrar)
            accionactual = 3;
            //Vaciando posible informacion ingresada en otros campos 
            vaciarTextboxs();
            LlenarTextboxs();
            textBox1.ForeColor = Color.Silver;
            textBox2.ForeColor = Color.Silver;
            textBox3.ForeColor = Color.Silver;
            textBox4.ForeColor = Color.Silver;

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
            insertado = "NO";
            if (accionactual == 1)
            {
                if (verificarCamposLlenos() == true)
                {
                    insertarCliente();
                    if (insertado == "OK")
                    {
                        MessageBox.Show("Insertado!");
                        insertado = "NO";
                    }
                }
            }
            else
                MessageBox.Show("No es la accion");
        }

        private void button13_Click_2(object sender, EventArgs e)
        {
            insertado = "NO";
            if (accionactual == 1)
            {
                if (verificarCamposLlenos() == true)
                {
                    insertarCliente();
                    if (insertado == "OK")
                    {
                        MessageBox.Show("Insertado!");
                        insertado = "NO";
                    }
                }
            }
            else
                MessageBox.Show("No es la accion");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Asigando como accion actual 1(nuevo cliente)
            accionactual = 1;
            //Vaciando posible informacion ingresada en otros campos 
            vaciarTextboxs();
            LlenarTextboxs();
            textBox1.ForeColor = Color.Silver;
            textBox2.ForeColor = Color.Silver;
            textBox3.ForeColor = Color.Silver;
            textBox4.ForeColor = Color.Silver;

            //Cambia Imagenes al dar click
            button2.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.Registrar));
            button13.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.Registrar_Seleccionado));
            pictureBox4.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.Datos_Cliente));
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //Asigando como accion actual 2(reservar)
            accionactual = 2;
            //Vaciando posible informacion ingresada en otros campos 
            vaciarTextboxs();
            LlenarTextboxs();
            textBox1.ForeColor = Color.Silver;
            textBox2.ForeColor = Color.Silver;
            textBox3.ForeColor = Color.Silver;
            textBox4.ForeColor = Color.Silver;

            //Cambia imagenes al dar click
            button2.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.Reservar));
            button13.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.Reservar_Seleccionado));
            pictureBox4.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.Datos_Reservacion));
        }




        //Asigando eventos de click a las mesas
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            idmesa = "1";
            textBox4.Text = idmesa;

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            idmesa = "2";
            textBox4.Text = idmesa;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            groupBox1.Visible = false;
            idmesa = "3";
            textBox4.Text = idmesa;
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            groupBox1.Visible = false;
            idmesa = "4";
            textBox4.Text = idmesa;
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            groupBox1.Visible = false;
            idmesa = "4";
            textBox4.Text = idmesa;
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox15_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox16_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox17_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox18_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox19_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox20_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox21_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox22_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox23_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox24_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox25_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox26_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox27_Click(object sender, EventArgs e)
        {

        }

        //Asignando click para cerrar la informacion de la mesa
        private void Recepcion_MouseClick(object sender, MouseEventArgs e)
        {
            groupBox1.Visible = false;
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {
            groupBox1.Visible = false;

        }
        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {
            groupBox1.Visible = false;

        }

        private void splitContainer1_Panel2_MouseClick(object sender, MouseEventArgs e)
        {
            groupBox1.Visible = false;
        }

        private void splitContainer1_Panel1_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;

        }

        private void button12_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;

        }

        private void pictureBox2_MouseHover(object sender, EventArgs e)
        {
            pictureBox1.Visible = true;
            groupBox1.Visible = true;
            string[] atributosMesa = informacionMesa(0);
            label3.Text = atributosMesa[1];
            label5.Text = atributosMesa[2];
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            groupBox1.Visible = false;
        }

        private void pictureBox3_MouseHover(object sender, EventArgs e)
        {
            pictureBox1.Visible = true;
            groupBox1.Visible = true;
            string[] atributosMesa = informacionMesa(1);
            label3.Text = atributosMesa[1];
            label5.Text = atributosMesa[2];
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            groupBox1.Visible = false;
        }

        private void pictureBox5_MouseHover(object sender, EventArgs e)
        {
            pictureBox1.Visible = true;
            groupBox1.Visible = true;
            string[] atributosMesa = informacionMesa(2);
            label3.Text = atributosMesa[1];
            label5.Text = atributosMesa[2];
        }

        private void pictureBox5_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            groupBox1.Visible = false;
        }

        private void pictureBox6_MouseHover(object sender, EventArgs e)
        {
            pictureBox1.Visible = true;
            groupBox1.Visible = true;
            string[] atributosMesa = informacionMesa(3);
            label3.Text = atributosMesa[1];
            label5.Text = atributosMesa[2];
        }

        private void pictureBox6_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            groupBox1.Visible = false;
        }

        private void pictureBox7_MouseHover(object sender, EventArgs e)
        {
            pictureBox1.Visible = true;
            groupBox1.Visible = true;
            string[] atributosMesa = informacionMesa(4);
            label3.Text = atributosMesa[1];
            label5.Text = atributosMesa[2];
        }

        private void pictureBox7_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            groupBox1.Visible = false;
        }

        private void pictureBox8_MouseHover(object sender, EventArgs e)
        {
            pictureBox1.Visible = true;
            groupBox1.Visible = true;
            string[] atributosMesa = informacionMesa(5);
            label3.Text = atributosMesa[1];
            label5.Text = atributosMesa[2];
        }

        private void pictureBox8_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            groupBox1.Visible = false;
        }

        private void pictureBox9_MouseHover(object sender, EventArgs e)
        {
            pictureBox1.Visible = true;
            groupBox1.Visible = true;
            string[] atributosMesa = informacionMesa(6);
            label3.Text = atributosMesa[1];
            label5.Text = atributosMesa[2];
        }

        private void pictureBox9_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            groupBox1.Visible = false;     
        }

        private void pictureBox10_MouseHover(object sender, EventArgs e)
        {
            pictureBox1.Visible = true;
            groupBox1.Visible = true;
            string[] atributosMesa = informacionMesa(7);
            label3.Text = atributosMesa[1];
            label5.Text = atributosMesa[2];

        }

        private void pictureBox10_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            groupBox1.Visible = false;   
        }

        private void pictureBox11_MouseHover(object sender, EventArgs e)
        {
            pictureBox1.Visible = true;
            groupBox1.Visible = true;
            string[] atributosMesa = informacionMesa(8);
            label3.Text = atributosMesa[1];
            label5.Text = atributosMesa[2];
        }

        private void pictureBox11_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            groupBox1.Visible = false;   
        }

        private void pictureBox12_MouseHover(object sender, EventArgs e)
        {
            pictureBox1.Visible = true;
            groupBox1.Visible = true;
            string[] atributosMesa = informacionMesa(9);
            label3.Text = atributosMesa[1];
            label5.Text = atributosMesa[2];
        }

        private void pictureBox12_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            groupBox1.Visible = false;
        }

        private void pictureBox13_MouseHover(object sender, EventArgs e)
        {
            pictureBox1.Visible = true;
            groupBox1.Visible = true;
            string[] atributosMesa = informacionMesa(10);
            label3.Text = atributosMesa[1];
            label5.Text = atributosMesa[2];
        }

        private void pictureBox13_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            groupBox1.Visible = false;
        }

        private void pictureBox14_MouseHover(object sender, EventArgs e)
        {
            pictureBox1.Visible = true;
            groupBox1.Visible = true;
            string[] atributosMesa = informacionMesa(11);
            label3.Text = atributosMesa[1];
            label5.Text = atributosMesa[2];
        }

        private void pictureBox14_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            groupBox1.Visible = false;
        }

        private void pictureBox15_MouseHover(object sender, EventArgs e)
        {
            pictureBox1.Visible = true;
            groupBox1.Visible = true;
            string[] atributosMesa = informacionMesa(12);
            label3.Text = atributosMesa[1];
            label5.Text = atributosMesa[2];
        }

        private void pictureBox15_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            groupBox1.Visible = false;
        }

        private void pictureBox16_MouseHover(object sender, EventArgs e)
        {
            pictureBox1.Visible = true;
            groupBox1.Visible = true;
            string[] atributosMesa = informacionMesa(13);
            label3.Text = atributosMesa[1];
            label5.Text = atributosMesa[2];
        }

        private void pictureBox16_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            groupBox1.Visible = false;
        }

        private void pictureBox17_MouseHover(object sender, EventArgs e)
        {
            pictureBox1.Visible = true;
            groupBox1.Visible = true;
            string[] atributosMesa = informacionMesa(14);
            label3.Text = atributosMesa[1];
            label5.Text = atributosMesa[2];
        }

        private void pictureBox17_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            groupBox1.Visible = false;
        }

        private void pictureBox18_MouseHover(object sender, EventArgs e)
        {
            pictureBox1.Visible = true;
            groupBox1.Visible = true;
            string[] atributosMesa = informacionMesa(15);
            label3.Text = atributosMesa[1];
            label5.Text = atributosMesa[2];
        }

        private void pictureBox18_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            groupBox1.Visible = false;
        }

        private void pictureBox19_MouseHover(object sender, EventArgs e)
        {
            pictureBox1.Visible = true;
            groupBox1.Visible = true;
            string[] atributosMesa = informacionMesa(16);
            label3.Text = atributosMesa[1];
            label5.Text = atributosMesa[2];
        }

        private void pictureBox19_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            groupBox1.Visible = false;
        }

        private void pictureBox20_MouseHover(object sender, EventArgs e)
        {
            pictureBox1.Visible = true;
            groupBox1.Visible = true;
            string[] atributosMesa = informacionMesa(17);
            label3.Text = atributosMesa[1];
            label5.Text = atributosMesa[2];
        }

        private void pictureBox20_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            groupBox1.Visible = false;
        }

        private void pictureBox21_MouseHover(object sender, EventArgs e)
        {
            pictureBox1.Visible = true;
            groupBox1.Visible = true;
            string[] atributosMesa = informacionMesa(18);
            label3.Text = atributosMesa[1];
            label5.Text = atributosMesa[2];
        }

        private void pictureBox21_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            groupBox1.Visible = false;
        }

        private void pictureBox22_MouseHover(object sender, EventArgs e)
        {
            pictureBox1.Visible = true;
            groupBox1.Visible = true;
            string[] atributosMesa = informacionMesa(19);
            label3.Text = atributosMesa[1];
            label5.Text = atributosMesa[2];
        }

        private void pictureBox22_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            groupBox1.Visible = false;
        }

        private void pictureBox23_MouseHover(object sender, EventArgs e)
        {
            pictureBox1.Visible = true;
            groupBox1.Visible = true;
            string[] atributosMesa = informacionMesa(20);
            label3.Text = atributosMesa[1];
            label5.Text = atributosMesa[2];
        }

        private void pictureBox23_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            groupBox1.Visible = false;
        }

        private void pictureBox24_MouseHover(object sender, EventArgs e)
        {
            pictureBox1.Visible = true;
            groupBox1.Visible = true;
            string[] atributosMesa = informacionMesa(21);
            label3.Text = atributosMesa[1];
            label5.Text = atributosMesa[2];
        }

        private void pictureBox24_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            groupBox1.Visible = false;
        }

        private void pictureBox25_MouseHover(object sender, EventArgs e)
        {
            pictureBox1.Visible = true;
            groupBox1.Visible = true;
            string[] atributosMesa = informacionMesa(22);
            label3.Text = atributosMesa[1];
            label5.Text = atributosMesa[2];
        }

        private void pictureBox25_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            groupBox1.Visible = false;
        }

        private void pictureBox26_MouseHover(object sender, EventArgs e)
        {
            pictureBox1.Visible = true;
            groupBox1.Visible = true;
            string[] atributosMesa = informacionMesa(23);
            label3.Text = atributosMesa[1];
            label5.Text = atributosMesa[2];
        }

        private void pictureBox26_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            groupBox1.Visible = false;
        }

        private void pictureBox27_MouseHover(object sender, EventArgs e)
        {
            pictureBox1.Visible = true;
            groupBox1.Visible = true;
            string[] atributosMesa = informacionMesa(24);
            label3.Text = atributosMesa[1];
            label5.Text = atributosMesa[2];
        }

        private void pictureBox27_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            groupBox1.Visible = false;
        }

        private void pictureBox5_Click_1(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            groupBox1.Visible = false;
            idmesa = "3";
            textBox4.Text = idmesa;
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            idmesa = textBox4.Text;
        }
        ///Terminando de asignar eventos de click
    }
}
