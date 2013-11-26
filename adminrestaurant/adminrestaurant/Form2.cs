﻿using System;
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

            conexion search2 = new conexion();
            search2.crearConexion();
            string search4 = "SELECT *FROM alimento";
            MySqlCommand buscaralimentos = new MySqlCommand(search4, search2.getConexion());
            MySqlDataAdapter cmc2 = new MySqlDataAdapter(buscaralimentos);
            DataSet tht2 = new DataSet();
            cmc2.Fill(tht, "alimento");
            dataGridView2.DataSource = tht.Tables["alimento"].DefaultView;
        }

        public void llenarInformacionUsuario(string idAbuscar)
        {
            string[] resultados = new string[4];
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
            //return resultados;
        }

        public void llenarInformacionAlimento(string idAbuscar)
        {
            string[] resultados = new string[4];
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

        }

        private void registrarUsuario()
        {
            conexion ins_pro = new conexion();
            ins_pro.crearConexion();
            string inserta = "INSERT INTO usuario (nombre, apellidos,nickname,pass,area) Values ('" + textBox1.Text + "', '" + textBox2.Text + "' , '" + textBox3.Text + "' , '" + textBox4.Text + "' , '"+ comboBox1.SelectedItem +"')";
            MySqlCommand pro = new MySqlCommand(inserta);
            pro.Connection = ins_pro.getConexion();
            pro.ExecuteNonQuery();
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

            return resultadoQuery;
        }

        private void registrarAlimento()
        {
            conexion ins_pro = new conexion();
            ins_pro.crearConexion();
            string inserta = "INSERT INTO alimento (nombre, tipoalimento,descripcion,precio) Values ('" + textBox5.Text + "', '" + comboBox2.SelectedItem + "' , '"+ textBox6.Text +"' , "+ textBox7.Text +")";
            MySqlCommand pro = new MySqlCommand(inserta);
            pro.Connection = ins_pro.getConexion();
            pro.ExecuteNonQuery();
            MessageBox.Show("Se ha ingresado satisfactoriamente", "Insercion exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //Llenando los elementos de los combobox que estna en la opcion de modificar con los id de la tabla usuario,alimento y  mesa
        private void llenarCombo(string tabla)
        {
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
            }
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

        }

        public void actualizarUsuario(string usuarioID)
        {
            //Actualizacion
            conexion mod = new conexion();
            mod.crearConexion();
            string actualizar = " UPDATE usuario SET nombre=@C2,apellidos=@C3, nickname=@C4 , pass=@C5 , area=@C6 WHERE idusuario=" + usuarioID + "";
            MySqlCommand revisa = new MySqlCommand(actualizar);
            revisa.Connection = mod.getConexion();
            revisa.Parameters.AddWithValue("@C2", (textBox8.Text));
            revisa.Parameters.AddWithValue("@C3", (textBox9.Text));
            revisa.Parameters.AddWithValue("@C4", (textBox10.Text));
            revisa.Parameters.AddWithValue("@C5", (textBox11.Text));
            revisa.Parameters.AddWithValue("@C6", (comboBox3.SelectedItem.ToString()));
            MessageBox.Show("Registro Modificado ", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            mod.getConexion();
            revisa.ExecuteNonQuery();
            mod.cerrarConexion();

        }

        public void actualizarAlimento(string alimentoID)
        {
            //Actualizacion
            conexion mod = new conexion();
            mod.crearConexion();
            string actualizar = " UPDATE alimento SET nombre=@C2,descripcion=@C3, precio=@C4 , tipoalimento=@C5 WHERE idalimento=" + alimentoID + "";
            MySqlCommand revisa = new MySqlCommand(actualizar);
            revisa.Connection = mod.getConexion();
            revisa.Parameters.AddWithValue("@C2", (textBox12.Text));
            revisa.Parameters.AddWithValue("@C3", (textBox13.Text));
            revisa.Parameters.AddWithValue("@C4", (textBox14.Text));
            revisa.Parameters.AddWithValue("@C5", (comboBox5.SelectedItem));
            MessageBox.Show("Registro Modificado ", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            mod.getConexion();
            revisa.ExecuteNonQuery();
            mod.cerrarConexion();

        }

        public Form2()
        {
            InitializeComponent();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
            radioButton6.Checked = true;
            //Ocultando el formulario para modificar usuarios por default
            groupBox5.Visible = false;

            //Llenando los comobox de usuario y alimento
            llenarCombo("usuario");
            llenarCombo("alimento");
            
            llenarGrids();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            registrarUsuario();
            llenarGrids();
            limpiar();
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

        private void button2_Click(object sender, EventArgs e)
        {
            registrarAlimento();
            llenarGrids();
            limpiar();
        }

        //LLenando textboxs con la info actual del usuario antes de modificar cualquier campo
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenarInformacionUsuario((comboBox4.SelectedItem).ToString());
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenarInformacionAlimento((comboBox6.SelectedItem).ToString());
        }

        //Boton para modificar al usuario
        private void button3_Click(object sender, EventArgs e)
        {
            actualizarUsuario(comboBox4.SelectedItem.ToString());
            llenarGrids();
            llenarCombo("usuario");
        }

        //Boton para modificar al alimento
        private void button4_Click(object sender, EventArgs e)
        {
            actualizarAlimento(comboBox6.SelectedItem.ToString());
            llenarGrids();
            llenarCombo("alimento");
        }






        //
    }
}
