using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adminrestaurant
{
    class variables
    {
        //Configuracion de la conexion, la pueden cambiar dependiendo de su configuracion local
        static string servidor = "localhost";
        static string db = "restaurant";
        static string usuario = "root";
        static string pass = "";

        static string usuarioActivo;

        public static string UsuarioActivo
        {
            get { return variables.usuarioActivo; }
            set { variables.usuarioActivo = value; }
        }


        public static string Servidor
        {
            get { return variables.servidor; }
            set { variables.servidor = value; }
        }


        public static string Db
        {
            get { return variables.db; }
            set { variables.db = value; }
        }


        public static string Usuario
        {
            get { return variables.usuario; }
            set { variables.usuario = value; }
        }


        public static string Pass
        {
            get { return variables.pass; }
            set { variables.pass = value; }
        }
    }
}
