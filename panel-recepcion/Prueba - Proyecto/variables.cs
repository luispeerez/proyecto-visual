using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba___Proyecto
{
    class variables
    {
        //Configuracion de la conexion, la pueden cambiar dependiendo de su configuracion local
        static string servidor = "localhost";
        static string db = "restaurant";
        static string usuario = "root";
        static string pass = "";
        static double totalOrden = 0;
        static int idordenApagar;

        public static int IdordenApagar
        {
            get { return variables.idordenApagar; }
            set { variables.idordenApagar = value; }
        }

        public static double TotalOrden
        {
            get { return variables.totalOrden; }
            set { variables.totalOrden = value; }
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
        static int cantidadMesas;

        public static int CantidadMesas
        {
            get { return variables.cantidadMesas; }
            set { variables.cantidadMesas = value; }
        }

    }
}
