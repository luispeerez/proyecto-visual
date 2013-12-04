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
        
        //idmesa es la variable global para hacer el registro de la mesa
        string idmesa, insertado;

        //Arreglo para guardar los ids de las mesas mostradas en pantalla
        string[] identificadoresMesas;

        //Creando un array para desplegar las mesas en el restaurant
        PictureBox[] mesas;

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
            search.cerrarConexion();

            return resultadoQuery;
        }


        public int contarMesas()
        {
            int resultadoQuery;
            conexion search = new conexion();
            search.crearConexion();
            //Solo evaluando las mesas que sean diferentes a "No disponibles"
            string search3 = "SELECT COUNT(*) FROM mesa WHERE NOT estatus='No disponible'";
            MySqlCommand buscarproductos = new MySqlCommand(search3, search.getConexion());
            resultadoQuery = Convert.ToInt32(buscarproductos.ExecuteScalar());
            search.cerrarConexion();

            return resultadoQuery;
        }

        //Llenando un arreglo de los ids de las mesas que esten accesibles
        public void llenarIDSMesas()
        {
            int num = contarMesas();
            identificadoresMesas = new string[num];

            //Haciendo consultas para llenar los datos de las mesas accesibles
            conexion search = new conexion();
            search.crearConexion();
            string search3 = "SELECT idmesa FROM mesa  WHERE NOT estatus='No disponible'";
            MySqlCommand buscarproductos = new MySqlCommand(search3, search.getConexion());
            MySqlDataAdapter cmc = new MySqlDataAdapter(buscarproductos);
            DataSet tht = new DataSet();
            buscarproductos.Connection = search.getConexion();
            cmc.Fill(tht, "mesa");

            //Llenando los ids
            for (int i = 0; i < num; i++)
                identificadoresMesas[i] = tht.Tables["mesa"].Rows[i][0].ToString();

            search.cerrarConexion();

        }

        public void insertarCliente(string estado)
        {
            string resultadoestatus = disponiblidad(Convert.ToInt32(idmesa) - 1);

            //Validando que la mesa elegida este disponible
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
                search.cerrarConexion();

                //Asignandole id de la mesa en caso de que el usuario haya escrito el valor manualmente
                idmesa = textBox4.Text;

                inserta = "UPDATE mesa SET idcliente='" + resultadoQuery + "', estatus='"+ estado +"' WHERE idmesa='" + idmesa + "';";
                MySqlCommand pra = new MySqlCommand(inserta);
                pra.Connection = ins_pro.getConexion();
                pra.ExecuteNonQuery();
                insertado = "OK";
                
                ActualizarMesas();

                ins_pro.cerrarConexion();
                search.cerrarConexion();
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

            search.cerrarConexion();
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

            search.cerrarConexion();
            return resultados;
        }

        //Funcion general para consultar un campo en especifico(en dado caso que existan varios campos con la misma condicion regresara solo el primero
        //el parametro consulta es opcional
        public string consultaIndividual(string tabla, string columnaConsultada, string columnaCondicion , int idconsulta=0,string columnaCondicion2="",string condicion2="")
        {
            string resultado="";
            string comando;

            //Consulta
            conexion search = new conexion();
            search.crearConexion();

            //Condicionando que se haya pasado como parametro un id a consultar
            if (idconsulta > 0)
                comando = "SELECT " + columnaConsultada + " FROM " + tabla + "  WHERE " + columnaCondicion + "=" + idconsulta;

            //Condicionando si se desea hacer una consulta usando WHERE y AND
            else if(idconsulta > 0 && columnaCondicion2!="" && condicion2!="")
                comando = "SELECT " + columnaConsultada + " FROM " + tabla + "  WHERE " + columnaCondicion + "=" + idconsulta + "AND "+ columnaCondicion2 +"="+condicion2;
            //De lo contratrario solo se muestra el primer registro encontrado
            else
                comando = "SELECT " + columnaConsultada + " FROM " + tabla + "ORDER BY "+ columnaConsultada +"DESC LIMIT 1" ;


            MySqlCommand buscarproductos = new MySqlCommand(comando, search.getConexion());
            MySqlDataAdapter cmc = new MySqlDataAdapter(buscarproductos);
            DataSet tht = new DataSet();
            buscarproductos.Connection = search.getConexion();
            cmc.Fill(tht, tabla);

            if (buscarproductos.ExecuteScalar() != null && buscarproductos.ExecuteScalar() != "")
                resultado = (buscarproductos.ExecuteScalar()).ToString();
            //Devuelve una cadena vacia si no encuentra nada
            else
                resultado = "";
            search.cerrarConexion();

            return resultado;
        }


        public double calcularTotalOrden(int IDorden)
        {
            double totalOrden=0;

            conexion search = new conexion();
            search.crearConexion();
            //Solo evaluando las mesas que sean diferentes a "No disponibles"
            string comando = "SELECT SUM(precio) FROM alimento WHERE idalimento IN( SELECT idalimento FROM pedido WHERE idorden=" + IDorden + " AND estatus='Entregado')";
            MySqlCommand buscarproductos = new MySqlCommand(comando, search.getConexion());
            totalOrden = Convert.ToDouble(buscarproductos.ExecuteScalar());
            search.cerrarConexion();

            return totalOrden;
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
            if (textBox1.Text != placeholders[0] && textBox2.Text != placeholders[1] && textBox3.Text != placeholders[2] && textBox4.Text != placeholders[3] && textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "")
                llenos = true;
            else
                llenos = false;
           return llenos;
        }


        //Asigando eventos de click a las mesas
        public void clickMesa(object sender, EventArgs e)
        {
            PictureBox botonMesa = sender as PictureBox;

            //Obteniendo el id de la mesa apartir de su nombre(en el split se separan la palabra :"mesa1" en  "mes |a| 1")
            string[] palabras = botonMesa.Name.Split('a');
            string idbotonMesa = palabras[1];

            pictureBox1.Visible = false;
            groupBox1.Visible = false;

            //Asignando el numero de mesa a la orden
            idmesa = idbotonMesa;
            //Label que muestra el numero de mesa
            textBox4.Text = idmesa;

        }

        public void hoverMesaEntrante(object sender, EventArgs e)
        {
            PictureBox botonMesa = sender as PictureBox;

            pictureBox1.Visible = true;
            groupBox1.Visible = true;

            //Obteniendo el id de la mesa apartir de su nombre(en el split se separan la palabra :"mesa1" en  "mes |a| 1")
            string[] palabras = botonMesa.Name.Split('a');
            string idbotonMesa = palabras[1];

            //Restandole 1 al idbotonMesa debido a que la funcion informacionMesa() obtiene los valores apartir del indice 0
            string[] atributosMesa = informacionMesa(Convert.ToInt32(idbotonMesa) - 1);

            int idOrdenMesa;

            //Escribiendo el nombre del cliente que esta en la mesa
            //Comprueba si la mesa esta ocupada por algun cliente
            if (atributosMesa[3] != null && atributosMesa[3] != "")

                label1.Text = consultaIndividual("cliente", "nombre", "idcliente", Convert.ToInt32(atributosMesa[3])) +" "+ consultaIndividual("cliente", "apellidos", "idcliente", Convert.ToInt32(atributosMesa[3]));
            else
                label1.Text = "";

            //Llenando informacion de la orden actual en caso de que los clientes en la mesa hayan pedido algo
            //Comprueba si la mesa esta actualmente en una orden activa
            if (consultaIndividual("orden", "idorden", "idmesa", Convert.ToInt32(idbotonMesa)) == "")
                label2.Text = "No se encuentra ninguna orden";
            else
            {
                try
                {
                    label2.Text = consultaIndividual("orden", "idorden", "idmesa", Convert.ToInt32(idbotonMesa));
                    idOrdenMesa = Convert.ToInt32(consultaIndividual("orden", "idorden", "idmesa", Convert.ToInt32(idbotonMesa)));
                    label4.Text = "$ " + calcularTotalOrden(idOrdenMesa);
                }
                catch (Exception)
                {
                    label4.Text = "$ 0.00";
                }
            }

            //Escribiendo el numero de personas de la mesa
            label3.Text = atributosMesa[1];

            //Escribiendo su estatus
            label5.Text = atributosMesa[2];
        }

        public void hoverMesaSalida(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            groupBox1.Visible = false;

            label1.Text = "";
            label2.Text = "";
            label3.Text = "";
            label4.Text = "";
            label5.Text = "";
        }

        //Evento en en el que se actualizan las mesas(definido en el timer)
        public void timer_Tick(object sender, EventArgs e)
        {
            ActualizarMesas();
        }


        //Funcion para verificar si el usuario que se ingresa y habia realizado una reservacion
        public bool verificarReservacion(int idmesa)
        {
            bool previamenteReservado;

            conexion search = new conexion();
            search.crearConexion();
            //Obteniendo una consulta del cliente en caso de que la mesa tenga el estatus de Reservada
            string comando = "SELECT nombre,apellidos FROM cliente WHERE idcliente IN( SELECT idcliente FROM mesa WHERE idmesa=" + idmesa + " AND estatus='Reservada') ORDER BY apellidos DESC LIMIT 1";
            MySqlCommand buscarproductos = new MySqlCommand(comando, search.getConexion());
            MySqlDataAdapter cmc = new MySqlDataAdapter(buscarproductos);
            DataSet tht = new DataSet();
            buscarproductos.Connection = search.getConexion();
            cmc.Fill(tht, "cliente");

            //En caso de encontrar coincidencias entre los datos ingresados con la mesa reservada se devuelve true
            try
            {
                if (textBox1.Text == tht.Tables["cliente"].Rows[0][0].ToString() && textBox2.Text == tht.Tables["cliente"].Rows[0][1].ToString())
                    previamenteReservado = true;
                else
                    previamenteReservado = false;
            }
            catch (Exception)
            {
                previamenteReservado = false;
            }

            search.cerrarConexion();
            return previamenteReservado;
        }

        public void ocuparMesa(int idmesa)
        {
            conexion ins_pro = new conexion();
            ins_pro.crearConexion();
            string inserta = "UPDATE mesa SET estatus='Ocupada' WHERE idmesa=" + idmesa + "";
            MySqlCommand pro = new MySqlCommand(inserta);
            pro.Connection = ins_pro.getConexion();
            pro.ExecuteNonQuery();
            ins_pro.cerrarConexion();

            ActualizarMesas();
        }

        public Recepcion()
        {
            InitializeComponent();
            
            this.ShowInTaskbar = false;

            //Creando un timer para manejar los intervalos de actualizacion
            var timer = new Timer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = 30000; //30 segundos
            timer.Start();

            //Ocultando por default el groupbox que muestra la informacion de la mesa
            groupBox1.Visible = false;

            //Asignando accion por default : 1(nuevo cliente)
            accionactual = 1;

            //Llenando informacion de mesas
            ActualizarMesas();

            //Limpiando los labels de la informacion de la mesa al empezar el programa
            label1.Text = "";
            label2.Text = "";
            label3.Text = "";
            label4.Text = "";
            label5.Text = "";
        }


        //Funcion para vaciar el contenedor de la mesas, de modo que no se encimen cada vez que se acutalicen
        public void limpiarPanel()
        {
            splitContainer1.Panel2.Controls.Clear();
        }

        public void ActualizarMesas()
        {
            //Limpiando el contenedor
            limpiarPanel();

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
            numMesas = contarMesas();

            variables.CantidadMesas = numMesas;

            //Creando un array para desplegar las mesas en el restaurant
            mesas = new PictureBox[numMesas];

            //Funcion para llenar un arreglo con todos los ids de las mesas
            llenarIDSMesas();

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
                //Pasando de parametro el identificador de las mesas que estan en el arreglo identificadoresMesas[] y restandole 1 debido a que la funcion empieza desde el indice 0
                if (disponiblidad(Convert.ToInt32(identificadoresMesas[i]) -1) == "Disponible")
                    fondo = mesadisponible;
                else if (disponiblidad(Convert.ToInt32(identificadoresMesas[i]) -1) == "Ocupada" || disponiblidad(Convert.ToInt32(identificadoresMesas[i]) -1) == "No disponible")
                    fondo = mesaocupada;
                else if (disponiblidad(Convert.ToInt32(identificadoresMesas[i]) -1) == "Reservada")
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

                
                //Creando la mesa
                mesas[i] = new PictureBox();

                //Poniendole nombre, algo asi como un bautizo :)
                mesas[i].Name = "botonmesa" + identificadoresMesas[i];


                //Asigandoles eventos de click y hover
                mesas[i].Click += new EventHandler(clickMesa);
                mesas[i].MouseHover += new EventHandler(hoverMesaEntrante);
                mesas[i].MouseLeave += new EventHandler(hoverMesaSalida);


                //Añadiendole propiedades a cada mesa
                mesas[i].Size = new Size(80, 80);
                mesas[i].Location = new Point(posX, posY);
                mesas[i].Image = null;
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


        //Boton para cambiar de pestaña a NUEVO CLIENTE
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


        //Boton para cambiar de pestaña a RESERVAR
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


        //Boton para cambiar de pestaña a COBRAR
        private void button3_Click_1(object sender, EventArgs e)
        {
            //Asigando como accion actual 3(cobrar)
            accionactual = 3;
            /*
            //Vaciando posible informacion ingresada en otros campos 
            vaciarTextboxs();
            LlenarTextboxs();
            textBox1.ForeColor = Color.Silver;
            textBox2.ForeColor = Color.Silver;
            textBox3.ForeColor = Color.Silver;
            textBox4.ForeColor = Color.Silver;*/

            panelcobro cobro = new panelcobro();
            cobro.ShowDialog();

        }


        //Boton para registrar CLIENTE y REGISTRAR RESERVACION
        private void button13_Click_2(object sender, EventArgs e)
        {
            insertado = "NO";
            if (accionactual == 1)
            {
                if (verificarCamposLlenos() == true)
                {
                    //Antes que nada verifica que la mesa no haya sido reservada por el cliente actual
                    //En su caso cambia el estatus de la mesa a 'Ocupada'
                    if (verificarReservacion(Convert.ToInt32(idmesa)) == true)
                    {
                        ocuparMesa(Convert.ToInt32(idmesa));
                        MessageBox.Show("Mesa entregada!");
                    }

                    //De lo contrario realiza todo el proceso de registro
                    else
                    {
                        insertarCliente("Ocupada");
                        if (insertado == "OK")
                        {
                            MessageBox.Show("Insertado!");
                            insertado = "NO";
                        }
                    }
                }
            }
            else if (accionactual == 2)
            {
                if (verificarCamposLlenos() == true)
                {
                    insertarCliente("Reservada");
                    if (insertado == "OK")
                    {
                        MessageBox.Show("Reservado!");
                        insertado = "NO";
                    }
                }
            }
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

        private void button6_Click(object sender, EventArgs e)
        {

        }
    }
}
