using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace adminrestaurant
{
    class conexion
    {
        public MySqlConnection conexionObj;
        public void crearConexion()
        {
            string conec = "Server=" + variables.Servidor + ";database=" + variables.Db + ";uid=" + variables.Usuario + ";pwd=" + variables.Pass + ";";
            conexionObj = new MySqlConnection(conec);
            conexionObj.Open();
        }

        public void cerrarConexion()
        {
            conexionObj.Close();
        }

        public MySqlConnection getConexion()
        {
            return conexionObj;
        }
    }
}
