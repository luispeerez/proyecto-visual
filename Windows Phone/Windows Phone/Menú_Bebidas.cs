﻿using System;
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

namespace Windows_Phone
{
    public partial class Menú_Bebidas : Form
    {
        public Menú_Bebidas()
        {
            InitializeComponent();
            VerificaAlimentos();
            if (variables.numeroorden != " - ")
                button3.Visible = true;
        }

        public void VerificaAlimentos()
        {
            Conexion CantidadAlimentos = new Conexion();
            CantidadAlimentos.crearConexion();
            string Comando = "SELECT idalimento FROM alimento ORDER BY idalimento DESC LIMIT 1;";
            MySqlCommand Busqueda = new MySqlCommand(Comando, CantidadAlimentos.getConexion());
            string Resultado = (Busqueda.ExecuteScalar()).ToString();
            Button[] Objeto = new Button[Convert.ToInt32(Resultado)];
            int y = 1;
            for (int contador = 0; contador < Objeto.Length; contador++)
            {
                Comando = "SELECT tipoalimento FROM alimento WHERE idalimento = " + (contador + 1) + ";";
                MySqlCommand BusquedaEntrada = new MySqlCommand(Comando, CantidadAlimentos.getConexion());
                string ResultadoTipo = (BusquedaEntrada.ExecuteScalar()).ToString();
                Comando = "SELECT estatus FROM alimento WHERE idalimento = " + (contador + 1) + ";";
                MySqlCommand BusquedaEstatus = new MySqlCommand(Comando, CantidadAlimentos.getConexion());
                string ResultadoEstatus = (BusquedaEstatus.ExecuteScalar()).ToString();
                if (ResultadoTipo == "Bebida" && ResultadoEstatus == "Disponible")
                {
                    Objeto[contador] = new Button();
                    Objeto[contador].Size = new Size(260, 42);
                    Objeto[contador].Location = new Point(3, y);
                    Objeto[contador].FlatStyle = FlatStyle.Flat;
                    Objeto[contador].BackColor = Color.Transparent;
                    Objeto[contador].FlatAppearance.MouseDownBackColor = Color.Transparent;
                    Objeto[contador].FlatAppearance.MouseOverBackColor = Color.Transparent;
                    Objeto[contador].FlatAppearance.BorderSize = 0;
                    Objeto[contador].ForeColor = Color.White;
                    Objeto[contador].TextAlign = ContentAlignment.MiddleLeft;
                    Objeto[contador].Cursor = Cursors.Cross;
                    Objeto[contador].Font = new Font("Microsoft Sans Serif", 12);
                    panel1.Controls.Add(Objeto[contador]);
                    Objeto[contador].Click += new EventHandler(ClickAlimento);
                    Comando = "SELECT idalimento FROM alimento WHERE idalimento = " + (contador + 1) + ";";
                    MySqlCommand BusquedaId = new MySqlCommand(Comando, CantidadAlimentos.getConexion());
                    Resultado = (BusquedaId.ExecuteScalar()).ToString();
                    Objeto[contador].Name = Resultado;
                    Comando = "SELECT nombre FROM alimento WHERE idalimento = " + (contador + 1) + ";";
                    MySqlCommand BusquedaNombre = new MySqlCommand(Comando, CantidadAlimentos.getConexion());
                    Resultado = (BusquedaNombre.ExecuteScalar()).ToString();
                    Objeto[contador].Text = Resultado;
                    y += 50;
                }
            }
            CantidadAlimentos.cerrarConexion();
            panel1.Focus();
        }

        public void ClickAlimento(object sender, EventArgs e)
        {
            string IdObjeto = "";
            Button boton = sender as Button;
            if (boton != null)
                IdObjeto = boton.Name;

            if (variables.numeroorden != " - ")
            {
                Conexion Crear_Pedido = new Conexion();
                Crear_Pedido.crearConexion();
                string Comando = "INSERT INTO pedido (idorden, idalimento, estatus, actualizado) values (" + variables.numeroorden + "," + IdObjeto + ", 'Pendiente', 1)";
                MySqlCommand Insercion = new MySqlCommand(Comando);
                Insercion.Connection = Crear_Pedido.getConexion();
                Insercion.ExecuteNonQuery();
                Crear_Pedido.cerrarConexion();
                Menú crear = new Menú();
                this.Hide();
                crear.ShowDialog();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Menú crear = new Menú();
            this.Hide();
            crear.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Conexion Modificar = new Conexion();
            Modificar.crearConexion();
            string Comando = "UPDATE orden SET estatus='CERRADA', actualizado = 1 WHERE idorden = " + variables.numeroorden + ";";
            MySqlCommand Editar = new MySqlCommand(Comando);
            Editar.Connection = Modificar.getConexion();
            Editar.ExecuteNonQuery();
            Modificar.cerrarConexion();
            variables.numeroorden = " - ";
            Menú crear = new Menú();
            this.Hide();
            crear.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Menú crear = new Menú();
            this.Hide();
            crear.ShowDialog();
        }
    }
}
