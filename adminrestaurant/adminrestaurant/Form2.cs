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


namespace adminrestaurant
{
    public partial class Form2 : Form
    {
        //Asignando el numero maximo de mesas que pueden estar disponibles
        int numeroMaxmesas = 4;


        //Funcion para obtener el ultimo ID insertado en una tabla
        public int obtenerUltimoID(string tabla)
        {
            int resultadoQuery;
            conexion search = new conexion();
            search.crearConexion();
            string search3 = "SELECT MAX(idusuario) FROM " + tabla;
            MySqlCommand buscarproductos = new MySqlCommand(search3, search.getConexion());
            resultadoQuery = Convert.ToInt32(buscarproductos.ExecuteScalar());
            search.cerrarConexion();

            return resultadoQuery;
        }

        //Funcion para llenar todos los datagrids (Usuario,Alimento y Mesa)
        private void llenarGrids()
        {
            //Agregando toda la tabla de usuarios al datagridview
            conexion search = new conexion();
            search.crearConexion();
            string search3 = "SELECT *FROM usuario";
            MySqlCommand buscarproductos = new MySqlCommand(search3, search.getConexion());
            MySqlDataAdapter cmc = new MySqlDataAdapter(buscarproductos);
            DataSet tht = new DataSet();
            cmc.Fill(tht, "usuario");
            dataGridView1.DataSource = tht.Tables["usuario"].DefaultView;
            search.cerrarConexion();

            //Agregando toda la tabla de alimentos al datagridview
            conexion search2 = new conexion();
            search2.crearConexion();
            string search4 = "SELECT *FROM alimento";
            MySqlCommand buscaralimentos = new MySqlCommand(search4, search2.getConexion());
            MySqlDataAdapter cmc2 = new MySqlDataAdapter(buscaralimentos);
            DataSet tht2 = new DataSet();
            cmc2.Fill(tht2, "alimento");
            dataGridView2.DataSource = tht2.Tables["alimento"].DefaultView;
            search2.cerrarConexion();

            //Agregando toda la tabla de mesas al datagridview
            conexion search6 = new conexion();
            search6.crearConexion();
            string search5 = "SELECT *FROM mesa";
            MySqlCommand buscarmesas = new MySqlCommand(search5, search6.getConexion());
            MySqlDataAdapter cmc3 = new MySqlDataAdapter(buscarmesas);
            DataSet tht3 = new DataSet();
            cmc3.Fill(tht3, "mesa");
            dataGridView3.DataSource = tht3.Tables["mesa"].DefaultView;
            search6.cerrarConexion();


            //Agregando toda la tabla de pedidos dependiendo de pedidos
            conexion search7 = new conexion();
            search7.crearConexion();

            string dia = String.Format("{0:00}", Convert.ToInt32(dateTimePicker1.Value.Day));
            string mes = String.Format("{0:00}", Convert.ToInt32(dateTimePicker1.Value.Month));
            string anio = String.Format("{0:0000}", Convert.ToInt32(dateTimePicker1.Value.Year));

            //string comando = "SELECT *FROM alimento WHERE idalimento IN(SELECT idorden FROM orden WHERE fecha LIKE '"+anio+"-"+mes+"-"+dia+"%')";


            string comando =
                "SELECT pedido.idorden,tablota.nombre,tablota.precio  FROM pedido  LEFT JOIN( SELECT *FROM alimento AS alimentodia WHERE idalimento IN( SELECT idalimento FROM pedido AS pedidos WHERE idorden IN( SELECT idorden FROM orden AS ordenes WHERE fecha LIKE '"+anio+"-"+mes+"-"+dia+"%'))) AS tablota USING(idalimento)";
            
            MySqlCommand buscarpedidos = new MySqlCommand(comando, search7.getConexion());
            MySqlDataAdapter cmc4 = new MySqlDataAdapter(buscarpedidos);
            DataSet tht4 = new DataSet();
            cmc4.Fill(tht4, "orden");
            dataGridView4.DataSource = tht4.Tables["orden"].DefaultView;
            search7.cerrarConexion();
        }

        public void llenarInformacionUsuario(string idAbuscar)
        {
            conexion search = new conexion();
            search.crearConexion();
            string search3 = "SELECT *FROM usuario WHERE idusuario = "+ idAbuscar +"";
            MySqlCommand buscarproductos = new MySqlCommand(search3, search.getConexion());
            MySqlDataAdapter cmc = new MySqlDataAdapter(buscarproductos);
            DataSet tht = new DataSet();
            buscarproductos.Connection = search.getConexion();
            cmc.Fill(tht, "usuario");
            //Nombre
            textBox8.Text = tht.Tables["usuario"].Rows[0][1].ToString();
            //Apellidos
            textBox9.Text = tht.Tables["usuario"].Rows[0][2].ToString();
            //Nickname
            textBox10.Text = tht.Tables["usuario"].Rows[0][3].ToString();
            //Pass
            textBox11.Text = tht.Tables["usuario"].Rows[0][4].ToString();

            //Area
            if (tht.Tables["usuario"].Rows[0][5].ToString() != "Administrador")
            {
                if (tht.Tables["usuario"].Rows[0][5].ToString() == "Meseros")
                    comboBox3.SelectedIndex = 0;
                else
                    comboBox3.SelectedIndex = 1; ;

                comboBox3.Enabled = true;
            }
            else
                comboBox3.Enabled = false;

            //Estatus
            if (tht.Tables["usuario"].Rows[0][6].ToString() == "No disponible")
                comboBox10.SelectedIndex = 1;
            else
                comboBox10.SelectedIndex = 0;


            search.cerrarConexion();
        }

        public void llenarInformacionAlimento(string idAbuscar)
        {
            conexion search = new conexion();
            search.crearConexion();
            string search3 = "SELECT *FROM alimento WHERE idalimento = " + idAbuscar + "";
            MySqlCommand buscarproductos = new MySqlCommand(search3, search.getConexion());
            MySqlDataAdapter cmc = new MySqlDataAdapter(buscarproductos);
            DataSet tht = new DataSet();
            buscarproductos.Connection = search.getConexion();
            cmc.Fill(tht, "alimento");
            //Nombre
            textBox12.Text = tht.Tables["alimento"].Rows[0][1].ToString();
            //Descripcion
            textBox13.Text = tht.Tables["alimento"].Rows[0][3].ToString();
            //Precio
            textBox14.Text = tht.Tables["alimento"].Rows[0][4].ToString();

            //Tipo de alimento
            if (tht.Tables["alimento"].Rows[0][2].ToString() == "Entrada")
                comboBox5.SelectedIndex = 0;
            else if (tht.Tables["alimento"].Rows[0][2].ToString() == "Plato fuerte")
                comboBox5.SelectedIndex = 1;
            else if (tht.Tables["alimento"].Rows[0][2].ToString() == "Postre")
                comboBox5.SelectedIndex = 2;
            else
                comboBox5.SelectedIndex = 3;

            //Estatus
            if (tht.Tables["alimento"].Rows[0][5].ToString() == "No disponible")
                comboBox11.SelectedIndex = 1;
            else
                comboBox11.SelectedIndex = 0;

            search.cerrarConexion();

        }


        public void llenarInformacionMesa(string idAbuscar)
        {
            conexion search = new conexion();
            search.crearConexion();
            string search3 = "SELECT *FROM mesa WHERE idmesa = " + idAbuscar + "";
            MySqlCommand buscarproductos = new MySqlCommand(search3, search.getConexion());
            MySqlDataAdapter cmc = new MySqlDataAdapter(buscarproductos);
            DataSet tht = new DataSet();
            buscarproductos.Connection = search.getConexion();
            cmc.Fill(tht, "mesa");

            //Num. max de personas en la mesa
            numericUpDown3.Value = Convert.ToInt32(tht.Tables["mesa"].Rows[0][1]);
            //Estatus
            if (tht.Tables["mesa"].Rows[0][2].ToString() == "No disponible")
                comboBox8.SelectedIndex = 1;
            else
                comboBox8.SelectedIndex = 0;

            search.cerrarConexion();

        }

        private void registrarUsuario()
        {
            conexion ins_pro = new conexion();
            ins_pro.crearConexion();
            string inserta = "INSERT INTO usuario (nombre, apellidos,nickname,pass,area,estatus) Values ('" + textBox1.Text + "', '" + textBox2.Text + "' , '" + textBox3.Text + "' , '" + textBox4.Text + "' , '"+ comboBox1.SelectedItem +"','"+ comboBox12.SelectedItem +"')";
            MySqlCommand pro = new MySqlCommand(inserta);
            pro.Connection = ins_pro.getConexion();
            pro.ExecuteNonQuery();
            ins_pro.cerrarConexion();



            //Agregando por default un registro en la tabla mesero si el usuario registrado esta en esa area
            if (comboBox1.SelectedItem.ToString() == "Meseros")
            {
                int ultimoID = obtenerUltimoID("usuario");
                conexion ingresarMesero = new conexion();
                ingresarMesero.crearConexion();
                inserta = "INSERT INTO mesero (nombre, apellidos,estatus,idusuario) Values ('" + textBox1.Text + "', '" + textBox2.Text + "' , '" + comboBox12.SelectedItem + "'," + ultimoID + ")";
                MySqlCommand pro2 = new MySqlCommand(inserta);
                pro2.Connection = ingresarMesero.getConexion();
                pro2.ExecuteNonQuery();
                ingresarMesero.cerrarConexion();
            }
            MessageBox.Show("Se ha ingresado satisfactoriamente", "Insercion exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);

            
        }

        public int contarUsuarios(string tabla)
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

        public int contarMesasDisponibles()
        {
            int resultadoQuery;
            conexion search = new conexion();
            search.crearConexion();
            string search3 = "SELECT COUNT(*) FROM mesa WHERE estatus='Disponible' OR estatus='Reservada' OR estatus='Ocupada'";
            MySqlCommand buscarproductos = new MySqlCommand(search3, search.getConexion());
            resultadoQuery = Convert.ToInt32(buscarproductos.ExecuteScalar());

            search.cerrarConexion();

            return resultadoQuery;
        }

        private void registrarAlimento()
        {
            conexion ins_pro = new conexion();
            ins_pro.crearConexion();
            string inserta = "INSERT INTO alimento (nombre, tipoalimento,descripcion,precio,estatus) Values ('" + textBox5.Text + "', '" + comboBox2.SelectedItem + "' , '"+ textBox6.Text +"' , "+ textBox7.Text +",'"+ comboBox13.SelectedItem +"')";
            MySqlCommand pro = new MySqlCommand(inserta);
            pro.Connection = ins_pro.getConexion();
            pro.ExecuteNonQuery();
            MessageBox.Show("Se ha ingresado satisfactoriamente", "Insercion exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);

            ins_pro.cerrarConexion();
        }

        private void registrarMesa()
        {
            conexion ins_pro = new conexion();
            ins_pro.crearConexion();
            string inserta = "INSERT INTO mesa (numpersonas, estatus) Values (" + numericUpDown1.Value + ", '" + comboBox9.SelectedItem + "' )";
            MySqlCommand pro = new MySqlCommand(inserta);
            pro.Connection = ins_pro.getConexion();
            pro.ExecuteNonQuery();
            MessageBox.Show("Se ha ingresado satisfactoriamente", "Insercion exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);

            ins_pro.cerrarConexion();
        }

        //Llenando los elementos de los combobox que estna en la opcion de modificar con los id de la tabla usuario,alimento y  mesa
        private void llenarCombo(string tabla)
        {
            int contador;
            //Vaciando todos los elementos cargados anteriormente

            if (tabla == "usuario")
            {
                /*if (comboBox4.Items.Count > 0)
                    for (contador = 0; contador < comboBox4.Items.Count; contador++)
                        comboBox4.Items.RemoveAt(contador);*/
                comboBox4.Items.Clear();
            }
            else if (tabla == "alimento")
            {
                /*if (comboBox6.Items.Count > 0)
                    for (contador = 0; contador < comboBox6.Items.Count; contador++)
                        comboBox6.Items.RemoveAt(contador);*/
                comboBox6.Items.Clear();
            }
            else if (tabla == "mesa")
            {
                /*if (comboBox7.Items.Count > 0)
                    for (contador = 0; contador < comboBox7.Items.Count; contador++)
                        comboBox7.Items.RemoveAt(contador);*/
                comboBox7.Items.Clear();
            }

            int numUsuarios = contarUsuarios(tabla);
            conexion search = new conexion();
            search.crearConexion();
            string search3 = "SELECT *FROM " + tabla + "";
            MySqlCommand buscarproductos = new MySqlCommand(search3, search.getConexion());
            MySqlDataAdapter cmc = new MySqlDataAdapter(buscarproductos);
            DataSet tht = new DataSet();
            buscarproductos.Connection = search.getConexion();
            cmc.Fill(tht, tabla);


            for (int i = 0; i < numUsuarios; i++)
            {
                //Condicionando en que combobox se llenaran los elementos
                if(tabla == "usuario")
                    comboBox4.Items.Add(tht.Tables[tabla].Rows[i][0].ToString());
                else if (tabla == "alimento")
                    comboBox6.Items.Add(tht.Tables[tabla].Rows[i][0].ToString());
                else if(tabla == "mesa")
                    comboBox7.Items.Add(tht.Tables[tabla].Rows[i][0].ToString());
            }

            search.cerrarConexion();
        }

        private void limpiar()
        {
            //Vaciando campos de AGREGAR USUARIO
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            comboBox1.Text = "";

            //Vaciando campos de MODIFICAR USUARIO
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";
            comboBox3.Text = "";
            comboBox4.Text = "";

            //Vaciando campos de AGREGAR ALIMENTO
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            comboBox2.Text = "";

            //Vaciando campos de MODIFICAR ALIMENTO
            textBox12.Text = "";
            textBox13.Text = "";
            textBox14.Text = "";
            comboBox6.Text = "";
            comboBox5.Text = "";

            //Vaciando campos de AGREGAR MESA
            numericUpDown1.Value = 0;
            comboBox9.Text = "";

        }

        public void actualizarUsuario(string usuarioID)
        {
            //Actualizacion
            conexion mod = new conexion();
            mod.crearConexion();
            string actualizar = " UPDATE usuario SET nombre=@C2,apellidos=@C3, nickname=@C4 , pass=@C5 , area=@C6 , estatus=@C7 WHERE idusuario=" + usuarioID + "";
            MySqlCommand revisa = new MySqlCommand(actualizar);
            revisa.Connection = mod.getConexion();
            revisa.Parameters.AddWithValue("@C2", (textBox8.Text));
            revisa.Parameters.AddWithValue("@C3", (textBox9.Text));
            revisa.Parameters.AddWithValue("@C4", (textBox10.Text));
            revisa.Parameters.AddWithValue("@C5", (textBox11.Text));
            revisa.Parameters.AddWithValue("@C6", (comboBox3.SelectedItem.ToString()));
            revisa.Parameters.AddWithValue("@C7", (comboBox10.SelectedItem.ToString()));

            mod.getConexion();
            revisa.ExecuteNonQuery();
            mod.cerrarConexion();


            //Agregando por default un registro en la tabla mesero si el usuario registrado esta en esa area
            if (comboBox3.SelectedItem.ToString() == "Meseros")
            {
                conexion ingresarMesero = new conexion();
                ingresarMesero.crearConexion();
                actualizar = "UPDATE mesero SET nombre = '" + textBox8.Text + "', apellidos = '" + textBox9.Text + "' , estatus = '" + comboBox10.SelectedItem + "' WHERE idusuario = " + usuarioID;
                MySqlCommand pro2 = new MySqlCommand(actualizar);
                pro2.Connection = ingresarMesero.getConexion();
                pro2.ExecuteNonQuery();
                ingresarMesero.cerrarConexion();
            }


            MessageBox.Show("Registro Modificado ", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

        }

        public void actualizarAlimento(string alimentoID)
        {
            //Actualizacion
            conexion mod = new conexion();
            mod.crearConexion();
            string actualizar = " UPDATE alimento SET nombre=@C2,descripcion=@C3, precio=@C4 , tipoalimento=@C5 , estatus=@C6 WHERE idalimento=" + alimentoID + "";
            MySqlCommand revisa = new MySqlCommand(actualizar);
            revisa.Connection = mod.getConexion();
            revisa.Parameters.AddWithValue("@C2", (textBox12.Text));
            revisa.Parameters.AddWithValue("@C3", (textBox13.Text));
            revisa.Parameters.AddWithValue("@C4", (textBox14.Text));
            revisa.Parameters.AddWithValue("@C5", (comboBox5.SelectedItem));
            revisa.Parameters.AddWithValue("@C6", (comboBox11.SelectedItem));
            MessageBox.Show("Registro Modificado ", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            mod.getConexion();
            revisa.ExecuteNonQuery();
            mod.cerrarConexion();
        }


        public void actualizarMesa(string mesaID)
        {
            //Actualizacion
            conexion mod = new conexion();
            mod.crearConexion();
            string actualizar = " UPDATE mesa SET numpersonas=@C2,estatus=@C3 WHERE idmesa =" + mesaID + "";
            MySqlCommand revisa = new MySqlCommand(actualizar);
            revisa.Connection = mod.getConexion();
            revisa.Parameters.AddWithValue("@C2", (numericUpDown3.Value));
            revisa.Parameters.AddWithValue("@C3", (comboBox8.SelectedItem));

            MessageBox.Show("Registro Modificado ", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            mod.getConexion();
            revisa.ExecuteNonQuery();
            mod.cerrarConexion();
        }


        public Form2()
        {
            InitializeComponent();

            //Creando un timer para manejar los intervalos de actualizacion
            var timer = new Timer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = 30000; //30 segundos
            timer.Start();
        }


        //Evento en en el que se actualizan las mesas(definido en el timer)
        public void timer_Tick(object sender, EventArgs e)
        {
            //Actualiza los grids de al admin en caso de que la pestaña seleccionada sea la de venta
            if (tabControl1.SelectedIndex == 3)
            {
                llenarGrids();
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
            radioButton6.Checked = true;
            radioButton9.Checked = true;
            //Ocultando el formulario para modificar usuarios por default
            groupBox5.Visible = false;
            groupBox7.Visible = false;

            //Llenando los combobox de usuario y alimento
            llenarCombo("usuario");
            llenarCombo("alimento");
            llenarCombo("mesa");
            
            llenarGrids();
        }

        //Boton para agregar usuario
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //Verificando si los campos estan llenos
                if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && comboBox1.SelectedItem.ToString() != "" && comboBox12.SelectedItem.ToString() != "")
                {
                    registrarUsuario();
                    llenarGrids();
                    llenarCombo("usuario");
                    limpiar();
                }
                else
                {
                    MessageBox.Show("Formato invalido o incompleto");
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Formato invalido o incompleto");
            }
        }

        //Boton para agregar alimento
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                //Verificando si los campos estan llenos
                if (textBox5.Text != "" && textBox6.Text != "" && textBox7.Text != "" && comboBox2.SelectedItem.ToString() != "" && comboBox13.SelectedItem.ToString() != "")
                {
                    registrarAlimento();
                    llenarGrids();
                    llenarCombo("alimento");
                    limpiar();
                }
                else
                {
                    MessageBox.Show("Formato invalido o incompleto");
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Formato invalido o incompleto");

            }
        }

        //Boton para agregar mesa
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                //Verificando si los campos estan llenos
                if (numericUpDown1.Value > 0 && comboBox9.SelectedItem.ToString() != "")
                {
                    //Verifica que entre en el rango de mesas disponibles
                    if (contarMesasDisponibles() < numeroMaxmesas)
                    {
                        MessageBox.Show("numero de mesas disponibles: " + contarMesasDisponibles());
                        registrarMesa();
                        llenarGrids();
                        llenarCombo("mesa");
                        limpiar();
                    }
                    else
                    {
                        //Si no esta en el rango aun asi lo añade si es que la agrega como no disponible
                        if (comboBox9.SelectedItem.ToString() != "Disponible")
                        {
                            registrarMesa();
                            llenarGrids();
                            llenarCombo("mesa");
                            limpiar();
                        }
                        //Si agrega la mesa como disponible , se enseña un mensaje de error
                        else
                            MessageBox.Show("El numero de mesas disponibles ha llegado al maximo, modifica algula mesa para poder registrar una mesa disponible");

                    }
                }
                else
                {
                    MessageBox.Show("Formato invalido o incompleto");
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Formato invalido o incompleto");
            }
        }

        //Radio buttons de la seccion de usuarios

        //Radiobuton AGREGAR USUARIO
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            groupBox5.Visible = false;
        }

        //Radiobuton MODIFICAR USUARIO
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            groupBox5.Visible = true;
        }

        //Radiobuton ELIMINAR USUARIO
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
        }

        //
        //

        //Readio buttons de la seccion de alimentos

        //Radio button AGREGAR ALIMENTO
        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            groupBox4.Visible = true;
            groupBox6.Visible = false;
        }

        //Radio button MODIFICAR ALIMENTO
        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            groupBox4.Visible = false;
            groupBox6.Visible = true;
        }

        //Radio button ELIMINAR ALIMENTO
        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            groupBox4.Visible = false;
        }

        //Radio buttons de la seccion de mesas

        //Radio button AGREGAR MESA
        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
            groupBox7.Visible = false;
            groupBox9.Visible = true;
        }

        //Radio button MODIFICAR MESA
        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            groupBox7.Visible = true;
            groupBox9.Visible = false;
        }
        //Radio button ELIMINAR MESA
        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {

        }


        //LLenando textboxs con la info actual del usuario antes de modificar cualquier campo
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenarInformacionUsuario((comboBox4.SelectedItem).ToString());
        }

        //LLenando textboxs con la info actual del alimento antes de modificar cualquier campo
        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenarInformacionAlimento((comboBox6.SelectedItem).ToString());
        }

        //LLenando textboxs con la info actual de la mesa antes de modificar cualquier campo
        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenarInformacionMesa((comboBox7.SelectedItem).ToString());
        }



        //Boton para modificar al usuario
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                //Verificando si los campos estan llenos
                if (textBox8.Text != "" && textBox9.Text != "" && textBox10.Text != "" && textBox11.Text != "" && comboBox3.SelectedItem.ToString() != "" && comboBox4.SelectedItem.ToString() != "" && comboBox10.SelectedItem.ToString() != "")
                {
                    actualizarUsuario(comboBox4.SelectedItem.ToString());
                    llenarGrids();
                    llenarCombo("usuario");
                }
                else
                {
                    MessageBox.Show("Formato invalido o incompleto");
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Formato invalido o incompleto");
            }
        }

        //Boton para modificar al alimento
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                //Verificando si los campos estan llenos
                if (textBox12.Text != "" && textBox13.Text != "" && textBox14.Text != "" && comboBox5.SelectedItem.ToString() != "" && comboBox11.SelectedItem.ToString() != "" && comboBox6.SelectedItem.ToString() != "")
                {
                    actualizarAlimento(comboBox6.SelectedItem.ToString());
                    llenarGrids();
                    llenarCombo("alimento");
                }
                else
                {
                    MessageBox.Show("Formato invalido o incompleto");
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Formato invalido o incompleto");

            }
        }

        //Boton para modificar la mesa
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                //Verificando si los campos estan llenos
                if (numericUpDown3.Value > 0 && comboBox8.SelectedItem.ToString() != "" && comboBox7.SelectedItem.ToString() != "")
                {
                    //Verifica que entre en el rango de mesas disponibles
                    if (contarMesasDisponibles() < numeroMaxmesas)
                    {
                        actualizarMesa(comboBox7.SelectedItem.ToString());
                        llenarGrids();
                        llenarCombo("mesa");
                    }
                    else
                    {
                        //Si no esta en el rango aun asi lo añade si es que la modifica como no disponible
                        if (comboBox7.SelectedItem.ToString() != "Disponible")
                        {
                            actualizarMesa(comboBox7.SelectedItem.ToString());
                            llenarGrids();
                            llenarCombo("mesa");
                        }
                        //Si modifica la mesa como disponible , se enseña un mensaje de error
                        else
                            MessageBox.Show("El numero de mesas disponibles ha llegado al maximo, modifica algula mesa para poder registrar una mesa disponible");

                    }
                }
                else
                {
                    MessageBox.Show("Formato invalido o incompleto");
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Formato invalido o incompleto");
            }

        }

        private void label30_Click(object sender, EventArgs e)
        {

        }

        private void comboBox11_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //Asignando eventos cuando se llena el ultimo campo de cada formulario
        //Seccion de usuarios
        private void comboBox12_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                button1.Focus();
                return;
            }
        }

        private void comboBox10_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                button3.Focus();
                return;
            }
        }

        //Solo admitiendo numeros cuando se asigna el precio de un alimento
        private void textBox14_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != '.'))
            {
                MessageBox.Show("Solo se admiten numeros.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != '.'))
            {
                MessageBox.Show("Solo se admiten numeros.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 2;
        }

        private void button12_MouseHover(object sender, EventArgs e)
        {
            button12.Visible = false;
            button13.Visible = true;
        }

        private void button13_MouseLeave(object sender, EventArgs e)
        {
            button12.Visible = true;
            button13.Visible = false;
        }

        private void button11_MouseHover(object sender, EventArgs e)
        {
            button11.Visible = false;
            button14.Visible = true;
        }

        private void button14_MouseLeave(object sender, EventArgs e)
        {
            button11.Visible = true;
            button14.Visible = false;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 3;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            llenarGrids();
        }
    }
}
