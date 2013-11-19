using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Prueba___Proyecto
{
    class conexion
    {
        public MySqlConnection conexionObj;
        public void crearConexion()
        {
            string conec = "Server=localhost;database=tienda;uid=root;pwd=;";
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
